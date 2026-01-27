using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using DirectoryService.Contracts.Locations;
using DirectoryService.Domain;
using DirectoryService.Domain.VO;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Application.Locations;

public class CreateLocationHandler
{
    private readonly ILocationsRepository _locationsRepository;
    private readonly ILogger<CreateLocationHandler> _logger;

    public CreateLocationHandler(ILocationsRepository locationsRepository, ILogger<CreateLocationHandler> logger)
    {
        _locationsRepository = locationsRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, string>> Handle(CreateLocationRequest createLocationRequest, CancellationToken cancellationToken)
    {
        // проверка валдинойсти
        var locationId = LocationId.NewLocationId().Value;
        var locationName = LocationName.Create(createLocationRequest.LoactionsName);
        var locationTimezone = LocationTimezone.Create(createLocationRequest.LocationTimezone);
        var locationAddresses = new List<LocationAddress>();

        // var locationAddress = locationDTO.Addresses
        //     .Select(address => LocationAddress.Create(address.City,address.Street,address.HouseNumber));
        foreach (var locationAddress in createLocationRequest.Addresses)
        {
            var addres = LocationAddress.Create(locationAddress.City, locationAddress.Street,
                locationAddress.HouseNumber);
            if (addres.IsFailure)
                return addres.Error;
            locationAddresses.Add(addres.Value);
        }

        // создание сущности Location
        var location = Location.Create(LocationId.Create(locationId), locationName.Value, locationTimezone.Value, locationAddresses);

        if (location.IsFailure)
       {
           return location.Error;
       }

        // сохранине сущности в БД
        await _locationsRepository.AddAsync(location.Value, cancellationToken);

        // логирование об успешноим или не успешном сохранении
        _logger.LogInformation("Locations created with id {locationId}", locationId);
 
        return locationId;
    }
}