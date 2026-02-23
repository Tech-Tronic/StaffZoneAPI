using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace StaffZone.Helpers;

public class EgyptianPhoneAttribute : ValidationAttribute
{
	private const string EgyptianPhoneRegex = @"^01[0125]\d{8}$";

	public EgyptianPhoneAttribute()
	{
		ErrorMessage = "The {0} field must be a valid 11-digit Egyptian mobile number starting with 01.";
	}

	protected override ValidationResult IsValid(object value, ValidationContext validationContext)
	{
		if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
			return ValidationResult.Success;

		var phoneNumber = value.ToString();

		if (Regex.IsMatch(phoneNumber, EgyptianPhoneRegex))
			return ValidationResult.Success;

		return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
	}
}