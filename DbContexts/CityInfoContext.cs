using Microsoft.EntityFrameworkCore;
using WebApi1.Entities;

namespace WebApi1.DbContexts;

public class CityInfoContext : DbContext
{
    public DbSet<City> Cities {get;set;} = null!; //null forgiving operator
    public DbSet<PointOfInterest> PointOfInterest {get;set;} = null!;

    public CityInfoContext(DbContextOptions<CityInfoContext> options) // another way
    : base (options){}
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) one way
    // {
    //     optionsBuilder.UseSqlite("conntectionstring");
    //     base.OnConfiguring(optionsBuilder);
    // }
}