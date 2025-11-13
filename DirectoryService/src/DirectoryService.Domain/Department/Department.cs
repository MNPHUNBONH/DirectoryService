using CSharpFunctionalExtensions;
using DirectoryService.Domain.Department.VO;
using Path = DirectoryService.Domain.Department.VO.Path;

namespace DirectoryService.Domain.Department;

public class Department
{
    private List<DepartmentLocation> _departmentLocations = [];

    private List<DepartmentPosition> _departmentPositions = [];

    private List<Department> _childDepartments = [];

    private Department(
        Guid id,
        DepartmentName departmentName,
        Identifier identifier,
        Path path,
        Guid? parentId,
        short depth,
        IEnumerable<DepartmentLocation> locations,
        IEnumerable<DepartmentPosition> positions,
        IEnumerable<Department> childDepartments)
    {
        Id = id;
        Name = departmentName;
        Identifier = identifier;
        Path = path;
        ParentId = parentId;
        Depth = depth;
        _departmentLocations = locations.ToList();
        _departmentPositions = positions.ToList();
        _childDepartments = childDepartments.ToList();
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }

    public DepartmentName Name { get; private set; }

    public Identifier Identifier { get; private set; }

    public Path Path { get; private set; }


    public Guid? ParentId { get; private set; }

    public short Depth { get; private set; }

    public bool IsActive { get; private set; }

    public IReadOnlyList<DepartmentLocation> Locations => _departmentLocations;

    public IReadOnlyList<DepartmentPosition> Positions => _departmentPositions;

    public IReadOnlyList<Department> ChildDepartments => _childDepartments;

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public static Result<Department> Create(
        Guid id,
        DepartmentName name,
        Identifier identifier,
        Path path,
        Guid? parentId,
        short depth,
        IEnumerable<DepartmentLocation> locations,
        IEnumerable<DepartmentPosition> positions,
        IEnumerable<Department> childDepartments)
    {
        return new Department(
            id,
            name,
            identifier,
            path,
            parentId,
            depth,
            locations,
            positions,
            childDepartments);
    }
}