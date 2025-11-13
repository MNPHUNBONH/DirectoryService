using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.VO;

public record LocationTimezone
{
    private LocationTimezone(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<LocationTimezone> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<LocationTimezone>("Timezone ID cannot be empty.");

        try
        {
            TimeZoneInfo.FindSystemTimeZoneById(value);
        }
        catch (TimeZoneNotFoundException)
        {
            return Result.Failure<LocationTimezone>($"'{value}' не является валидным IANA timezone ID.");
        }

        return new LocationTimezone(value);
    }
}