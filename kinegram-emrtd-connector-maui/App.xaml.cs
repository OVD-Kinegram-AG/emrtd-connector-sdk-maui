using System.Globalization;
using EmrtdConnectorMaui.Resources.Locales;
using Microsoft.Maui.Controls;

namespace EmrtdConnectorMaui
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            //MainPage = new AppShell();
            var systemCulture = CultureInfo.CurrentCulture;
            LocalizationResourceManager.Instance.SetCulture(systemCulture);

            //MainPage = new NavigationPage(new MainPage());
            MainPage = new NavigationPage(serviceProvider.GetRequiredService<MainPage>());
        }
    }
}
