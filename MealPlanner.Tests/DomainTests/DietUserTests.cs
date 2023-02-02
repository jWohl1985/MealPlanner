using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealPlanner.Domain.Models;
using MealPlanner.Tests.Generators;

namespace MealPlanner.Tests.DomainTests;

public class DietUserTests
{
    [Theory]
    [MemberData(nameof(UserTestGenerator.GetTestUsers), MemberType = typeof(UserTestGenerator))]
    public void Height_In_Inches_Calculates_Correctly(DietUser user)
    {
        // Arrange

        // Act
        int expectedHeightInInches = user.HeightFeetComponent * 12 + user.HeightInchComponent;

        // Assert
        Assert.Equal(expected: expectedHeightInInches, actual: user.HeightInInches);
    }
}
