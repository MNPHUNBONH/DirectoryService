namespace DirectoryService.Domain.Departments;

public sealed record Path
{
    private const char Separator = '/';
    private Path(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Path CreateParent(DepartmentIdentifier identifier)
        => new Path(identifier.Value);
    public Path CreateChild(DepartmentIdentifier childeIdentifier)
        => new Path(Value + Separator + childeIdentifier.Value);
}