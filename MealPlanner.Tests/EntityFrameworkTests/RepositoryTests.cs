using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealPlanner.Domain.Models;
using MealPlanner.Domain.Services;
using Moq;

namespace MealPlanner.Tests.EntityFrameworkTests;

public class RepositoryTests
{
    [Fact]
    public void Get_All_Does_Not_Return_Null()
    {
        // Arrange
        Mock<IRepository<object>> sut = new Mock<IRepository<object>>();

        // Act
        IEnumerable<object> result = sut.Object.GetAll();

        // Assert
        Assert.NotNull(result);
    }
}
