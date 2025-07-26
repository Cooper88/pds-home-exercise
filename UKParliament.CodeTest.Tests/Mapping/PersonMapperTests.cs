using UKParliament.CodeTest.Web.Mapping;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Tests.Mapping;

using System;
using Xunit;

public class PersonMapperTests
{
    [Fact]
    public void MapToPerson_ShouldMapAllPropertiesCorrectly()
    {
        // Arrange
        var personViewModel = new PersonViewModel
        {
            Id = 1,
            FirstName = "Jane",
            LastName = "Austin",
            DateOfBirth = "01/01/1999",
            DepartmentId = 3,
            EmailAddress = "jane.austin@example.com"
        };

        // Act
        var result = PersonMapper.MapToPerson(personViewModel);

        // Assert
        Assert.Equal(1, result.Id);
        Assert.Equal("Jane", result.FirstName);
        Assert.Equal("Austin", result.LastName);
        Assert.Equal(new DateOnly(1999, 1, 1), result.DateOfBirth);
        Assert.Equal(3, result.DepartmentId);
        Assert.Equal("jane.austin@example.com", result.EmailAddress);
    }
}