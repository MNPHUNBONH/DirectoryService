using CSharpFunctionalExtensions;
using Shared;

namespace DirectoryService.Domain;

public record PositionName
{
    public const int MAX_LENGTH = 100;
    public const int MIN_LENGTH = 3;

    private PositionName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<PositionName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
           return GeneralErrors.ValueIsRequired("position.name");

        if (value.Length > MAX_LENGTH || value.Length < MIN_LENGTH)
           return GeneralErrors.ValueIsInvalid("position.name");

        return new PositionName(value);
    }
}