using CSharpFunctionalExtensions;
using DirectoryService.Domain;

namespace DirectoryService.Application.Locations;

public interface ILocationsRepository
{
    public Task<Result<Guid, string>> AddAsync(Location location, CancellationToken cancellationToken = default);
}