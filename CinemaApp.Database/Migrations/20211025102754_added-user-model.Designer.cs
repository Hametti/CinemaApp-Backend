﻿// <auto-generated />
using System;
using CinemaApp.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CinemaApp.Database.Migrations
{
    [DbContext(typeof(CinemaAppDbContext))]
    [Migration("20211025102754_added-user-model")]
    partial class addedusermodel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CinemaApp.Database.Entities.MovieModels.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Budget")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cast")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Director")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Duration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LongDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReleaseYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("CinemaApp.Database.Entities.MovieModels.Screening", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.Property<int?>("ScreeningDayId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("ScreeningDayId");

                    b.ToTable("Screenings");
                });

            modelBuilder.Entity("CinemaApp.Database.Entities.MovieModels.ScreeningDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ScreeningDays");
                });

            modelBuilder.Entity("CinemaApp.Database.Entities.MovieModels.ScreeningHour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Hour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ScreeningId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ScreeningId");

                    b.ToTable("ScreeningHours");
                });

            modelBuilder.Entity("CinemaApp.Database.Entities.MovieModels.Screening", b =>
                {
                    b.HasOne("CinemaApp.Database.Entities.MovieModels.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId");

                    b.HasOne("CinemaApp.Database.Entities.MovieModels.ScreeningDay", null)
                        .WithMany("Screenings")
                        .HasForeignKey("ScreeningDayId");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("CinemaApp.Database.Entities.MovieModels.ScreeningHour", b =>
                {
                    b.HasOne("CinemaApp.Database.Entities.MovieModels.Screening", null)
                        .WithMany("ScreeningHours")
                        .HasForeignKey("ScreeningId");
                });

            modelBuilder.Entity("CinemaApp.Database.Entities.MovieModels.Screening", b =>
                {
                    b.Navigation("ScreeningHours");
                });

            modelBuilder.Entity("CinemaApp.Database.Entities.MovieModels.ScreeningDay", b =>
                {
                    b.Navigation("Screenings");
                });
#pragma warning restore 612, 618
        }
    }
}