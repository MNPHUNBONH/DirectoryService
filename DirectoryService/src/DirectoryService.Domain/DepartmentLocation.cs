using CSharpFunctionalExtensions;
using DirectoryService.Domain.Departments;
using DirectoryService.Shared;

namespace DirectoryService.Domain;

public sealed class DepartmentLocation
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

    public static Result<DepartmentLocation, Error> Create(DepartmentId departmentId, LocationId locationId)
    {
        if (departmentId.Value == Guid.Empty || locationId.Value == Guid.Empty)
        {
            return GeneralErrors.ValueIsRequired("department.location");
        }

        return new DepartmentLocation(departmentId, locationId);
    }
}