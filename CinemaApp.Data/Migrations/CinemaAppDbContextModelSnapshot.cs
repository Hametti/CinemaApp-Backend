﻿// <auto-generated />
using CinemaApp.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CinemaApp.Database.Migrations
{
    [DbContext(typeof(CinemaAppDbContext))]
    partial class CinemaAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CinemaApp.Database.Entities.Movie.DailyView", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DailyViews");
                });

            modelBuilder.Entity("CinemaApp.Database.Entities.Movie.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("CinemaApp.Database.Entities.Movie.ShowingHour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Hour")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ShowingHours");
                });

            modelBuilder.Entity("DailyViewMovie", b =>
                {
                    b.Property<int>("DailyViewListId")
                        .HasColumnType("int");

                    b.Property<int>("MovieListId")
                        .HasColumnType("int");

                    b.HasKey("DailyViewListId", "MovieListId");

                    b.HasIndex("MovieListId");

                    b.ToTable("DailyViewMovie");
                });

            modelBuilder.Entity("MovieShowingHour", b =>
                {
                    b.Property<int>("MovieListId")
                        .HasColumnType("int");

                    b.Property<int>("ShowingHourListId")
                        .HasColumnType("int");

                    b.HasKey("MovieListId", "ShowingHourListId");

                    b.HasIndex("ShowingHourListId");

                    b.ToTable("MovieShowingHour");
                });

            modelBuilder.Entity("DailyViewMovie", b =>
                {
                    b.HasOne("CinemaApp.Database.Entities.Movie.DailyView", null)
                        .WithMany()
                        .HasForeignKey("DailyViewListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinemaApp.Database.Entities.Movie.Movie", null)
                        .WithMany()
                        .HasForeignKey("MovieListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieShowingHour", b =>
                {
                    b.HasOne("CinemaApp.Database.Entities.Movie.Movie", null)
                        .WithMany()
                        .HasForeignKey("MovieListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinemaApp.Database.Entities.Movie.ShowingHour", null)
                        .WithMany()
                        .HasForeignKey("ShowingHourListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}