using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.Mapping;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;
    
    public PersonController(IPersonService personService)
    {
     _personService = personService;
    }
    
    [Route("{id:int}")]
    [HttpGet]
    public ActionResult<PersonViewModel> GetById(int id)
    {
 
        var person = _personService.Get(id);

        if (person == null) return NotFound();
        
        var mappedPerson = PersonToPersonViewModelMapper.MapPersonToPersonViewModel(person);
      
        return Ok(mappedPerson);
    }
    
    [Route("all")]
    [HttpGet]
    public ActionResult<List<PersonViewModel>> GetAll()
    {
        var personList = _personService.GetAll();
        var  mappedPerson = PersonToPersonViewModelMapper.MapPersonToPersonViewModel(personList);
        return Ok(mappedPerson);
    }
    
    [Route("update")]
    [HttpPut]
    public ActionResult Update([FromBody] PersonViewModel personViewModel)
    {
        var person = PersonViewModelToPersonMapper.MapPersonViewModelToPerson(personViewModel);
        _personService.Update(person);
        return Ok();
    }
    
    [Route("add")]
    [HttpPost]
    public ActionResult Add([FromBody] PersonViewModel personViewModel)
    {
        var person = PersonViewModelToPersonMapper.MapPersonViewModelToPerson(personViewModel);
        _personService.Add(person);
        return Ok();
    }
}