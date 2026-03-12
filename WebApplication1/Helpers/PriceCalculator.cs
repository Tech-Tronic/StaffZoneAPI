using StaffZone.Enums;

namespace StaffZone.Helpers;

public class PriceCalculator
{
	public static decimal CalcBookingPrice(DateTime checkIn, DateTime checkOut, RoomType type, RoomSize size)
	{
		var numberOfNights = (checkOut - checkIn).Days;

		decimal typePrice = type switch
		{
			RoomType.Basic => Constant.BasicPrice,
			RoomType.VIP => Constant.VIPPrice,
			_ => Constant.BasicPrice
		};

		decimal sizePrice = size switch
		{
			RoomSize.Single => Constant.SinglePrice,
			RoomSize.Double => Constant.DoublePrice,
			RoomSize.Triple => Constant.TriplePrice,
			RoomSize.Family => Constant.FamilyPrice,
			_ => Constant.SinglePrice
		};

		return typePrice * sizePrice * numberOfNights;
	}
}
