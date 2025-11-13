using CSharpFunctionalExtensions;

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

    public static Result<PositionName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<PositionName>("PositionName cannot be null or empty.");

        if (value.Length > MAX_LENGTH || value.Length < MIN_LENGTH)
            return Result.Failure<PositionName>($"PositionName must be between {MIN_LENGTH} and {MAX_LENGTH} characters.");

        return new PositionName(value);
    }
}