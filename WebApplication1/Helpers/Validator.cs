namespace StaffZone.Helpers;

public static class Validator
{
	public static bool IsValidRoomCount(int roomCount)
	{
		return roomCount + 1 <= Constant.MaxRoomCount;
	}

	public static bool IsValidType(int type)
	{
		return type >= Constant.MinRoomType && type <= Constant.MaxRoomType;
	}

	public static bool IsValidSize(int size)
	{
		return size >= Constant.MinRoomSize && size <= Constant.MaxRoomSize;
	}

	public static bool IsValidState(int state)
	{
		return state >= Constant.MinRoomState && state <= Constant.MaxRoomState;
	}

	public static bool HasNullInfo(string? phoneNumber, string? email)
	{
		bool nullPhoneNumber = string.IsNullOrEmpty(phoneNumber);
		bool nullEmail = string.IsNullOrEmpty(email);
		return nullPhoneNumber && nullEmail;
	}

	public static bool IsValidId(int id)
	{
		return id > 0;
	}

	public static bool IsValidEmail(string? email)
	{
		return !string.IsNullOrWhiteSpace(email);
	}

	public static bool IsValidSearchString(string? searchTerm)
	{
		return !string.IsNullOrWhiteSpace(searchTerm);
	}
}
