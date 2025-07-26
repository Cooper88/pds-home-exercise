using UKParliament.CodeTest.Web.ValidationAttributes;

namespace UKParliament.CodeTest.Tests.ValidationAttributes;

using Xunit;

public class EmailAttributeTests
{
    [Theory]
    [InlineData("test@example.com")]
    [InlineData("user.name@domain.co.uk")]
    [InlineData("user_name@domain.org")]
    [InlineData("user-name@domain.com")]
    [InlineData("USER@EXAMPLE.COM")]
    public void IsValid_ValidEmails_ReturnsTrue(string email)
    {
        // Arrange
        var attribute = new EmailAttribute();

        // Act
        var result = attribute.IsValid(email);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData("plainaddress")]
    [InlineData("missing@dot")]
    [InlineData("@nouser.com")]
    [InlineData("user@.com")]
    [InlineData("user@domain,com")]
    [InlineData("user domain.com")]
    public void IsValid_InvalidEmails_ReturnsFalse(string email)
    {
        // Arrange
        var attribute = new EmailAttribute();

        // Act
        var result = attribute.IsValid(email);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValid_EmptyString_ReturnsFalse()
    {
        // Arrange
        var attribute = new EmailAttribute();

        // Act
        var result = attribute.IsValid("");

        // Assert
        Assert.False(result);
    }
}
