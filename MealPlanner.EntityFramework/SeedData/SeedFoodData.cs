using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MealPlanner.Domain.Models;

namespace MealPlanner.EntityFramework.SeedData;

internal class SeedFoodData : IEntityTypeConfiguration<Food>
{
    public void Configure(EntityTypeBuilder<Food> builder)
    {
        Food[] foods = SeedFoods();
        builder.HasData(foods);
    }

    public static Food[] SeedFoods()
    {
        Food banana = new()
        {
            Id = 1,
            Name = "Banana",
            ServingSize = "118g",
            Calories = 105,
            GramsOfProtein = 1.3f,
            GramsOfCarbs = 27,
            GramsOfFat = .4f,
        };

        Food proteinShake = new()
        {
            Id = 2,
            Name = "Protein Shake",
            ServingSize = "118g",
            Notes = "Optimum Nutrition - Vanilla Ice Cream",
            Calories = 120,
            GramsOfProtein = 24,
            GramsOfCarbs = 4,
            GramsOfFat = 1,
        };

        Food miniWheats = new()
        {
            Id = 3,
            Name = "Frosted Miniwheats",
            ServingSize = "60g",
            Notes = "with 120mL milk",
            Calories = 265,
            GramsOfProtein = 9.5f,
            GramsOfCarbs = 57.5f,
            GramsOfFat = 2.8f,
        };

        return new Food[] { banana, proteinShake, miniWheats };
    }
}
