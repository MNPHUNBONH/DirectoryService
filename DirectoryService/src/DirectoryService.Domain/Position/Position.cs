using CSharpFunctionalExtensions;

namespace DirectoryService.Domain;

public class Position
{
    private Position(
        Guid id,
        PositionName name,
        string? description)
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public Guid Id { get; private set; }

    public PositionName Name { get; private set; }

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public static Result<Position> Create(Guid id, PositionName name, string? description)
    {
        if (description != null && description.Length > 1000)
            return Result.Failure<Position>($"Position {name} is too long");

        return new Position(id, name, description);
    }

}