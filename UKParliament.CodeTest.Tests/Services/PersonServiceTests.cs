using NSubstitute;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Repositories.Person;
using UKParliament.CodeTest.Services;
using Xunit;

namespace UKParliament.CodeTest.Tests.Services;

public class PersonServiceTests
{
    private readonly IPersonRepository _personRepository;
    private readonly PersonService _personService;

    public PersonServiceTests()
    {
        _personRepository = Substitute.For<IPersonRepository>();
        _personService = new PersonService(_personRepository);
    }

    [Fact]
    public void PersonService_GetAll_ReturnsAllPeople()
    {
        // Arrange
        var personList = new List<Person>
        {
            new() { Id = 1, FirstName = "Jeff", LastName = "Cooper" },
            new() { Id = 2, FirstName = "Matt", LastName = "Smith" },
            new() { Id = 3, FirstName = "David", LastName = "Moore" }
        };

        _personRepository.GetAll().Returns(personList);

        // Act
        var result = _personService.GetAll();

        // Assert
        Assert.Equal(3, result.Count());

        _personRepository.Received(1).GetAll();
    }

    [Fact]
    public void PersonService_Get_ReturnsPerson()
    {
        // Arrange
        var person = new Person { Id = 1, FirstName = "Jeff", LastName = "Cooper" };

        _personRepository.Get(1).Returns(person);

        // Act
        var result = _personService.Get(1);

        // Assert
        Assert.Equal(result.Id, person.Id);
        Assert.Equal(result.FirstName, person.FirstName);
        Assert.Equal(result.LastName, person.LastName);

        _personRepository.Received(1).Get(Arg.Any<int>());
    }

    [Fact]
    public void PersonService_Update()
    {
        // Arrange
        var person = new Person { Id = 1, FirstName = "Jeff", LastName = "Cooper" };

        _personRepository.Update(Arg.Any<Person>());

        // Act
        _personService.Update(person);

        // Assert
        _personRepository.Received(1).Update(Arg.Any<Person>());
    }
}