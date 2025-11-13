using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.Department.VO;

public record Identifier
{
    public const int MAX_LENGTH = 150;
    public const int MIN_LENGTH = 3;

    private static readonly Regex LatinRegex = new("^[a-zA-Z0-9]+$", RegexOptions.Compiled);

    private Identifier(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Identifier> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<Identifier>("Identifier cannot be null or empty.");

        if (value.Length < MIN_LENGTH || value.Length > MAX_LENGTH)
            return Result.Failure<Identifier>($"Identifier must be between {MIN_LENGTH} and {MAX_LENGTH} characters.");

        if (!LatinRegex.IsMatch(value))
            return Result.Failure<Identifier>("Identifier must contain only Latin letters and digits.");

        return new Identifier(value);
    }
}