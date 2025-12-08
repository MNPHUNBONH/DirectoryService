using CSharpFunctionalExtensions;
using DirectoryService.Domain.Department.VO;

namespace DirectoryService.Domain;

public class DepartmentPosition
{
    private DepartmentPosition(DepartmentId departmentId, PositionId positionId)
    {
        Id = Guid.NewGuid();
        DepartmentId = departmentId;
        PositionId = positionId;
    }

    public Guid Id { get; private set; }

    public DepartmentId DepartmentId { get; private set; }

    public PositionId PositionId { get; private set; }

    public static Result<DepartmentPosition> Create(DepartmentId departmentId, PositionId positionId)
    {
        if (departmentId.Value == Guid.Empty || positionId.Value == Guid.Empty)
            return Result.Failure<DepartmentPosition>("Department Id and Position Id cannot be null");

        return new DepartmentPosition(departmentId, positionId);
    }
}