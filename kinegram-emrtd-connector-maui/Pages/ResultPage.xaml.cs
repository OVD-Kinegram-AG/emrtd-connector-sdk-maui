namespace EmrtdConnectorMaui;

// This page is using the MVVM approach and therefore differs from the other pages.

public partial class ResultPage : ContentPage
{
    public ResultPage()
    {
        InitializeComponent();
    }

    public ResultPage(CSharpEmrtdPassport emrtdPassport)
    {
        BindingContext = new ResultPageModel(emrtdPassport);
        InitializeComponent();
    }
}