namespace StaffZone.Helpers;

public class RoomNumberCalculator
{
	public static int CalculateRoomNumber(int floorNumber, int roomsCount)
	{
		return floorNumber + roomsCount + 1;
	}
}
