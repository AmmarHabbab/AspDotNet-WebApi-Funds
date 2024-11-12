using System.Drawing;
using Microsoft.EntityFrameworkCore;
using WebApi1.Entities;

namespace WebApi1.DbContexts;

public class CityInfoContext : DbContext
{
    public DbSet<City> Cities {get;set;} = null!; //null forgiving operator
    public DbSet<PointOfInterest> PointOfInterest {get;set;} = null!;

    public CityInfoContext(DbContextOptions<CityInfoContext> options) // another way
    : base (options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>().HasData(
             new City("New York City")
            {
                Id = 1,
                Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf.",
            },
            new City("Antwerp")
            {
                Id = 2,
                Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf.",
            },
            new City("Paris")
            {
                Id = 3,
                Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf.",
            }
        );

         modelBuilder.Entity<PointOfInterest>().HasData(
            new PointOfInterest("Central Park")
            {
                Id = 1,
                cityId = 1,
                        Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf"
            },
             new PointOfInterest("Times Square")
            {
                Id = 2, cityId = 1,
                        Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf"
            },
            new PointOfInterest("sadf")
                    {
                        Id = 3, cityId = 2,
                        Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf"
                    },
                    new PointOfInterest("Times fghgfdhf")
                    {
                        Id = 4, cityId = 2,
                        Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf"
                    },
                    new PointOfInterest("Censadsa sadtral Park")
                    {
                        Id = 5, cityId = 3,
                        Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf"
                    },
                    new PointOfInterest("Timxzczxxces Square")
                    {
                        Id = 6, cityId = 3,
                        Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf"
                    }
         );
        base.OnModelCreating(modelBuilder);
    }
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) one way
    // {
    //     optionsBuilder.UseSqlite("conntectionstring");
    //     base.OnConfiguring(optionsBuilder);
    // }
}