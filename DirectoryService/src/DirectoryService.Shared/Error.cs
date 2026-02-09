namespace Shared;

public record ErrorMessage(string Code, string Message, string? InvalidField = null);
public record Error
{
    private const string SEPARATOR = "||";
    public string Code { get; }
    public string Message { get; }
    public string? InvalidField { get; }
    public ErrorType Type { get; }
    private Error(string code, string message, ErrorType type, string? invalidField = null)
    {
        Code = code;
        Message = message;
        Type = type;
        InvalidField = invalidField;
    }

    public static Error Validation(string code, string message, string? invalidField = null) => new (code, message, ErrorType.VALIDATION, invalidField);

    public static Error NotFound(string code, string message) => new (code, message, ErrorType.NOT_FOUND);

    public static Error Conflict(string code, string message) => new (code, message, ErrorType.CONFLICT);

    public static Error Failure(string code, string message) => new (code, message, ErrorType.FAILURE);

    public string Serialize() => string.Join(SEPARATOR,Code, Message, Type);

    public static Error Deserialize(string serialized)
    {
        var parts = serialized.Split(SEPARATOR);

        if (parts.Length < 3)
        {
            throw new ArgumentException("Invalid serialized from.");
        }

        if (Enum.TryParse<ErrorType>(parts[2], out var type) == false)
        {
            throw new ArgumentException("Invalid serialized from.");
        }

        return new Error(parts[0], parts[1], type);
    }
    
    public Errors ToError() => new([this]);
}

public enum ErrorType
{
    /// <summary>
    /// Ошибка валидации
    /// </summary>
    VALIDATION,

    /// <summary>
    /// Ошибка ничего не найдено
    /// </summary>
    NOT_FOUND,

    /// <summary>
    /// Ошибка сервера
    /// </summary>
    FAILURE,

    /// <summary>
    /// Ошибка конфликт
    /// </summary>
    CONFLICT,

    /// <summary>
    /// Ошибка авторизации
    /// </summary>
    AUTHORIZATION,

    /// <summary>
    /// Ошибка аутентификации
    /// </summary>
    AUTHENTICATION,
}