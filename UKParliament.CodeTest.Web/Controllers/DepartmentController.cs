using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.Mapping;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Controllers;


[ApiController]
[Route("api/[controller]")]
public class DepartmentController  : ControllerBase
{
    IDepartmentService _departmentService;
    
    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }
    
    [Route("all")]
    [HttpGet]
    public ActionResult<List<DepartmentViewModel>> GetAll()
    {
        var departments = _departmentService.GetAll();
        var mappedDepartments = DepartmentToDepartmentViewModelMapper.MapDepartmentToDepartmentViewModel(departments);
        return Ok(mappedDepartments);
    }
    
}