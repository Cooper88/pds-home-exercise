using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Mapping;
 

public static class PersonViewModelToPersonMapper
{

    public static Person MapPersonViewModelToPerson(PersonViewModel person)
    {
        return new Person()
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            DateOfBirth = person.DateOfBirth,
            DepartmentId = person.DepartmentId,
            EmailAddress = person.EmailAddress
        };
    }
}