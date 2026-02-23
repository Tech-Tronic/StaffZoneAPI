namespace StaffZone.Services.Contracts;

public interface IEmailService
{
	Task SendBookingConfirmationAsync(string email, int bookingId);
}
