using CSharpFunctionalExtensions;
using DirectoryService.Domain.VO;
using Shared;

namespace DirectoryService.Domain;

public sealed class Location
{
    private List<DepartmentLocation> _departments = [];

    // EF Core
    public Location() { }

    private Location(
        LocationId? id,
        LocationName name,
        LocationTimezone timezone,
        LocationAddress address)
    {
        Id = id ?? LocationId.NewLocationId();
        Name = name;
        Timezone = timezone;
        Address = address;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public LocationId Id { get; private set; }

    public LocationName Name { get; private set; }

    public LocationTimezone Timezone { get; private set; }

    public LocationAddress Address { get; private set; }

    public IReadOnlyList<DepartmentLocation> Departments => _departments;

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public static Result<Location, Error> Create(
        LocationId? id,
        LocationName name,
        LocationTimezone timezone,
        LocationAddress addresses)
    {
        return new Location(id, name, timezone, addresses);
    }
}