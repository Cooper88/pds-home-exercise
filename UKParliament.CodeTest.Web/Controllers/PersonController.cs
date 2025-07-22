using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{

    IPersonService _personService;
    
    public PersonController(IPersonService personService)
    {
     _personService = personService;
    }
    
    [Route("{id:int}")]
    [HttpGet]
    public ActionResult<PersonViewModel> GetById(int id)
    {
 
        var person = _personService.Get(id);
        
        // PersonViewModel  personViewModel = new PersonViewModel();
        
        // Convert to view model
        // show department 
        
        return  person == null ? NotFound() : Ok(person);
    }
    
    [Route("all")]
    [HttpGet]
    public ActionResult<IEnumerable<Person>> GetAll()
    {
        return Ok(_personService.GetAll());
    }
    
    [Route("update")]
    [HttpPost]
    public ActionResult Update([FromBody] Person person)
    {
        _personService.Update(person);
        return Ok();
    }
}