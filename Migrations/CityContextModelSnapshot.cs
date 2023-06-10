﻿// <auto-generated />
using System;
using AspCoreWebAPIDemos.DBContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AspCoreWebAPIDemos.Migrations
{
    [DbContext(typeof(CityContext))]
    partial class CityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("AspCoreWebAPIDemos.Entities.CityEntity", b =>
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

                    b.ToTable("City");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "This is Viet Nam capital",
                            Name = "Ha Noi"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Thai Lan capital where is a very attractive tourist place",
                            Name = "Bangkok"
                        },
                        new
                        {
                            Id = 3,
                            Description = "China captial with many Chinese traditional food you can taste",
                            Name = "Beijing"
                        },
                        new
                        {
                            Id = 4,
                            Description = "A beautiful city of Japan located in the South East",
                            Name = "Okinawa"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Kingdom of fashion and France capital. You should definitely visit it at least once.",
                            Name = "Paris"
                        });
                });

            modelBuilder.Entity("AspCoreWebAPIDemos.Entities.RateEntity", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("GuestName")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Point")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Rate");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 1,
                            GuestName = "Nguyen Son",
                            Point = 10
                        },
                        new
                        {
                            Id = 2,
                            CityId = 1,
                            GuestName = "Thu Huong",
                            Point = 7
                        },
                        new
                        {
                            Id = 3,
                            CityId = 1,
                            GuestName = "Sarah Chalez",
                            Point = 4
                        },
                        new
                        {
                            Id = 4,
                            CityId = 2,
                            GuestName = "David Micheal",
                            Point = 8
                        },
                        new
                        {
                            Id = 5,
                            CityId = 2,
                            GuestName = "Mariah Ozawa",
                            Point = 6
                        },
                        new
                        {
                            Id = 6,
                            CityId = 3,
                            GuestName = "Okata Mutan",
                            Point = 9
                        });
                });

            modelBuilder.Entity("AspCoreWebAPIDemos.Models.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CityEntityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CityEntityId");

                    b.ToTable("District");
                });

            modelBuilder.Entity("AspCoreWebAPIDemos.Entities.RateEntity", b =>
                {
                    b.HasOne("AspCoreWebAPIDemos.Entities.CityEntity", "City")
                        .WithMany("Rates")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("AspCoreWebAPIDemos.Models.District", b =>
                {
                    b.HasOne("AspCoreWebAPIDemos.Entities.CityEntity", null)
                        .WithMany("Districts")
                        .HasForeignKey("CityEntityId");
                });

            modelBuilder.Entity("AspCoreWebAPIDemos.Entities.CityEntity", b =>
                {
                    b.Navigation("Districts");

                    b.Navigation("Rates");
                });
#pragma warning restore 612, 618
        }
    }
}
