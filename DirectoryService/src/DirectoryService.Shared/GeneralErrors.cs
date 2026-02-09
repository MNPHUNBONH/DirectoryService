namespace DirectoryService.Shared;

public static class GeneralErrors
{
    public static Error ValueIsInvalid(string? name = null)
    {
        var label = name ?? "значение";
        return Error.Validation("value.is.invalid", $"{label} is not valid", label);
    }

    public static Error NotFound(Guid? id = null, string? name = null)
    {
        var forId = id == null ? string.Empty : $" no Id '{id}'";
        return Error.NotFound("record.not.found", $"{name ?? "record"} is not found{forId}");
    }

    public static Error ValueIsRequired(string? name = null)
    {
        var label = name == null ? string.Empty : " " + name + " ";
        return Error.Validation("length.is.invalid", $"Field{label}is required", label);
    }

    public static Error AlreadyExist()
    {
        return Error.Conflict("record.already.exist", $"record already exist");
    }

    public static Error Failur(string? message = null)
    {
        return Error.Failure("server.failure", message ?? "server failure");
    }

}