using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using UKParliament.CodeTest.Web.ValidationAttributes;

namespace UKParliament.CodeTest.Web.ViewModels;

public class PersonViewModel
{
    [Required] public int Id { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [MaxLength(20, ErrorMessage = "First name must be 20 characters or less.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    [MaxLength(20, ErrorMessage = "Last name must be 20 characters or less.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Date of Birth is required.")]
    [PresentOrPastDate]
    public string DateOfBirth { get; set; }

    [Required(ErrorMessage = "Department is required.")]
    [Department]
    public int DepartmentId { get; set; }

    [ValidateNever] public string DepartmentName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [MaxLength(30, ErrorMessage = "Email must be 30 characters or less.")]
    [Email]
    public string EmailAddress { get; set; }
}