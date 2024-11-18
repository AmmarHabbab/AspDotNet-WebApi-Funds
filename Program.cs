using Microsoft.AspNetCore.StaticFiles;  
using Serilog;
using WebApi1.Services;
using WebApi1;
using WebApi1.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;
using Microsoft.OpenApi.Models;

Log.Logger = new LoggerConfiguration() // configuring serilog
   .MinimumLevel.Debug()
   .WriteTo.Console()
   .WriteTo.File("logs/cityinfo.txt",rollingInterval: RollingInterval.Day)
   .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
// builder.Logging.ClearProviders(); // here we configure them manually instead of relying on the createdefaultbuilder code 
// builder.Logging.AddConsole();

builder.Host.UseSerilog(); // use Serlig Logger
// Add services to the container.

builder.Services.AddControllers(options => {
    //option.InputFormatters.Add(formatters => ) u know and the defualt is the first one in list is always the default
    //option.OutputFormatters.Add(formatters => ) u know and the defualt is the first one in list is always the default
    options.ReturnHttpNotAcceptable = true; // 406 not acceptable if xml for and example requiested and api only supports json
}).AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters(); // supports additional dataformatters like xml
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction=>{
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory,xmlCommentsFile);

    setupAction.IncludeXmlComments(xmlCommentsFullPath);

    setupAction.AddSecurityDefinition("CityInfoApiBearerAuth",new OpenApiSecurityScheme(){
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a Valid token to access this API"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
        new OpenApiSecurityScheme{
            Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = "CityInfoApiBearerAuth"}
            }, new List<string>()}
        });
    });
builder.Services.AddSingleton<FileExtensionContentTypeProvider>(); // add a service to the container so that we can inject it in other parts of our code that will be used to get the content type of a file 

//builder.Services.AddTransient<LocalMailService>();
#if DEBUG
builder.Services.AddTransient<IMailService,LocalMailService>();
#else
builder.Services.AddTransient<IMailService,CloudMailService>();
#endif

builder.Services.AddSingleton<CitiesDataStore>();

builder.Services.AddDbContext<CityInfoContext>(DbContextOptions=>DbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:CityInfoDBConnectionString"]));
// for a production envronment we will environment varialbe to store data that which exists in windows or u can use azure 

builder.Services.AddScoped<ICityInfoRepository,CityInforepository>();// addscoped gets it created once per request and it works pretty well with reposirty pattern

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options => 
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
    };
});

builder.Services.AddAuthorization(options => {
    options.AddPolicy("MustBeFromAntwerp",policy => {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("city","Antwerp");
    });
});

builder.Services.AddApiVersioning(setupAction=>{
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
    setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1,0);
    setupAction.ReportApiVersions = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

//app.MapControllers();

app.UseEndpoints(endpoints =>{
    endpoints.MapControllers();
});

app.Run();
