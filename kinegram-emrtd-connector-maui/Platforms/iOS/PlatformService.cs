using Foundation;

using EmrtdConnectorIos;
using EmrtdConnectorMaui.Common;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EmrtdConnectorMaui;

public class PlatformService : IPlatformService
{
    private static PlatformService? _instance;
    public static PlatformService Instance => _instance ??= new PlatformService();

    private EmrtdConnectorObjCWrapper? _connector;

    private static readonly JsonSerializerSettings JsonSettings = new()
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        },
        MissingMemberHandling = MissingMemberHandling.Ignore,
        NullValueHandling = NullValueHandling.Ignore
    };

    public async Task<ValidationResult?> NavigateToReaderAsync(string can)
    {
        try
        {
            var serverUrl = new NSUrl(ValidationSettings.VALIDATION_URI);
            var validationId = Guid.NewGuid().ToString();
            var clientId = ValidationSettings.CLIENT_ID;

            var tcs = new TaskCompletionSource<string>();

            _connector = new EmrtdConnectorObjCWrapper(serverUrl, validationId, clientId);
            _connector.ReadPassport(can, validationId, null, false, (result, error) =>
            {
                try
                {
                    if (error != null) tcs.TrySetException(new NSErrorException(error));
                    else if (result != null) tcs.TrySetResult(result);
                }
                finally
                {
                    _connector = null;
                }
            });

            string jsonResult = await tcs.Task;
            return JsonConvert.DeserializeObject<ValidationResult>(jsonResult, JsonSettings);
        }
        catch (ObjCRuntime.ObjCException)
        {
            // TODO Handle error
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Managed exception: " + ex);
            return null;
        }
    }

    public async Task<ValidationResult?> NavigateToReaderAsync(string documentNumber, string dateOfBirth, string dateOfExpiry)
    {
        try
        {
            var serverUrl = new NSUrl(ValidationSettings.VALIDATION_URI);
            var validationId = Guid.NewGuid().ToString();
            var clientId = ValidationSettings.CLIENT_ID;

            var tcs = new TaskCompletionSource<string>();

            _connector = new EmrtdConnectorObjCWrapper(serverUrl, validationId, clientId);
            _connector.ReadPassport(documentNumber, dateOfBirth, dateOfExpiry, validationId, null, false, (result, error) =>
            {
                try
                {
                    if (error != null) tcs.TrySetException(new NSErrorException(error));
                    else if (result != null) tcs.TrySetResult(result);
                }
                finally
                {
                    _connector = null;
                }
            });

            string jsonResult = await tcs.Task;
            return JsonConvert.DeserializeObject<ValidationResult>(jsonResult, JsonSettings);
        }
        catch (ObjCRuntime.ObjCException)
        {
            // TODO Handle error
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Managed exception: " + ex);
            return null;
        }
    }
}