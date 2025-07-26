using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Repositories.Person;
using Xunit;

namespace UKParliament.CodeTest.Tests.Repositories.Person;

public class PersonRepositoryTests : BaseRepository
{
    private static Data.Person CreatePerson(int id = 1)
    {
        return new Data.Person { 
            Id = id, 
            FirstName = "John", 
            LastName = "Cooper", 
            EmailAddress = "test@test.com",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            DepartmentId = 1
        };
    }

    [Fact]
    public void PersonRepository_Get_ReturnsCorrectPerson()
    {
        var options = CreateNewContextOptions();

        using var context = new PersonManagerContext(options);
        var person = CreatePerson();
        
        context.People.Add(person);
        context.SaveChanges();

        var repo = new PersonRepository(context);
        var result = repo.Get(1);

        Assert.NotNull(result);
        Assert.Equal("John", result.FirstName);
        Assert.Equal("Cooper", result.LastName);
        Assert.Equal(DateOnly.FromDateTime(DateTime.Now), result.DateOfBirth);
        Assert.Equal(1, result.DepartmentId);
        Assert.Equal("test@test.com", result.EmailAddress);
    }

    [Fact]
    public void PersonRepository_Get_ReturnsNull()
    {
        var options = CreateNewContextOptions();

        using var context = new PersonManagerContext(options);
        var person = CreatePerson();
        
        context.People.Add(person);
        context.SaveChanges();

        var repo = new PersonRepository(context);
        var result = repo.Get(2);

        Assert.Null(result);
       
    }
    
    [Fact]
    public void PersonRepository_GetAll_ReturnsAllPeople()
    {
        var options = CreateNewContextOptions();

        using var context = new PersonManagerContext(options);
        
        context.People.AddRange(
            CreatePerson(1),
            CreatePerson(2),
            CreatePerson(3)
        );
        context.SaveChanges();

        var repo = new PersonRepository(context);
        var result = repo.GetAll();

        Assert.Equal(3, result.Count());
    }
    
    [Fact]
    public void PersonRepository_Update_UpdatesExistingPerson()
    {
        var options = CreateNewContextOptions();

        using var context = new PersonManagerContext(options);
        var person = CreatePerson();
        context.People.Add(person);
        context.SaveChanges();
            
        var repo = new PersonRepository(context);
        var updatePerson = new Data.Person
        {
            Id = 1, 
            FirstName = "Jeff", 
            LastName = "Moore", 
            EmailAddress = "jeff.moore@gmail.com",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            DepartmentId = 2
        };
        repo.Update(updatePerson);
            
        var updatedPerson = context.People.Find(1);
        Assert.Equal("Jeff", updatedPerson.FirstName);
        Assert.Equal("Moore", updatedPerson.LastName);
        Assert.Equal("jeff.moore@gmail.com", updatedPerson.EmailAddress);
        Assert.Equal(DateOnly.FromDateTime(DateTime.Now.AddDays(1)), updatedPerson.DateOfBirth);
        Assert.Equal(2, updatedPerson.DepartmentId);
        
    }
    
    [Fact]
    public void PersonRepository_Add_Person()
    {
        var options = CreateNewContextOptions();

        using var context = new PersonManagerContext(options);
        var person = CreatePerson();
        context.People.Add(person);
        context.SaveChanges();
            
        var repo = new PersonRepository(context);
        var addPerson = new Data.Person
        {
            Id = 0, FirstName = "Katie", 
            LastName = "Jackson", 
            EmailAddress = "katie@gmail.com",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            DepartmentId = 3
        };
        repo.Add(addPerson);
            
        var addedPerson = context.People.Find(2);
        Assert.Equal(2, addedPerson.Id);
        Assert.Equal("Katie", addedPerson.FirstName);
        Assert.Equal("Jackson", addedPerson.LastName);
        Assert.Equal("katie@gmail.com", addedPerson.EmailAddress);
        Assert.Equal(DateOnly.FromDateTime(DateTime.Now), addedPerson.DateOfBirth);
        Assert.Equal(3, addedPerson.DepartmentId);
    }
}