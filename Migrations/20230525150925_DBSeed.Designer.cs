﻿// <auto-generated />
using System;
using AspCoreWebAPIDemos.DBContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AspCoreWebAPIDemos.Migrations
{
    [DbContext(typeof(CityContext))]
    [Migration("20230525150925_DBSeed")]
    partial class DBSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("AspCoreWebAPIDemos.Entities.City", b =>
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
                            Description = "This is Thai Lan capital",
                            Name = "Bangkok"
                        },
                        new
                        {
                            Id = 3,
                            Description = "This is a very big city of China",
                            Name = "Beijing"
                        });
                });

            modelBuilder.Entity("AspCoreWebAPIDemos.Entities.Rate", b =>
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
                });

            modelBuilder.Entity("AspCoreWebAPIDemos.Models.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("District");
                });

            modelBuilder.Entity("AspCoreWebAPIDemos.Entities.Rate", b =>
                {
                    b.HasOne("AspCoreWebAPIDemos.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("AspCoreWebAPIDemos.Models.District", b =>
                {
                    b.HasOne("AspCoreWebAPIDemos.Entities.City", null)
                        .WithMany("Districts")
                        .HasForeignKey("CityId");
                });

            modelBuilder.Entity("AspCoreWebAPIDemos.Entities.City", b =>
                {
                    b.Navigation("Districts");
                });
#pragma warning restore 612, 618
        }
    }
}
