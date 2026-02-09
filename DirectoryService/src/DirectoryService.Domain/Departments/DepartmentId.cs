namespace DirectoryService.Domain.Departments;

public sealed record DepartmentId
{
    private DepartmentId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static DepartmentId NewDepartmentId() => new(Guid.NewGuid());

    public static DepartmentId Create(Guid id) => new(id);
}