using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take100.Domain.Models;
using Take100.WPF.Helpers;

namespace MealPlanner.Tests;

public class UserGoalsCalculatorTests
{
    [Theory]
    [MemberData(nameof(UserTestGenerator.GetUserCalories), MemberType = typeof(UserTestGenerator))]
    public void Calorie_Goal_Calculates_Correctly(DietUser user, int expectedCalories)
    {
        // Arrange
        UserGoalCalculator sut = new UserGoalCalculator(user);

        // Act
        int calorieGoal = sut.CalorieGoal;

        // Assert
        Assert.Equal(expectedCalories, calorieGoal);
    }

    public static class UserTestGenerator 
    {
        public static IEnumerable<object[]> GetUserCalories()
        {
            // expected calorie goal as calculated by hand from the formulas
            yield return new object[] { TestMaleCutter, 1433 };
            yield return new object[] { TestMaleMaintainer, 2698 };
            yield return new object[] { TestMaleBulker, 2205 };
            yield return new object[] { TestFemaleCutter, 1147 };
            yield return new object[] { TestFemaleMaintainer, 1702 };
            yield return new object[] { TestFemaleBulker, 1899 };
        }

        public static DietUser TestMaleCutter = new()
        {
            Age = 30,
            IsMale = true,
            WeightInLbs = 160,
            Goal = 0, // lose weight
            HoursExercisePerWeek = 2,
            HeightFeetComponent = 5,
            HeightInchComponent = 10,
            NeckCircumferenceInches = 14,
            WaistCircumferenceInches = 38,
        };

        public static DietUser TestMaleMaintainer = new()
        {
            Age = 25,
            IsMale = true,
            WeightInLbs = 195,
            Goal = 1, // maintain weight
            HoursExercisePerWeek = 5,
            HeightFeetComponent = 6,
            HeightInchComponent = 2,
            NeckCircumferenceInches = 13.5f,
            WaistCircumferenceInches = 34,
        };

        public static DietUser TestMaleBulker = new()
        {
            Age = 41,
            IsMale = true,
            WeightInLbs = 140,
            Goal = 2, // gain weight
            HoursExercisePerWeek = 4,
            HeightFeetComponent = 5,
            HeightInchComponent = 8,
            NeckCircumferenceInches = 12.75f,
            WaistCircumferenceInches = 32,
        };

        public static DietUser TestFemaleCutter = new()
        {
            Age = 60,
            IsMale = false,
            WeightInLbs = 180,
            Goal = 0, // lose weight
            HoursExercisePerWeek = 0,
            HeightFeetComponent = 5,
            HeightInchComponent = 5,
            NeckCircumferenceInches = 16,
            WaistCircumferenceInches = 42,
            HipCircumferenceInches = 44,
        };

        public static DietUser TestFemaleMaintainer = new()
        {
            Age = 18,
            IsMale = false,
            WeightInLbs = 120,
            Goal = 1, // maintain weight
            HoursExercisePerWeek = 3,
            HeightFeetComponent = 5,
            HeightInchComponent = 3,
            NeckCircumferenceInches = 12,
            WaistCircumferenceInches = 30,
            HipCircumferenceInches = 30,
        };

        public static DietUser TestFemaleBulker = new()
        {
            Age = 28,
            IsMale = false,
            WeightInLbs = 110,
            Goal = 2, // gain weight
            HoursExercisePerWeek = 6,
            HeightFeetComponent = 5,
            HeightInchComponent = 8,
            NeckCircumferenceInches = 13,
            WaistCircumferenceInches = 32,
            HipCircumferenceInches = 34,
        };
    }
}
