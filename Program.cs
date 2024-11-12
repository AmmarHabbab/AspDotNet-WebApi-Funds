using Microsoft.AspNetCore.StaticFiles;  
using Serilog;

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
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>(); // add a service to the container so that we can inject it in other parts of our code that will be used to get the content type of a file 

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

//app.MapControllers();

app.UseEndpoints(endpoints =>{
    endpoints.MapControllers();
});

app.Run();
