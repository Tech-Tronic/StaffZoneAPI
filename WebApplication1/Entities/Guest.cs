using System.ComponentModel.DataAnnotations;
using StaffZone.Helpers;
namespace StaffZone.Entities;

public class Guest
{
	public int Id { get; set; }

	[Required(ErrorMessage = "First name is strongly required.")]
	[StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
	public string? FirstName { get; set; }

	[Required(ErrorMessage = "Last name is required.")]
	[StringLength(50, MinimumLength = 2)]
	public string? LastName { get; set; }

	[Required(ErrorMessage = "Email address is required.")]
	[EmailAddress(ErrorMessage = "Please provide a valid email format.")]
	public string? Email { get; set; }

	[Required(ErrorMessage = "Phone number is required")]
	[EgyptianPhone(ErrorMessage = "Invalid phone number format")]
	public string? PhoneNumber { get; set; }

	public int VisitCount { get; set; }
}
