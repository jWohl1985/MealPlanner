using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealPlanner.Domain.Models;
using MealPlanner.WPF.Helpers;
using MealPlanner.Tests.Generators;

namespace MealPlanner.Tests.DomainTests;

public class UserGoalsCalculatorTests
{
    [Theory]
    [MemberData(nameof(UserTestGenerator.GetUserCalorieGoal), MemberType = typeof(UserTestGenerator))]
    public void Calorie_Goal_Calculates_Correctly(DietUser user, int expectedCalorieGoal)
    {
        // Arrange
        UserGoalCalculator sut = new UserGoalCalculator(user);

        // Act
        int actualCalorieGoal = sut.CalorieGoal;

        // Assert
        Assert.Equal(expected: expectedCalorieGoal, actual: actualCalorieGoal);
    }

    [Theory]
    [MemberData(nameof(UserTestGenerator.GetUserProteinGoal), MemberType = typeof(UserTestGenerator))]
    public void Protein_Goal_Calculates_Correctly(DietUser user, int expectedProteinGoal)
    {
        // Arrange
        UserGoalCalculator sut = new UserGoalCalculator(user);

        // Act
        int actualProteinGoal = sut.ProteinGramGoal;

        // Assert
        Assert.Equal(expected: expectedProteinGoal, actual: actualProteinGoal);
    }

    [Theory]
    [MemberData(nameof(UserTestGenerator.GetUserFatGoal), MemberType = typeof(UserTestGenerator))]
    public void Fat_Goal_Calculates_Correctly(DietUser user, int expectedFatGoal)
    {
        // Arrange
        UserGoalCalculator sut = new UserGoalCalculator(user);

        // Act
        int actualFatGoal = sut.FatGramGoal;

        // Assert
        Assert.Equal(expected: expectedFatGoal, actual: actualFatGoal);
    }

    [Theory]
    [MemberData(nameof(UserTestGenerator.GetUserMinimumAndMaximumCarbGoal), MemberType = typeof(UserTestGenerator))]
    public void Carb_Min_And_Max_Calculate_Correctly(DietUser user, int expectedCarbMinimum, int expectedCarbMaximum)
    {
        // Arrange
        UserGoalCalculator sut = new UserGoalCalculator(user);

        // Act
        int actualCarbMinimum = sut.CarbGramMinimum;
        int actualCarbMaximum = sut.CarbGramMaximum;

        // Assert
        Assert.Equal(expected: expectedCarbMinimum, actual: actualCarbMinimum);
        Assert.Equal(expected: expectedCarbMaximum, actual: actualCarbMaximum);
    }
}
