using CSharpFunctionalExtensions;
using Shared;

namespace DirectoryService.Domain;

public sealed class Position
{
    // EF Core
    public Position() { }

    private Position(
        PositionId? id,
        PositionName name,
        string? description)
    {
        Id = id ?? PositionId.NewPositionId();
        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public PositionId Id { get; private set; }

    public PositionName Name { get; private set; }

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public static Result<Position, Error> Create(PositionId? id, PositionName name, string? description)
    {
        if (description != null)
            return GeneralErrors.ValueIsInvalid("position.description");

        return new Position(id, name, description);
    }

}