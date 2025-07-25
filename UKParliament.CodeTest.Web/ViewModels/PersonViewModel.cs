using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using UKParliament.CodeTest.Web.Validation;

namespace UKParliament.CodeTest.Web.ViewModels;

public class PersonViewModel
{
    [Required]
    public int Id { get; set; } 
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    [BirthDate]
    public string DateOfBirth { get; set; }
    
    [Required(ErrorMessage = "Department is required.")]
    public int DepartmentId { get; set; }
    
    [ValidateNever]
    public string DepartmentName { get; set; }
    
    [Required]
    [Email]
    public string EmailAddress { get; set; }
}