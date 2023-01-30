﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Take100.EntityFramework;

#nullable disable

namespace Take100.EntityFramework.Migrations
{
    [DbContext(typeof(DietContext))]
    [Migration("20230126202813_WeightLogs")]
    partial class WeightLogs
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<float>("HeightInInches")
                        .HasColumnType("real");

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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 37,
                            Goal = 0,
                            HeightInInches = 69f,
                            HipCircumferenceInches = 0f,
                            HoursExercisePerWeek = 3,
                            IsMale = true,
                            Name = "John",
                            NeckCircumferenceInches = 13f,
                            WaistCircumferenceInches = 32f,
                            WeightInLbs = 145f
                        },
                        new
                        {
                            Id = 2,
                            Age = 54,
                            Goal = 0,
                            HeightInInches = 65f,
                            HipCircumferenceInches = 38f,
                            HoursExercisePerWeek = 1,
                            IsMale = false,
                            Name = "Mary",
                            NeckCircumferenceInches = 15f,
                            WaistCircumferenceInches = 38f,
                            WeightInLbs = 170f
                        });
                });

            modelBuilder.Entity("Take100.Domain.Models.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Calories")
                        .HasColumnType("real");

                    b.Property<int>("FoodCategoryId")
                        .HasColumnType("int");

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
                            FoodCategoryId = 3,
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
                            FoodCategoryId = 3,
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
                            FoodCategoryId = 1,
                            GramsOfCarbs = 57.5f,
                            GramsOfFat = 2.8f,
                            GramsOfProtein = 9.5f,
                            Name = "Frosted Miniwheats",
                            Notes = "with 120mL milk",
                            ServingSize = "60g"
                        });
                });

            modelBuilder.Entity("Take100.Domain.Models.FoodCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FoodCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "breakfast"
                        },
                        new
                        {
                            Id = 2,
                            Description = "lunch"
                        },
                        new
                        {
                            Id = 3,
                            Description = "snack"
                        },
                        new
                        {
                            Id = 4,
                            Description = "dinner"
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

                    b.Property<float>("ServingsToEat")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("DietUserId");

                    b.HasIndex("FoodId");

                    b.ToTable("Meals");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DayNumber = 1,
                            DietUserId = 1,
                            FoodId = 1,
                            ServingsToEat = 1f
                        },
                        new
                        {
                            Id = 2,
                            DayNumber = 1,
                            DietUserId = 1,
                            FoodId = 2,
                            ServingsToEat = 2f
                        },
                        new
                        {
                            Id = 3,
                            DayNumber = 2,
                            DietUserId = 2,
                            FoodId = 3,
                            ServingsToEat = 1f
                        },
                        new
                        {
                            Id = 4,
                            DayNumber = 2,
                            DietUserId = 2,
                            FoodId = 2,
                            ServingsToEat = 0.5f
                        });
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
