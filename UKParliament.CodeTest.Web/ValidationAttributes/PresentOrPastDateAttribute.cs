using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UKParliament.CodeTest.Web.ValidationAttributes;

/// <summary>
/// To be used to check if the value passed in is a valid UK date either for today or in the past. 
/// </summary>
public class PresentOrPastDateAttribute : ValidationAttribute
{
    private static readonly Regex _dateRegex = new Regex(
        @"^\d{2}/\d{2}/\d{4}$"
    );

    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true; // Required attribute takes care of this check.
        }
        
        var date = value.ToString();

        // Check if format matches regex
        if (!_dateRegex.IsMatch(date))
        {
            ErrorMessage = "Invalid format. Accepted format is DD/MM/YYYY.";
            return false;
        }

        // Check is a valid date
        if (!DateOnly.TryParse(date, out DateOnly result))
        {
            ErrorMessage = "This date is invalid.";
            return false;
        }

        // Check if date is in the future
        if (result > DateOnly.FromDateTime(DateTime.Now))
        {
            ErrorMessage = "This date is in the future.";
            return false;
        }

        return true;
    }
}
