using DirectoryService.Application.Exceptions;
using DirectoryService.Shared;

namespace DirectoryService.Application.Locations.Exceptions;

public class LocationNotFoundException : NotFoundException
{
    protected LocationNotFoundException(Error[] errors)
        : base(errors)
    {
    }
}