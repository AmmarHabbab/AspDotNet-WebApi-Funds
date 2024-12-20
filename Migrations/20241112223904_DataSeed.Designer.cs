﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi1.DbContexts;

#nullable disable

namespace WebApi1.Migrations
{
    [DbContext(typeof(CityInfoContext))]
    [Migration("20241112223904_DataSeed")]
    partial class DataSeed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("WebApi1.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf.",
                            Name = "New York City"
                        },
                        new
                        {
                            Id = 2,
                            Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf.",
                            Name = "Antwerp"
                        },
                        new
                        {
                            Id = 3,
                            Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf.",
                            Name = "Paris"
                        });
                });

            modelBuilder.Entity("WebApi1.Entities.PointOfInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("cityId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("cityId");

                    b.ToTable("PointOfInterest");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf",
                            Name = "Central Park",
                            cityId = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf",
                            Name = "Times Square",
                            cityId = 1
                        },
                        new
                        {
                            Id = 3,
                            Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf",
                            Name = "sadf",
                            cityId = 2
                        },
                        new
                        {
                            Id = 4,
                            Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf",
                            Name = "Times fghgfdhf",
                            cityId = 2
                        },
                        new
                        {
                            Id = 5,
                            Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf",
                            Name = "Censadsa sadtral Park",
                            cityId = 3
                        },
                        new
                        {
                            Id = 6,
                            Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf",
                            Name = "Timxzczxxces Square",
                            cityId = 3
                        });
                });

            modelBuilder.Entity("WebApi1.Entities.PointOfInterest", b =>
                {
                    b.HasOne("WebApi1.Entities.City", null)
                        .WithMany("PointsOfInterest")
                        .HasForeignKey("cityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi1.Entities.City", b =>
                {
                    b.Navigation("PointsOfInterest");
                });
#pragma warning restore 612, 618
        }
    }
}
