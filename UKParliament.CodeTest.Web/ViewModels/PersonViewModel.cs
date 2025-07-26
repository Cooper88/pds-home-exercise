using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using UKParliament.CodeTest.Web.ValidationAttributes;

namespace UKParliament.CodeTest.Web.ViewModels;

public class PersonViewModel
{
    [Required]
    public int Id { get; set; } 
    
    [Required(ErrorMessage = "First name is required.")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Last name is required.")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Date of Birth is required.")]
    [PresentOrPastDate]
    public string DateOfBirth { get; set; }
    
    [Required(ErrorMessage = "Department is required.")]
    [Department]
    public int DepartmentId { get; set; }
    
    [ValidateNever]
    public string DepartmentName { get; set; }
    
    [Required(ErrorMessage = "Email is required.")]
    [Email]
    public string EmailAddress { get; set; }
}