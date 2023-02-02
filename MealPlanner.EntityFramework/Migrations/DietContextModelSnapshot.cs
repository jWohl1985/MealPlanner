﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MealPlanner.EntityFramework;

#nullable disable

namespace MealPlanner.EntityFramework.Migrations
{
    [DbContext(typeof(DietContext))]
    partial class DietContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Take100.Domain.Models.DietUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("Goal")
                        .HasColumnType("int");

                    b.Property<int>("HeightFeetComponent")
                        .HasColumnType("int");

                    b.Property<int>("HeightInchComponent")
                        .HasColumnType("int");

                    b.Property<float>("HipCircumferenceInches")
                        .HasColumnType("real");

                    b.Property<int>("HoursExercisePerWeek")
                        .HasColumnType("int");

                    b.Property<bool>("IsMale")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("NeckCircumferenceInches")
                        .HasColumnType("real");

                    b.Property<float>("WaistCircumferenceInches")
                        .HasColumnType("real");

                    b.Property<float>("WeightInLbs")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Take100.Domain.Models.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Calories")
                        .HasColumnType("real");

                    b.Property<float>("GramsOfCarbs")
                        .HasColumnType("real");

                    b.Property<float>("GramsOfFat")
                        .HasColumnType("real");

                    b.Property<float>("GramsOfProtein")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServingSize")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Foods");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Calories = 105f,
                            GramsOfCarbs = 27f,
                            GramsOfFat = 0.4f,
                            GramsOfProtein = 1.3f,
                            Name = "Banana",
                            ServingSize = "118g"
                        },
                        new
                        {
                            Id = 2,
                            Calories = 120f,
                            GramsOfCarbs = 4f,
                            GramsOfFat = 1f,
                            GramsOfProtein = 24f,
                            Name = "Protein Shake",
                            Notes = "Optimum Nutrition - Vanilla Ice Cream",
                            ServingSize = "118g"
                        },
                        new
                        {
                            Id = 3,
                            Calories = 265f,
                            GramsOfCarbs = 57.5f,
                            GramsOfFat = 2.8f,
                            GramsOfProtein = 9.5f,
                            Name = "Frosted Miniwheats",
                            Notes = "with 120mL milk",
                            ServingSize = "60g"
                        });
                });

            modelBuilder.Entity("Take100.Domain.Models.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DayNumber")
                        .HasColumnType("int");

                    b.Property<int>("DietUserId")
                        .HasColumnType("int");

                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.Property<bool>("HasBeenEaten")
                        .HasColumnType("bit");

                    b.Property<float>("ServingsToEat")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("DietUserId");

                    b.HasIndex("FoodId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("Take100.Domain.Models.WeightLogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DietUserId")
                        .HasColumnType("int");

                    b.Property<float>("WeightInLbs")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("DietUserId");

                    b.ToTable("WeightLogs");
                });

            modelBuilder.Entity("Take100.Domain.Models.Meal", b =>
                {
                    b.HasOne("Take100.Domain.Models.DietUser", "DietUser")
                        .WithMany()
                        .HasForeignKey("DietUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Take100.Domain.Models.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DietUser");

                    b.Navigation("Food");
                });

            modelBuilder.Entity("Take100.Domain.Models.WeightLogEntry", b =>
                {
                    b.HasOne("Take100.Domain.Models.DietUser", "DietUser")
                        .WithMany()
                        .HasForeignKey("DietUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DietUser");
                });
#pragma warning restore 612, 618
        }
    }
}
