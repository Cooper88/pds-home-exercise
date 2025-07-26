namespace UKParliament.CodeTest.Data.Repositories.Department;
using Data;

public class DepartmentRepository : IDepartmentRepository
{
    
    private readonly PersonManagerContext _context;
    
    public DepartmentRepository(PersonManagerContext context)
    {
        _context = context;
    }

    public List<Department> GetAll()
    {
        return _context.Departments.OrderBy(i => i.Name).ToList();
    }
}