using CSharpFunctionalExtensions;
using DirectoryService.Domain;

namespace DirectoryService.Application.Locations;

public interface ILocationsRepository
{
    Task<Result<Guid, string>> AddAsync(Location location, CancellationToken cancellationToken = default);
}