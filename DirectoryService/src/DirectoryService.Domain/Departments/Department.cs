using CSharpFunctionalExtensions;
using DirectoryService.Shared;

namespace DirectoryService.Domain.Departments;

public sealed class Department
{
    private readonly List<DepartmentLocation> _departmentLocations = [];

    private readonly List<DepartmentPosition> _departmentPositions = [];

    private readonly List<Department> _childDepartments = [];

    // EF Core
    public Department() { }

    public DepartmentId Id { get; private set; }

    public DepartmentName Name { get; private set; }

    public DepartmentIdentifier Identifier { get; private set; }

    public Path Path { get; private set; }


    public DepartmentId? ParentId { get; private set; }

    public short Depth { get; private set; }

    public int ChildrenCount { get; private set; }

    public bool IsActive { get; private set; }

    public IReadOnlyList<DepartmentLocation> Locations => _departmentLocations;

    public IReadOnlyList<DepartmentPosition> Positions => _departmentPositions;

    public IReadOnlyList<Department> ChildDepartments => _childDepartments;

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    private Department(
        DepartmentId id,
        DepartmentName name,
        DepartmentIdentifier identifier,
        Path path,
        short depth,
        IEnumerable<DepartmentLocation> locations)
    {
        Id = id;
        Name = name;
        Identifier = identifier;
        IsActive = true;
        Depth = depth;
        ChildrenCount = ChildDepartments.Count;
        _departmentLocations = locations.ToList();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public static Result<Department, Error> CreateParent(
        DepartmentName name,
        DepartmentIdentifier identifier,
        IEnumerable<DepartmentLocation> departmentLocations,
        DepartmentId? departmentId = null)
    {
        var departmentLocationList = departmentLocations.ToList();

        if (departmentLocationList.Count == 0)
            return Error.Validation("department.location","Department locations must contain at least one location");

        var path = Path.CreateParent(identifier);
        return new Department(departmentId ?? DepartmentId.NewDepartmentId(), name, identifier, path,0, departmentLocationList);
    }

    public static Result<Department, Error> CreateChild(
        DepartmentName name,
        DepartmentIdentifier identifier,
        Department paretn,
        IEnumerable<DepartmentLocation> departmentLocations,
        DepartmentId? departmentId = null)
    {
        var departmentLocationList = departmentLocations.ToList();

        if (departmentLocationList.Count == 0)
            return Error.Validation("department.location","Department locations must contain at least one location");

        var path = paretn.Path.CreateChild(identifier);
        return new Department(departmentId ?? DepartmentId.NewDepartmentId(), name, identifier, path,0, departmentLocationList);
    }
}