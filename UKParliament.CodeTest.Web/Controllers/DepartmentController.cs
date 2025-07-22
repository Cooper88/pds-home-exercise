using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;

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
    public ActionResult<IEnumerable<Department>> GetAll()
    {
        return Ok(_departmentService.GetAll());
    }
    
}