namespace UKParliament.CodeTest.Data.Repositories.Person;

using Data;

public class PersonRepository : IPersonRepository
{
    private readonly PersonManagerContext _context;

    public PersonRepository(PersonManagerContext context)
    {
        _context = context;
    }

    public Person Get(int id)
    {
        return _context.People.Find(id)!;
    }

    public IEnumerable<Person> GetAll()
    {
        return _context.People.ToList();
    }

    public void Update(Person person)
    {
        var existing = _context.People.Find(person.Id);
        if (existing == null) return;
        // Update all the values 
        _context.Entry(existing).CurrentValues.SetValues(person);
        _context.SaveChanges();
    }

    public void Add(Person person)
    {
        // Ensure the person has a unique id
        var maxPersonId = _context.People.Max(i => i.Id);
        person.Id = maxPersonId + 1;
        
        _context.People.Add(person);
        
        _context.SaveChanges();
    }
}