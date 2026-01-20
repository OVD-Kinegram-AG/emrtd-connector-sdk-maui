#if ANDROID
using Android.Content;
using Java.Util;
using EmrtdConnectorAndroid;
using EmrtdConnectorMaui.Common;
using Android.App;
using Java.Lang;
using Android.Runtime;
using Android.Widget;

namespace EmrtdConnectorMaui
{
    public class PlatformService : IPlatformService
    {
        private static PlatformService? _instance;
        public event EventHandler<ReaderResultEventArgs>? ReaderCompleted;

        private static readonly int RequestCode = 0x6A5;
        private static readonly string RETURN_DATA = "DATA";
        private static readonly string RETURN_ERROR = "JSON_ERROR";

        public static PlatformService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlatformService();
                }
                return _instance;
            }
        }

        public void NavigateToReader(string can)
        {
            var context = Platform.CurrentActivity;
            if (context == null) return;

            var uuid = UUID.RandomUUID();
            if (uuid == null) return;
            var uuidString = uuid.ToString();

            var intent = new Intent(context, typeof(EmrtdConnectorActivity));
            intent.PutExtra(PlatformConstants.CLIENT_ID, ValidationSettings.CLIENT_ID);
            intent.PutExtra(PlatformConstants.VALIDATION_URI, ValidationSettings.VALIDATION_URI);
            intent.PutExtra(PlatformConstants.VALIDATION_ID_KEY, uuidString);
            intent.PutExtra(PlatformConstants.CAN_KEY, can);
            context?.StartActivityForResult(intent, RequestCode);
        }

        public void NavigateToReader(string documentNumber, string dateOfBirth, string dateOfExpiry)
        {
            var context = Platform.CurrentActivity;
            if (context == null) return;

            var uuid = UUID.RandomUUID();
            if (uuid == null) return;
            var uuidString = uuid.ToString();

            var intent = new Intent(context, typeof(EmrtdConnectorActivity));
            intent.PutExtra(PlatformConstants.CLIENT_ID, ValidationSettings.CLIENT_ID);
            intent.PutExtra(PlatformConstants.VALIDATION_URI, ValidationSettings.VALIDATION_URI);
            intent.PutExtra(PlatformConstants.VALIDATION_ID_KEY, uuidString);
            intent.PutExtra(PlatformConstants.DOCUMENT_NUMBER_KEY, documentNumber);
            intent.PutExtra(PlatformConstants.DATE_OF_BIRTH_KEY, dateOfBirth);
            intent.PutExtra(PlatformConstants.DATE_OF_EXPIRY_KEY, dateOfExpiry);
            context.StartActivityForResult(intent, RequestCode);
        }

        public void OnActivityResult(int requestCode, Result resultCode, Intent? data)
        {
            EmrtdPassport? emrtdPassport;
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
                            return;
                        }

                        if (OperatingSystem.IsAndroidVersionAtLeast(33))
                        {
                            emrtdPassport = data.GetParcelableExtra(RETURN_DATA, Class.FromType(typeof(EmrtdPassport))).JavaCast<EmrtdPassport>();
                        }
                        else
                        {
                            emrtdPassport = data.GetParcelableExtra(RETURN_DATA).JavaCast<EmrtdPassport>();
                        }

                        if (emrtdPassport == null)
                        {
                            OnReaderCompleted(new ReaderResultEventArgs { Success = false });
                        }
                        else
                        {
                            OnReaderCompleted(new ReaderResultEventArgs { Success = true, Result = emrtdPassport });
                        }
                    }
                    else
                    {
                        Toast.MakeText(context, "No data received from reader", ToastLength.Long)?.Show();
                    }
                }
                else if (resultCode == Result.Canceled)
                {
                    Toast.MakeText(context, "Reader was cancelled", ToastLength.Long)?.Show();
                }
                else
                {
                    Toast.MakeText(context, "Reader returned an error", ToastLength.Long)?.Show();
                }
            }
        }

        protected virtual void OnReaderCompleted(ReaderResultEventArgs e)
        {
            ReaderCompleted?.Invoke(this, e);
        }
    }
}
#endif
