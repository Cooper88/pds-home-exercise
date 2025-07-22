using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Repositories.Department;

namespace UKParliament.CodeTest.Services;

public class DepartmentService : IDepartmentService
{
    IDepartmentRepository _departmentRepository;
    public DepartmentService(IDepartmentRepository  departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public IEnumerable<Department> GetAll()
    {
        return _departmentRepository.GetAll();
    }
}