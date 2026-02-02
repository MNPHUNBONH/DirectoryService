using CSharpFunctionalExtensions;
using DirectoryService.Contracts.Locations;
using DirectoryService.Domain;
using DirectoryService.Domain.VO;
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

    public async Task<Result<Guid,string>> Create(CreateLocationRequest createLocationRequest, CancellationToken cancellationToken)
    {
        // проверка валдинойсти
        var locationId = LocationId.NewLocationId().Value;
        var locationName = LocationName.Create(createLocationRequest.LoactionsName);
        var locationTimezone = LocationTimezone.Create(createLocationRequest.LocationTimezone);
        var locationAddresses = LocationAddress.Create(
            createLocationRequest.Addresse.City,
            createLocationRequest.Addresse.Street,
            createLocationRequest.Addresse.HouseNumber);

        // создание сущности Location
        var location = Location.Create(LocationId.Create(locationId), locationName.Value, locationTimezone.Value, locationAddresses.Value);

        if (location.IsFailure)
        {
            return location.Error;
        }

        // сохранине сущности в БД
        _locationsRepository.AddAsync(location.Value);

        _logger.LogInformation("Locations created with id {locationId}", locationId);

        return locationId;
    }
}