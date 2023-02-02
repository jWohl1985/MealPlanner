using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MealPlanner.Domain.Models;
using MealPlanner.EntityFramework.SeedData;

namespace MealPlanner.EntityFramework;

public class DietContext : DbContext
{
    public DietContext(DbContextOptions<DietContext> options) : base(options)
    {

    }

    public DbSet<DietUser> Users { get; set; } = default!;
    public DbSet<Food> Foods { get; set; } = default!;
    public DbSet<Meal> Meals { get; set; } = default!;
    public DbSet<WeightLogEntry> WeightLogs { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new SeedFoodData());
    }
}
