#if ANDROID
using EmrtdConnectorAndroid;
#endif

namespace EmrtdConnectorMaui;

public interface IPlatformService
{
#if ANDROID
    void NavigateToReader(string can);
    void NavigateToReader(string documentNumber, string dateOfBirth, string dateOfExpiry);
    event EventHandler<ReaderResultEventArgs> ReaderCompleted;
#elif IOS
    Task NavigateToReader(string can);
    Task NavigateToReader(string documentNumber, string dateOfBirth, string dateOfExpiry);
#endif
}

#if ANDROID
public class ReaderResultEventArgs : EventArgs
{
    public EmrtdPassport Result { get; set; }
    public bool Success { get; set; }
}
#endif
