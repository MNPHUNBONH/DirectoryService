using CSharpFunctionalExtensions;
using DirectoryService.Shared;

namespace DirectoryService.Domain;

public sealed record LocationTimezone
{
    private LocationTimezone(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<LocationTimezone, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return GeneralErrors.ValueIsRequired("location.timezone");

        try
        {
            TimeZoneInfo.FindSystemTimeZoneById(value);
        }
        catch (TimeZoneNotFoundException)
        {
            return GeneralErrors.ValueIsInvalid("location.timezone");
        }

        return new LocationTimezone(value);
    }
}