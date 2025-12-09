using CSharpFunctionalExtensions;
using DirectoryService.Domain.Department.VO;
using DirectoryService.Domain.VO;

namespace DirectoryService.Domain;

public class DepartmentLocation
{
    private DepartmentLocation(DepartmentId departmentId, LocationId locationId)
    {
        Id = Guid.NewGuid();
        DepartmentId = departmentId;
        LocationId = locationId;
    }

    public Guid Id { get; private set; }

    public DepartmentId DepartmentId { get; private set; }

    public LocationId LocationId { get; private set; }

    public static Result<DepartmentLocation> Create(DepartmentId departmentId, LocationId locationId)
    {
        if (departmentId.Value == Guid.Empty || locationId.Value == Guid.Empty)
            return Result.Failure<DepartmentLocation>("Department Id and Location Id cannot be null");

        return new DepartmentLocation(departmentId, locationId);
    }
}