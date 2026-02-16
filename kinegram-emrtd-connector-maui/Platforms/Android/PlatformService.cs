using Android.App;
using Android.Content;
using Android.Widget;

using EmrtdConnectorAndroid;
using EmrtdConnectorMaui.Common;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EmrtdConnectorMaui;

public class PlatformService : IPlatformService
{
    private const int RequestCode = 1701;
    private const string RETURN_DATA = "DATA";
    private const string RETURN_ERROR = "ERROR";

    private static PlatformService? _instance;
    private TaskCompletionSource<ValidationResult?>? _taskCompletionSource;

    public static PlatformService Instance => _instance ??= new PlatformService();

    // Event behalten für Rückwärtskompatibilität (optional)
    public event EventHandler<ReaderResultEventArgs>? ReaderCompleted;

    public Task<ValidationResult?> NavigateToReaderAsync(string can)
    {
        _taskCompletionSource = new TaskCompletionSource<ValidationResult?>();
        NavigateToReader(can);
        return _taskCompletionSource.Task;
    }

    public Task<ValidationResult?> NavigateToReaderAsync(string documentNumber, string dateOfBirth, string dateOfExpiry)
    {
        _taskCompletionSource = new TaskCompletionSource<ValidationResult?>();
        NavigateToReader(documentNumber, dateOfBirth, dateOfExpiry);
        return _taskCompletionSource.Task;
    }

    private void NavigateToReader(string can)
    {
        var context = Platform.CurrentActivity;
        if (context == null) return;

        var clientId = ValidationSettings.CLIENT_ID;
        var validationId = Guid.NewGuid().ToString();
        var serverUrl = ValidationSettings.VALIDATION_URI;

        var intent = new Intent(context, typeof(EmrtdConnectorActivity));

        intent.PutExtra(PlatformConstants.CLIENT_ID, clientId);
        intent.PutExtra(PlatformConstants.VALIDATION_ID_KEY, validationId);
        intent.PutExtra(PlatformConstants.VALIDATION_URI, serverUrl);

        intent.PutExtra(PlatformConstants.CAN_KEY, can);

        context.StartActivityForResult(intent, RequestCode);
    }

    private void NavigateToReader(string documentNumber, string dateOfBirth, string dateOfExpiry)
    {
        var context = Platform.CurrentActivity;
        if (context == null) return;

        var clientId = ValidationSettings.CLIENT_ID;
        var validationId = Guid.NewGuid().ToString();
        var serverUrl = ValidationSettings.VALIDATION_URI;

        var intent = new Intent(context, typeof(EmrtdConnectorActivity));

        intent.PutExtra(PlatformConstants.CLIENT_ID, clientId);
        intent.PutExtra(PlatformConstants.VALIDATION_ID_KEY, validationId);
        intent.PutExtra(PlatformConstants.VALIDATION_URI, serverUrl);

        intent.PutExtra(PlatformConstants.DOCUMENT_NUMBER_KEY, documentNumber);
        intent.PutExtra(PlatformConstants.DATE_OF_BIRTH_KEY, dateOfBirth);
        intent.PutExtra(PlatformConstants.DATE_OF_EXPIRY_KEY, dateOfExpiry);
        context.StartActivityForResult(intent, RequestCode);
    }

    public void OnActivityResult(int requestCode, Result resultCode, Intent? data)
    {
        var context = Platform.CurrentActivity;
        if (context == null) return;

        if (requestCode == RequestCode)
        {
            if (resultCode == Result.Ok)
            {
                if (data != null)
                {
                    if (data.HasExtra(RETURN_ERROR))
                    {
                        Toast.MakeText(context, data.GetStringExtra(RETURN_ERROR), ToastLength.Long)?.Show();
                        _taskCompletionSource?.SetResult(null);
                        OnReaderCompleted(new ReaderResultEventArgs { Success = false });
                        return;
                    }

                    var settings = new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new CamelCaseNamingStrategy()
                        },
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                    };

                    var bundle = data?.Extras;
                    if (bundle == null)
                    {
                        _taskCompletionSource?.SetResult(null);
                        OnReaderCompleted(new ReaderResultEventArgs { Success = false });
                        return;
                    }

                    var json = bundle.GetString(RETURN_DATA);
                    if (string.IsNullOrWhiteSpace(json))
                    {
                        _taskCompletionSource?.SetResult(null);
                        OnReaderCompleted(new ReaderResultEventArgs { Success = false });
                        return;
                    }

                    ValidationResult? result = JsonConvert.DeserializeObject<ValidationResult>(json, settings);

                    if (result == null)
                    {
                        _taskCompletionSource?.SetResult(null);
                        OnReaderCompleted(new ReaderResultEventArgs { Success = false });
                    }
                    else
                    {
                        _taskCompletionSource?.SetResult(result);
                        OnReaderCompleted(new ReaderResultEventArgs { Success = true, Result = result });
                    }
                }
                else
                {
                    Toast.MakeText(context, "No data received from reader", ToastLength.Long)?.Show();
                    _taskCompletionSource?.SetResult(null);
                }
            }
            else if (resultCode == Result.Canceled)
            {
                if (data != null && data.HasExtra(RETURN_ERROR))
                {
                    Toast.MakeText(context, data.GetStringExtra(RETURN_ERROR), ToastLength.Long)?.Show();
                    _taskCompletionSource?.SetResult(null);
                }
                else
                {
                    Toast.MakeText(context, "Reader was cancelled", ToastLength.Long)?.Show();
                    _taskCompletionSource?.SetResult(null);
                }
            }
            else
            {
                Toast.MakeText(context, "Reader returned an error", ToastLength.Long)?.Show();
                _taskCompletionSource?.SetResult(null);
            }
        }
    }

    protected virtual void OnReaderCompleted(ReaderResultEventArgs e)
    {
        ReaderCompleted?.Invoke(this, e);
    }
}

// Event Args Klasse
public class ReaderResultEventArgs : EventArgs
{
    public bool Success { get; set; }
    public ValidationResult? Result { get; set; }
}
