using DirectoryService.Application.Exceptions;
using DirectoryService.Shared;

namespace DirectoryService.Application.Locations.Exceptions;

public class LocationValidationException : BadRequestException
{
    protected LocationValidationException(Error[] errors)
        : base(errors)
    {
    }
}