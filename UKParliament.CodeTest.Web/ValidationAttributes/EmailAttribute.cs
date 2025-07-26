using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UKParliament.CodeTest.Web.ValidationAttributes;

public class EmailAttribute : ValidationAttribute
{
    private static readonly Regex _emailRegex = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$", 
        RegexOptions.IgnoreCase
    );

    public EmailAttribute()
    {
        ErrorMessage = "Invalid email address.";
    }

    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true; // Required attribute takes care of this check.
        }
        
        var email = value.ToString();
        return _emailRegex.IsMatch(email);
    }
}
