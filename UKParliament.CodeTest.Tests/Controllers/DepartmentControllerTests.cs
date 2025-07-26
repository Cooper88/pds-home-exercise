using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.Controllers;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Tests.Controllers;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

public class DepartmentControllerTests
{
    [Fact]
    public void GetAll_ShouldReturnOkResult_WithDepartmentList()
    {
        // Arrange
        var departmentService = Substitute.For<IDepartmentService>();
        departmentService.GetAll().Returns(new List<Department>
        {
            new() { Id = 1, Name = "HR" },
            new() { Id = 2, Name = "IT" }
        });

        var controller = new DepartmentController(departmentService);

        // Act
        var result = controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var departments = Assert.IsAssignableFrom<List<DepartmentViewModel>>(okResult.Value);

        Assert.Equal(2, departments.Count);
        Assert.Equal("HR", departments[0].Name);
        Assert.Equal("IT", departments[1].Name);
        
        departmentService.Received(1).GetAll();
    }

    [Fact]
    public void GetAll_WhenNoDepartments_ReturnsEmptyList()
    {
        // Arrange
        var departmentService = Substitute.For<IDepartmentService>();
        departmentService.GetAll().Returns(new List<Department>());

        var controller = new DepartmentController(departmentService);

        // Act
        var result = controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var departments = Assert.IsAssignableFrom<List<DepartmentViewModel>>(okResult.Value);
        Assert.Empty(departments);
    }
}
