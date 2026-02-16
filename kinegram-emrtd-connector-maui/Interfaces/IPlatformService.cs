namespace EmrtdConnectorMaui;

public interface IPlatformService
{
    Task<ValidationResult?> NavigateToReaderAsync(string can);
    Task<ValidationResult?> NavigateToReaderAsync(string documentNumber, string dateOfBirth, string dateOfExpiry);
}
