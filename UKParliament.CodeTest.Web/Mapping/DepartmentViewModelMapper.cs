using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Mapping;

public static class DepartmentViewModelMapper
{
    private static DepartmentViewModel MapToDepartmentViewModel(Department department)
    {
        return new DepartmentViewModel()
        {
            Id = department.Id,
            Name = department.Name,
        };
    }
    
    public static List<DepartmentViewModel> MapToDepartmentViewModel(List<Department> departmentList)
    {
        List<DepartmentViewModel> list = [];
        list.AddRange(departmentList.Select(person => MapToDepartmentViewModel(person)));
        return list;
    }
}