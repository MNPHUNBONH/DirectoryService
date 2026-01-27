namespace DirectoryService.Contracts.Locations;

public record CreateLocationRequest(string LoactionsName, string LocationTimezone, IEnumerable<CreateAddressLocation> Addresses);