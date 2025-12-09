namespace DirectoryService.Domain.VO;

public record LocationId
{
    private LocationId(Guid value)
    {
        Value = value ;
    }

    public Guid Value { get; }

    public static LocationId NewLocationId() => new(Guid.NewGuid());

    public static LocationId Create(Guid id) => new(id);
}