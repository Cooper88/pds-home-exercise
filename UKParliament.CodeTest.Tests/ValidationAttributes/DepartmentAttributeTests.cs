using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.ValidationAttributes;

namespace UKParliament.CodeTest.Tests.ValidationAttributes;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NSubstitute;
using Xunit;

public class DepartmentAttributeTests
{
    [Fact]
    public void IsValid_DepartmentIdExists_ShouldReturnSuccess()
    {
        // Arrange
        var departmentService = Substitute.For<IDepartmentService>();
        departmentService.GetAll().Returns(new List<Department>
        {
            new() { Id = 1, Name = "HR" },
            new() { Id = 2, Name = "IT" }
        });

        var attribute = new DepartmentAttribute();
        var validationContext = CreateValidationContext(departmentService);

        // Act
        var result = attribute.GetValidationResult(2, validationContext);

        // Assert
        Assert.Equal(ValidationResult.Success, result);
    }

    [Fact]
    public void IsValid_DepartmentIdDoesNotExist_ShouldReturnError()
    {
        // Arrange
        var departmentService = Substitute.For<IDepartmentService>();
        departmentService.GetAll().Returns(new List<Department> 
        { 
            new() { Id = 1, Name = "HR" } 
        });

        var attribute = new DepartmentAttribute();
        var validationContext = CreateValidationContext(departmentService);

        // Act
        var result = attribute.GetValidationResult(99, validationContext);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("A valid DepartmentId is required.", result.ErrorMessage);
    }

    private static ValidationContext CreateValidationContext(IDepartmentService departmentService)
    {
        var serviceProvider = Substitute.For<IServiceProvider>();
        serviceProvider.GetService(typeof(IDepartmentService)).Returns(departmentService);

        return new ValidationContext(new object(), serviceProvider, null);
    }
}
