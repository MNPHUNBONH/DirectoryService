namespace DirectoryService.Domain;

public record PositionId
{
    private PositionId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PositionId NewPositionId() => new(Guid.NewGuid());
    
    public static PositionId Create(Guid id) => new(id);
}