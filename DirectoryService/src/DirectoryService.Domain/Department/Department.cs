using CSharpFunctionalExtensions;
using DirectoryService.Domain.Department.VO;
using Shared;
using Path = DirectoryService.Domain.Department.VO.Path;

namespace DirectoryService.Domain.Department;

public sealed class Department
{
    private List<DepartmentLocation> _departmentLocations = [];

    private List<DepartmentPosition> _departmentPositions = [];

    private List<Department> _childDepartments = [];

    // EF Core
    public Department() {}

    private Department(
        DepartmentId? id,
        DepartmentName departmentName,
        DepartmentIdentifier departmentIdentifier,
        Path path,
        DepartmentId? parentId,
        short depth,
        IEnumerable<DepartmentLocation> locations,
        IEnumerable<DepartmentPosition> positions,
        IEnumerable<Department> childDepartments)
    {
        Id = id ?? DepartmentId.NewDepartmentId();
        Name = departmentName;
        DepartmentIdentifier = departmentIdentifier;
        Path = path;
        ParentId = parentId;
        Depth = depth;
        _departmentLocations = locations.ToList();
        _departmentPositions = positions.ToList();
        _childDepartments = childDepartments.ToList();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public DepartmentId Id { get; private set; }

    public DepartmentName Name { get; private set; }

    public DepartmentIdentifier DepartmentIdentifier { get; private set; }

    public Path Path { get; private set; }


    public DepartmentId? ParentId { get; private set; }

    public short Depth { get; private set; }

    public bool IsActive { get; private set; }

    public IReadOnlyList<DepartmentLocation> Locations => _departmentLocations;

    public IReadOnlyList<DepartmentPosition> Positions => _departmentPositions;

    public IReadOnlyList<Department> ChildDepartments => _childDepartments;

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public static Result<Department, Error> Create(
        DepartmentId id,
        DepartmentName name,
        DepartmentIdentifier departmentIdentifier,
        Path path,
        DepartmentId? parentId,
        short depth,
        IEnumerable<DepartmentLocation> locations,
        IEnumerable<DepartmentPosition> positions,
        IEnumerable<Department> childDepartments)
    {
        return new Department(
            id,
            name,
            departmentIdentifier,
            path,
            parentId,
            depth,
            locations,
            positions,
            childDepartments);
    }
}