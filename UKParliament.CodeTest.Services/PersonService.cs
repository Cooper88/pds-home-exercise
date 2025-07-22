using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Repositories.Person;

namespace UKParliament.CodeTest.Services;

public class PersonService : IPersonService
{

    IPersonRepository _personRepository;
    public PersonService(IPersonRepository  personRepository)
    {
        _personRepository = personRepository;
    }

    public Person Get(int id)
    {
        return _personRepository.Get(id);
    }

    public IEnumerable<Person> GetAll()
    {
        return _personRepository.GetAll();
    }

    public void Update(Person person)
    {
        _personRepository.Update(person);
    }
    
}