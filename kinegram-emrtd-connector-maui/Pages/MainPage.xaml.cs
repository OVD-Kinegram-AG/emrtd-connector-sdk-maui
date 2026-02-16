using EmrtdConnectorMaui.Common;

namespace EmrtdConnectorMaui;

public partial class MainPage : ContentPage
{
    private readonly LocalizedStrings localizedStrings = new();

    public MainPage()
    {
        InitializeComponent();

        CanEntry.Text = Preferences.Default.Get(PlatformConstants.CAN_KEY, String.Empty);
        DocumentNumberEntry.Text = Preferences.Default.Get(PlatformConstants.DOCUMENT_NUMBER_KEY, String.Empty);
        DateOfBirthEntry.Text = Preferences.Default.Get(PlatformConstants.DATE_OF_BIRTH_KEY, String.Empty);
        DateOfExpiryEntry.Text = Preferences.Default.Get(PlatformConstants.DATE_OF_EXPIRY_KEY, String.Empty);

        CanButton.Clicked += OnCanButtonClicked;
        MrzButton.Clicked += OnMrzButtonClicked;
    }

    private async void OnCanButtonClicked(object? sender, EventArgs e)
    {
        string strCan = CanEntry.Text;
        Preferences.Default.Set(PlatformConstants.CAN_KEY, strCan);

        try
        {
            var result = await PlatformService.Instance.NavigateToReaderAsync(strCan);
            if (result != null)
            {
                await Navigation.PushAsync(new ResultPage(result));
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert(localizedStrings["ChipAccessMrzButtonText"], ex.Message, localizedStrings["ButtonOk"]);
        }
    }

    private async void OnMrzButtonClicked(object? sender, EventArgs e)
    {
        string strDocumentNumber = DocumentNumberEntry.Text;
        string strDateOfBirth = DateOfBirthEntry.Text;
        string strDateOfExpiry = DateOfExpiryEntry.Text;

        Preferences.Default.Set(PlatformConstants.DOCUMENT_NUMBER_KEY, strDocumentNumber);
        Preferences.Default.Set(PlatformConstants.DATE_OF_BIRTH_KEY, strDateOfBirth);
        Preferences.Default.Set(PlatformConstants.DATE_OF_EXPIRY_KEY, strDateOfExpiry);

        try
        {
            var result = await PlatformService.Instance.NavigateToReaderAsync(strDocumentNumber, strDateOfBirth, strDateOfExpiry);
            if (result != null)
            {
                await Navigation.PushAsync(new ResultPage(result));
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert(localizedStrings["ChipAccessMrzButtonText"], ex.Message, localizedStrings["ButtonOk"]);
        }
    }
}
