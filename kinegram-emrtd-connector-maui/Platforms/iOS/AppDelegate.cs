using Foundation;
using UIKit;

namespace EmrtdConnectorMaui.Platforms.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            DependencyService.Register<IPlatformService, PlatformService>();
            return base.FinishedLaunching(application, launchOptions);
        }
    }
}
