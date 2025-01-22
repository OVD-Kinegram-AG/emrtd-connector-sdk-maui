namespace EmrtdConnectorMaui;

// .NET requires the implementation of the interface members
// although the interface is not being used on iOS

public class PlatformService : IPlatformService
{
    public Task NavigateToReader(string can)
    {
        throw new NotImplementedException();
    }

    public Task NavigateToReader(string documentNumber, string dateOfBirth, string dateOfExpiry)
    {
        throw new NotImplementedException();
    }
}