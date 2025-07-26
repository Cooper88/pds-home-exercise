using System.ComponentModel.DataAnnotations;
using UKParliament.CodeTest.Services;

namespace UKParliament.CodeTest.Web.ValidationAttributes;

public class DepartmentAttribute : ValidationAttribute
{
    public DepartmentAttribute()
    {
        ErrorMessage = "A valid DepartmentId is required.";
    }
    
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var departmentId = Convert.ToInt32(value);
        var departmentService = validationContext.GetService(typeof(IDepartmentService)) as IDepartmentService;
        
        var departmentList = departmentService!.GetAll().ToList();
        
        return departmentList.Any(i => i.Id == departmentId) ?  
            ValidationResult.Success : new ValidationResult(ErrorMessage);
    }
}