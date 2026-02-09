using CSharpFunctionalExtensions;
using Shared;

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

    public static Result<LocationAddress, Error> Create(string city, string street, string houseNumber)
    {
        if (string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(street) || string.IsNullOrWhiteSpace(houseNumber))
            return GeneralErrors.ValueIsRequired("location.address");

        if(city.Length > MAX_LENGTH || street.Length > MAX_LENGTH || houseNumber.Length > MAX_LENGTH)
            return GeneralErrors.ValueIsInvalid("location.address");

        return new LocationAddress(city, street, houseNumber);
    }
}


