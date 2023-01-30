using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Take100.Domain.Models;

namespace Take100.EntityFramework.SeedData;

internal class SeedMealData : IEntityTypeConfiguration<Meal>
{
    public void Configure(EntityTypeBuilder<Meal> builder)
    {
        Meal[] meals = SeedMeals();
        builder.HasData(meals);
    }

    public Meal[] SeedMeals()
    {
        Meal johnMeal1 = new Meal
        {
            Id = 1,
            DietUserId = 1,
            DayNumber = 1,
            FoodId = 1,
            ServingsToEat = 1,
        };

        Meal johnMeal2 = new Meal
        {
            Id = 2,
            DietUserId = 1,
            DayNumber = 1,
            FoodId = 2,
            ServingsToEat = 2,
        };

        Meal maryMeal1 = new Meal
        {
            Id = 3,
            DietUserId = 2,
            DayNumber = 2,
            FoodId = 3,
            ServingsToEat = 1,
        };

        Meal maryMeal2 = new Meal
        {
            Id = 4,
            DietUserId = 2,
            DayNumber = 2,
            FoodId = 2,
            ServingsToEat = .5f,
        };

        return new Meal[] { johnMeal1, johnMeal2, maryMeal1, maryMeal2 };
    }
}
