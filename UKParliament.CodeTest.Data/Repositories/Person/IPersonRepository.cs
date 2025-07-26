namespace UKParliament.CodeTest.Data.Repositories.Person;
using Data;

public interface IPersonRepository
{
    Person? Get(int id);
    List<Person> GetAll();
    void Update(Person person);
    void Add(Person person);
}