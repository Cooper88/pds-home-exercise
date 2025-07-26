using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Web.Mapping;

namespace UKParliament.CodeTest.Tests.Mapping;

using System.Collections.Generic;
using Xunit;

public class DepartmentViewModelMapperTests
{
    [Fact]
    public void MapToDepartmentViewModel_ShouldMapSingleDepartment()
    {
        // Arrange
        var departments = new List<Department>
        {
            new() { Id = 1, Name = "HR" }
        };

        // Act
        var result = DepartmentViewModelMapper.MapToDepartmentViewModel(departments);

        // Assert
        Assert.Single(result);
        Assert.Equal(1, result[0].Id);
        Assert.Equal("HR", result[0].Name);
    }

    [Fact]
    public void MapToDepartmentViewModel_ShouldMapMultipleDepartments()
    {
        // Arrange
        var departments = new List<Department>
        {
            new() { Id = 1, Name = "HR" },
            new() { Id = 2, Name = "IT" }
        };

        // Act
        var result = DepartmentViewModelMapper.MapToDepartmentViewModel(departments);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("HR", result[0].Name);
        Assert.Equal("IT", result[1].Name);
    }
}