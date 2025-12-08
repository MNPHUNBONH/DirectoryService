using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.VO;

public record LocationAddress
{
    public const int MAX_LENGTH = 150;

    public string City { get; }
    public string Street { get; }
    public string HouseNumber { get; }

    private LocationAddress(string city, string street, string houseNumber)
    {
        City = city;
        Street = street;
        HouseNumber = houseNumber;
    }

    public static Result<LocationAddress> Create(string city, string street, string houseNumber)
    {
        if (string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(street) || string.IsNullOrWhiteSpace(houseNumber))
            return Result.Failure<LocationAddress>("Address cannot be null or empty.");

        return new LocationAddress(city, street, houseNumber);
    }
}


