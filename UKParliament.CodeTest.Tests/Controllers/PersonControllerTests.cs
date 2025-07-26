using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.Controllers;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Tests.Controllers;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

public class PersonControllerTests
{
    private readonly IPersonService _personService = Substitute.For<IPersonService>();
    private readonly IDepartmentService _departmentService = Substitute.For<IDepartmentService>();
    private readonly PersonController _controller;

    public PersonControllerTests()
    {
        _controller = new PersonController(_personService, _departmentService);
    }

    [Fact]
    public void GetById_WhenPersonExists_ReturnsOkResult()
    {
        // Arrange
        var person = new Person
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            DepartmentId = 1,
            EmailAddress = "john@example.com"
        };

        _personService.Get(1).Returns(person);

        // Act
        var result = _controller.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var personViewModel = Assert.IsType<PersonViewModel>(okResult.Value);
        Assert.Equal(person.Id, personViewModel.Id);
        Assert.Equal(person.FirstName, personViewModel.FirstName);
        _personService.Received(1).Get(1);
    }

    [Fact]
    public void GetById_WhenPersonDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        _personService.Get(1).Returns((Person)null);

        // Act
        var result = _controller.GetById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
        _personService.Received(1).Get(1);
    }

    [Fact]
    public void GetAll_ReturnsOkResult_WithPersonList()
    {
        // Arrange
        var people = new List<Person>
        {
            new() { Id = 1, FirstName = "John", LastName = "Doe", DepartmentId = 1 },
            new() { Id = 2, FirstName = "Jane", LastName = "Smith", DepartmentId = 2 }
        };

        var departments = new List<Department>
        {
            new() { Id = 1, Name = "HR" },
            new() { Id = 2, Name = "IT" }
        };

        _personService.GetAll().Returns(people);
        _departmentService.GetAll().Returns(departments);

        // Act
        var result = _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var personViewModels = Assert.IsAssignableFrom<List<PersonViewModel>>(okResult.Value);
        Assert.Equal(2, personViewModels.Count);
        _personService.Received(1).GetAll();
        _departmentService.Received(1).GetAll();
    }

    [Fact]
    public void Update_ShouldCallPersonServiceUpdate_AndReturnOk()
    {
        // Arrange
        var personViewModel = new PersonViewModel
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            DepartmentId = 1,
            DateOfBirth = "2000-01-01",
            EmailAddress = "john@example.com"
        };

        // Act
        var result = _controller.Update(personViewModel);

        // Assert
        Assert.IsType<OkResult>(result);
        _personService.Received(1).Update(Arg.Any<Person>());
    }

    [Fact]
    public void Add_ShouldCallPersonServiceAdd_AndReturnOk()
    {
        // Arrange
        var personViewModel = new PersonViewModel
        {
            Id = 3,
            FirstName = "Alice",
            LastName = "Wonderland",
            DepartmentId = 1,
            DateOfBirth = "2001-05-05",
            EmailAddress = "alice@example.com"
        };

        // Act
        var result = _controller.Add(personViewModel);

        // Assert
        Assert.IsType<OkResult>(result);
        _personService.Received(1).Add(Arg.Any<Person>());
    }
}