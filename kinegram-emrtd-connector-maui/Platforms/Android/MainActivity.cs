#if ANDROID
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.View;

namespace EmrtdConnectorMaui.Platforms.Android
{
    [Activity(MainLauncher = true, LaunchMode = LaunchMode.SingleTop, Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : MauiAppCompatActivity
    {
        public MainActivity()
        {
            DependencyService.Register<IPlatformService, PlatformService>();
        }

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            Theme?.ApplyStyle(Resource.Style.OptOutEdgeToEdgeEnforcement, force: false);

            base.OnCreate(savedInstanceState);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent? data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            PlatformService.Instance.OnActivityResult(requestCode, resultCode, data);
        }
    }
}
#endif