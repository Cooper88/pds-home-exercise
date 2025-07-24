using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Mapping;

public static class PersonToPersonViewModelMapper
{

    public static PersonViewModel MapPersonToPersonViewModel(Person person)
    {
        return new PersonViewModel()
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            DateOfBirth = person.DateOfBirth,
            DepartmentId = person.DepartmentId,
            EmailAddress = person.EmailAddress
        };
    }
    
    public static List<PersonViewModel> MapPersonToPersonViewModel(IEnumerable<Person> personList)
    {
        List<PersonViewModel> list = [];
        list.AddRange(personList.Select(person => MapPersonToPersonViewModel(person)));
        return list;
    }
    
    
}