using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace EmrtdConnectorMaui.Resources.Locales
{
    public class LocalizationResourceManager : INotifyPropertyChanged
    {
        private CultureInfo currentCulture;
        private ResourceManager resourceManager;

        // Make the constructor public
        public LocalizationResourceManager()
        {
            currentCulture = CultureInfo.CurrentCulture;
            resourceManager = new ResourceManager("EmrtdConnectorMaui.Resources.Locales.Strings", typeof(LocalizationResourceManager).Assembly);
        }

        // Change to a backing field with property
        private static readonly LocalizationResourceManager instance = new();
        public static LocalizationResourceManager Instance => instance;

        public object this[string resourceKey]
            => resourceManager.GetObject(resourceKey, currentCulture) ?? string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetCulture(CultureInfo culture)
        {
            currentCulture = culture;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}