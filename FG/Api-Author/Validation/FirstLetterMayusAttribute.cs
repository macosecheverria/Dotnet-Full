using System.ComponentModel.DataAnnotations;

namespace Api_Author.Validation;

public class FirstLetterMayusAttribute : ValidationAttribute {
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {   
        if(value == null || string.IsNullOrEmpty(value.ToString())){
            return ValidationResult.Success;
        }

        var firstLetter = value.ToString()![0].ToString();

        if(firstLetter != firstLetter.ToUpper()){
            return new ValidationResult("La primera letra debe de ser mayuscula");
        }

        return ValidationResult.Success;

    }
}