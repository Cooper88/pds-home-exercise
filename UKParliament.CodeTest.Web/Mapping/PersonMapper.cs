using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Mapping;
 

public static class PersonMapper
{

    public static Person MapToPerson(PersonViewModel person)
    {
        return new Person()
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            DateOfBirth = DateOnly.Parse(person.DateOfBirth),
            DepartmentId = person.DepartmentId,
            EmailAddress = person.EmailAddress
        };
    }
}