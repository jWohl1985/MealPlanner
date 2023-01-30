using Take100.Domain.Models;
using Take100.EntityFramework;
using Xunit.Sdk;

namespace Take100.Tests;

public class DietContextTests
{
    [Fact]
    public void DietContextFactory_Should_Return_DietContext()
    {
        // Arrange
        DietContextFactory sut = new DietContextFactory();

        // Act
        var context = sut.CreateDbContext();

        // Assert
        Assert.NotNull(context);
        Assert.IsType<DietContext>(context);
    }

    [Fact]
    public void DietContext_DbSets_Should_Not_Be_Null()
    {
        // Arange
        DietContextFactory contextFactory = new DietContextFactory();

        // Act
        var sut = contextFactory.CreateDbContext();

        // Assert
        Assert.NotNull(sut.Users);
        Assert.NotNull(sut.Foods);
        Assert.NotNull(sut.Meals);
    }
}