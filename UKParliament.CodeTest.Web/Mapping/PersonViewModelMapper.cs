using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Mapping;

public static class PersonViewModelMapper
{
    public static PersonViewModel MapToPersonViewModel(Person person)
    {
        return new PersonViewModel()
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            DateOfBirth = person.DateOfBirth.ToString("dd/MM/yyyy"),
            DepartmentId = person.DepartmentId,
            EmailAddress = person.EmailAddress,
        };
    }

    private static PersonViewModel MapToPersonViewModel(Person person, List<Department> departmentList)
    {
        return new PersonViewModel()
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            DateOfBirth = person.DateOfBirth.ToString("dd/MM/yyyy"),
            DepartmentId = person.DepartmentId,
            EmailAddress = person.EmailAddress,
            DepartmentName = departmentList.First(i => i.Id == person.DepartmentId).Name,
        };
    }

    public static List<PersonViewModel> MapToPersonViewModel(
        IEnumerable<Person> personList,
        List<Department> departmentList)
    {
        List<PersonViewModel> list = [];
        list.AddRange(personList.Select(person => MapToPersonViewModel(person, departmentList)));
        return list;
    }
}