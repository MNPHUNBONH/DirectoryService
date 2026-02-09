using CSharpFunctionalExtensions;
using DirectoryService.Contracts.Locations;
using DirectoryService.Domain;
using DirectoryService.Shared;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Application.Locations;

public class LocationsService
{
    private readonly ILocationsRepository _locationsRepository;
    private readonly ILogger<LocationsService> _logger;

    public LocationsService(ILocationsRepository locationsRepository, ILogger<LocationsService> logger)
    {
        _locationsRepository = locationsRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Errors>> Create(CreateLocationRequest createLocationRequest, CancellationToken cancellationToken)
    {
        // проверка валдинойсти
        var locationId = LocationId.NewLocationId().Value;

        var locationName = LocationName.Create(createLocationRequest.LoactionsName);
        if (locationName.IsFailure)
            return locationName.Error.ToError();

        var locationTimezone = LocationTimezone.Create(createLocationRequest.LocationTimezone);
        if (locationTimezone.IsFailure)
            return locationTimezone.Error.ToError();

        var locationAddresses = LocationAddress.Create(
            createLocationRequest.Addresse.City,
            createLocationRequest.Addresse.Street,
            createLocationRequest.Addresse.HouseNumber);
        if (locationAddresses.IsFailure)
            return locationAddresses.Error.ToError();

        // создание сущности Location
        var locationResult = Location.Create(LocationId.Create(locationId), locationName.Value, locationTimezone.Value, locationAddresses.Value);

        if (locationResult.IsFailure)
            return locationResult.Error.ToError();

        // сохранине сущности в БД
        _locationsRepository.AddAsync(locationResult.Value);

        _logger.LogInformation("Locations created with id {locationId}", locationId);

        return locationId;
    }
}