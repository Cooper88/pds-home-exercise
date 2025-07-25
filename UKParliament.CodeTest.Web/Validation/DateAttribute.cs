namespace UKParliament.CodeTest.Web.Validation;
 
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class DateAttribute : ValidationAttribute
{
    private static readonly Regex _dateRegex = new Regex(
        @"^\d{2}/\d{2}/\d{4}$"
    );

    public override bool IsValid(object value)
    {
        if (value == null)
        {
            return true;
        }
        
        var date = value.ToString();

        if (!_dateRegex.IsMatch(date))
        {
            ErrorMessage = "Invalid format. Accepted format is DD/MM/YYYY.";
            return false;
        }
        
        ErrorMessage = "This date is invalid.";
        
        return DateOnly.TryParse(date, out _);
    }
}
