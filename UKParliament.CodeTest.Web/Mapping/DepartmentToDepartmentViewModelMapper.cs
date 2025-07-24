using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Mapping;

public class DepartmentToDepartmentViewModelMapper
{
    public static DepartmentViewModel MapDepartmentToDepartmentViewModel(Department department)
    {
        return new DepartmentViewModel()
        {
            Id = department.Id,
            Name = department.Name,
        };
    }
    
    public static List<DepartmentViewModel> MapDepartmentToDepartmentViewModel(IEnumerable<Department> departmentList)
    {
        List<DepartmentViewModel> list = [];
        list.AddRange(departmentList.Select(person => MapDepartmentToDepartmentViewModel(person)));
        return list;
    }
}