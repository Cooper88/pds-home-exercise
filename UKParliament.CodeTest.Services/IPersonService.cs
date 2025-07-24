using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Services;

public interface IPersonService
{
    Person Get(int id);

    IEnumerable<Person> GetAll();

    void Update(Person person);
    void Add(Person person);
}