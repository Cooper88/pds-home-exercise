using UKParliament.CodeTest.Web.ValidationAttributes;

namespace UKParliament.CodeTest.Tests.ValidationAttributes;

using System;
using Xunit;

public class PresentOrPastDateAttributeTests
{
    [Theory]
    [InlineData("01/01/2020")]
    [InlineData("31/12/1999")]
    public void IsValid_ValidPastDate_ReturnsTrue(string date)
    {
        // Arrange
        var attribute = new PresentOrPastDateAttribute();

        // Act
        var result = attribute.IsValid(date);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsValid_TodayDate_ReturnsTrue()
    {
        // Arrange
        var today = DateTime.Now.ToString("dd/MM/yyyy");
        var attribute = new PresentOrPastDateAttribute();

        // Act
        var result = attribute.IsValid(today);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsValid_FutureDate_ReturnsFalse()
    {
        // Arrange
        var futureDate = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
        var attribute = new PresentOrPastDateAttribute();

        // Act
        var result = attribute.IsValid(futureDate);

        // Assert
        Assert.False(result);
        Assert.Equal("This date is in the future.", attribute.ErrorMessage);
    }

    [Theory]
    [InlineData("2020-01-01")]
    [InlineData("01-01-2020")]
    [InlineData("01/01/20")]
    [InlineData("abcd")]
    public void IsValid_InvalidFormat_ReturnsFalse(string date)
    {
        // Arrange
        var attribute = new PresentOrPastDateAttribute();

        // Act
        var result = attribute.IsValid(date);

        // Assert
        Assert.False(result);
        Assert.Equal("Invalid format. Accepted format is DD/MM/YYYY.", attribute.ErrorMessage);
    }

    [Theory]
    [InlineData("32/01/2023")]
    [InlineData("29/02/2021")]
    [InlineData("01/13/2020")]
    public void IsValid_NonExistentDate_ReturnsFalse(string date)
    {
        // Arrange
        var attribute = new PresentOrPastDateAttribute();

        // Act
        var result = attribute.IsValid(date);

        // Assert
        Assert.False(result);
        Assert.Equal("This date is invalid.", attribute.ErrorMessage);
    }
}