using System.ComponentModel.DataAnnotations;

namespace MovieManagementAPI.CustomValidators
{
    public class ValidateReleaseYearAttribute: ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }
            if (value is int releaseYear)
            {
                return releaseYear >= 1000 && releaseYear <= DateTime.UtcNow.Year;
            }
            return false;
        }
    }
}
