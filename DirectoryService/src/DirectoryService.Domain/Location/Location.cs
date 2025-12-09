using CSharpFunctionalExtensions;
using DirectoryService.Domain.VO;

namespace DirectoryService.Domain;

public sealed class Location
{
    private List<DepartmentLocation> _departments = [];
    private List<LocationAddress> _addresses = [];

    // EF Core
    public Location() { }

    private Location(
        LocationId? id,
        LocationName name,
        LocationTimezone timezone,
        IEnumerable<LocationAddress> address,
        IEnumerable<DepartmentLocation> departments)
    {
        Id = id ?? LocationId.NewLocationId();
        Name = name;
        Timezone = timezone;
        _addresses = address.ToList();
        _departments = departments.ToList();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public LocationId Id { get; private set; }

    public LocationName Name { get; private set; }

    public LocationTimezone Timezone { get; private set; }

    public IReadOnlyList<LocationAddress> Address => _addresses;

    public IReadOnlyList<DepartmentLocation> Departments => _departments;

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public static Result<Location> Create(
        LocationId? id,
        LocationName name,
        LocationTimezone timezone,
        IEnumerable<LocationAddress> addresses,
        IEnumerable<DepartmentLocation> departments)
    {
        return new Location(id, name, timezone, addresses, departments);
    }
}