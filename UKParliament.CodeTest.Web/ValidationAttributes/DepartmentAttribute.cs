using System.ComponentModel.DataAnnotations;
using UKParliament.CodeTest.Services;

namespace UKParliament.CodeTest.Web.ValidationAttributes;

/// <summary>
/// To be used to check whether the departmentId passed in is a valid department. 
/// </summary>
public class DepartmentAttribute : ValidationAttribute
{
    public DepartmentAttribute()
    {
        ErrorMessage = "A valid Department is required.";
    }
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success; // Required attribute takes care of this check.
        }
        
        var departmentId = Convert.ToInt32(value);
        var departmentService = validationContext.GetService(typeof(IDepartmentService)) as IDepartmentService;
        
        var departmentList = departmentService!.GetAll().ToList();
        
        return departmentList.Any(i => i.Id == departmentId) ?  
            ValidationResult.Success : new ValidationResult(ErrorMessage);
    }
}