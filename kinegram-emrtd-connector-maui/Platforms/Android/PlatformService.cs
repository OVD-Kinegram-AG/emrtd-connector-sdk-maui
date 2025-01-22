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
        private static PlatformService _instance;
        public event EventHandler<ReaderResultEventArgs> ReaderCompleted;

        private static readonly int RequestCode = 0x6A5;
        private static readonly string RETURN_DATA = "PASSPORT_DATA";
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
            string uuid = UUID.RandomUUID().ToString();

            var intent = new Intent(context, typeof(EmrtdConnectorActivity));
            intent.PutExtra(PlatformConstants.VALIDATION_ID_KEY, uuid);
            intent.PutExtra(PlatformConstants.CAN_KEY, can);
            context?.StartActivityForResult(intent, RequestCode);
        }

        public void NavigateToReader(string documentNumber, string dateOfBirth, string dateOfExpiry)
        {
            var context = Platform.CurrentActivity;
            var uuid = UUID.RandomUUID().ToString();

            var intent = new Intent(context, typeof(EmrtdConnectorActivity));
            intent.PutExtra(PlatformConstants.VALIDATION_ID_KEY, uuid);
            intent.PutExtra(PlatformConstants.DOCUMENT_NUMBER_KEY, documentNumber);
            intent.PutExtra(PlatformConstants.DATE_OF_BIRTH_KEY, dateOfBirth);
            intent.PutExtra(PlatformConstants.DATE_OF_EXPIRY_KEY, dateOfExpiry);
            context.StartActivityForResult(intent, RequestCode);
        }

        public void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            EmrtdPassport? emrtdPassport;

            if (requestCode == RequestCode)
            {
                if (resultCode == Result.Ok)
                {
                    if (data != null)
                    {
                        if (data.HasExtra(RETURN_ERROR))
                        {
                            Toast.MakeText(Platform.CurrentActivity, data.GetStringExtra(RETURN_ERROR), ToastLength.Long).Show();
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
                        Toast.MakeText(Platform.CurrentActivity, "No data received from reader", ToastLength.Long).Show();
                    }
                }
                else if (resultCode == Result.Canceled)
                {
                    Toast.MakeText(Platform.CurrentActivity, "Reader was cancelled", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(Platform.CurrentActivity, "Reader returned an error", ToastLength.Long).Show();
                }
            }
        }

        protected virtual void OnReaderCompleted(ReaderResultEventArgs e)
        {
            ReaderCompleted?.Invoke(this, e);
        }
    }
}
