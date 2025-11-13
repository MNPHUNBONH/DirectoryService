using CSharpFunctionalExtensions;
using DirectoryService.Domain.VO;

namespace DirectoryService.Domain;

public class Location
{
    private List<DepartmentLocation> _departments = [];
    private List<LocationAddress> _addresses = [];

    private Location(
        Guid id,
        LocationName name,
        LocationTimezone timezone,
        IEnumerable<LocationAddress> address,
        IEnumerable<DepartmentLocation> departments)
    {
        Id = id;
        Name = name;
        Timezone = timezone;
        _addresses = address.ToList();
        _departments = departments.ToList();
    }

    public Guid Id { get; private set; }

    public LocationName Name { get; private set; }

    public LocationTimezone Timezone { get; private set; }

    public IReadOnlyList<LocationAddress> Address => _addresses;

    public IReadOnlyList<DepartmentLocation> Departments => _departments;

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public static Result<Location> Create(
        Guid id,
        LocationName name,
        LocationTimezone timezone,
        IEnumerable<LocationAddress> addresses,
        IEnumerable<DepartmentLocation> departments)
    {
        return new Location(id, name, timezone, addresses, departments);
    }
}