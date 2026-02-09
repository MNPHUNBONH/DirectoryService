using CSharpFunctionalExtensions;
using DirectoryService.Application.Locations;
using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace DirectoryService.Infrastructure.Repositories;

public class EfCoreLocationsRepository : ILocationsRepository
{
    private readonly DirectoryServiceDbContext _dbContext;
    private readonly ILogger<EfCoreLocationsRepository> _logger;

    public EfCoreLocationsRepository(DirectoryServiceDbContext dbContext, ILogger<EfCoreLocationsRepository> logger )
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> AddAsync(Location location, CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbContext.Locations.AddAsync(location, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return location.Id.Value;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            return Error.Failure("failed.to.insert.location", e.Message);
        }
    }
}