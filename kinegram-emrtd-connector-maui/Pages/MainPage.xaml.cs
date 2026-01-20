// Using #if-statements to switch between platforms breaks Visual Studio Code's error detection.
// Ignore "The type or namespace name" errors found in the original code downloaded from our Git.
#if ANDROID
using Org.Json;
using EmrtdConnectorAndroid;
#elif IOS
using EmrtdConnectorIos;
using Newtonsoft.Json;
#endif

using EmrtdConnectorMaui.Common;

namespace EmrtdConnectorMaui
{
    public partial class MainPage : ContentPage
    {
        // There is no compiler for resx files in Visual Studio Code,
        // so we have to use a workaround for localizations.
        private readonly LocalizedStrings localizedStrings;

        public MainPage()
        {
            InitializeComponent();

#if ANDROID
            PlatformService.Instance.ReaderCompleted += (sender, e) =>
            {
                if (e.Success)
                {
                    var result = e.Result;
                    if (result == null) return;

                    var emrtdPassport = EmrtdPassportMapper.ToCSharpEmrtdPassport(result);
                    if (emrtdPassport == null) return;
                    Navigation?.PushAsync(new ResultPage(emrtdPassport));
                }
            };
#endif

            localizedStrings = new LocalizedStrings();

            CanEntry.Text = Preferences.Default.Get(PlatformConstants.CAN_KEY, String.Empty);
            DocumentNumberEntry.Text = Preferences.Default.Get(PlatformConstants.DOCUMENT_NUMBER_KEY, String.Empty);
            DateOfBirthEntry.Text = Preferences.Default.Get(PlatformConstants.DATE_OF_BIRTH_KEY, String.Empty);
            DateOfExpiryEntry.Text = Preferences.Default.Get(PlatformConstants.DATE_OF_EXPIRY_KEY, String.Empty);

            CanButton.Clicked += (sender, args) =>
            {
                string strCan = CanEntry.Text;
                Preferences.Default.Set(PlatformConstants.CAN_KEY, strCan);
#if ANDROID
                PlatformService.Instance.NavigateToReader(strCan);
#elif IOS
                var wrapper = new KinegramEMRTDWrapper(ValidationSettings.CLIENT_ID, ValidationSettings.VALIDATION_URI);
                wrapper.ReadPassportWithCan(strCan, (passportJson, error) =>
                {
                    if (error != null)
                    {
                        DisplayAlert(localizedStrings["ChipAccessMrzButtonText"], error.LocalizedDescription, localizedStrings["ButtonOk"]);
                        return;
                    }

                    if (passportJson != null)
                    {
                        CSharpEmrtdPassport emrtdPassport = JsonConvert.DeserializeObject<CSharpEmrtdPassport>(passportJson);
                        Navigation.PushAsync(new ResultPage(emrtdPassport));
                    }
                });
#endif
            };

            MrzButton.Clicked += (sender, args) =>
            {
                string strDocumentNumber = DocumentNumberEntry.Text;
                string strDateOfBirth = DateOfBirthEntry.Text;
                string strDateOfExpiry = DateOfExpiryEntry.Text;

                Preferences.Default.Set(PlatformConstants.DOCUMENT_NUMBER_KEY, strDocumentNumber);
                Preferences.Default.Set(PlatformConstants.DATE_OF_BIRTH_KEY, strDateOfBirth);
                Preferences.Default.Set(PlatformConstants.DATE_OF_EXPIRY_KEY, strDateOfExpiry);
#if ANDROID
                PlatformService.Instance.NavigateToReader(strDocumentNumber, strDateOfBirth, strDateOfExpiry);
#elif IOS
                var wrapper = new KinegramEMRTDWrapper(ValidationSettings.CLIENT_ID, ValidationSettings.VALIDATION_URI);
                wrapper.ReadPassport(strDocumentNumber, strDateOfBirth, strDateOfExpiry, (passportJson, error) =>
                {
                    if (error != null)
                    {
                        DisplayAlert(localizedStrings["ChipAccessMrzButtonText"], error.LocalizedDescription, localizedStrings["ButtonOk"]);
                        return;
                    }

                    if (passportJson != null)
                    {
                        CSharpEmrtdPassport emrtdPassport = JsonConvert.DeserializeObject<CSharpEmrtdPassport>(passportJson);
                        Navigation.PushAsync(new ResultPage(emrtdPassport));
                    }
                });
#endif
            };
        }
    }
}