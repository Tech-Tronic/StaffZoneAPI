using StaffZone.Entities;
using StaffZone.Enums;

namespace StaffZone.Helpers;

public class PriceCalculator
{
	public static decimal CalcBookingPrice(DateTime checkOut, DateTime checkIn, RoomType type)
	{
		var numberOfNights = (checkOut - checkIn).Days;
		var pricePerNight = type switch
		{
			RoomType.Basic => 100m,
			RoomType.VIP => 250m,
			_ => 100m
		};

		var totalPrice = pricePerNight * numberOfNights;
		return totalPrice;
	}
}
