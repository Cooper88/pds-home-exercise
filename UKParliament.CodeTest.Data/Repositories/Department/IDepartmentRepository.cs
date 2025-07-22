namespace UKParliament.CodeTest.Data.Repositories.Department;
using Data;

public interface IDepartmentRepository
{
    IEnumerable<Department> GetAll();
}