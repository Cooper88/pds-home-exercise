using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Web.Mapping;

namespace UKParliament.CodeTest.Tests.Mapping;

using System;
using System.Collections.Generic;
using Xunit;

public class PersonViewModelMapperTests
{
    [Fact]
    public void MapToPersonViewModel_SinglePerson_ShouldMapCorrectly()
    {
        // Arrange
        var person = new Person
        {
            Id = 1,
            FirstName = "Jane",
            LastName = "Smith",
            DateOfBirth = new DateOnly(1990, 5, 10),
            DepartmentId = 2,
            EmailAddress = "jane.smith@example.com"
        };

        // Act
        var result = PersonViewModelMapper.MapToPersonViewModel(person);

        // Assert
        Assert.Equal(1, result.Id);
        Assert.Equal("Jane", result.FirstName);
        Assert.Equal("Smith", result.LastName);
        Assert.Equal("10/05/1990", result.DateOfBirth);
        Assert.Equal(2, result.DepartmentId);
        Assert.Equal("jane.smith@example.com", result.EmailAddress);
    }

    [Fact]
    public void MapToPersonViewModel_MultiplePeople_ShouldMapAll_With_DepartmentName()
    {
        // Arrange
        var persons = new List<Person>
        {
            new()
            {
                Id = 1, FirstName = "Dave", LastName = "Moore",
                DateOfBirth = new DateOnly(1990, 5, 10),
                DepartmentId = 1, EmailAddress = "dave.moore@test.com"
            },
            new()
            {
                Id = 2, FirstName = "Jeff", LastName = "Cooper",
                DateOfBirth = new DateOnly(1985, 12, 1),
                DepartmentId = 2, EmailAddress = "jeff.cooper@test.com"
            }
        };

        var departments = new List<Department>
        {
            new() { Id = 1, Name = "HR" },
            new() { Id = 2, Name = "IT" }
        };

        // Act
        var result = PersonViewModelMapper.MapToPersonViewModel(persons, departments);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("HR", result[0].DepartmentName);
        Assert.Equal("IT", result[1].DepartmentName);
    }
}