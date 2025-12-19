
using CManager.Presentation.GuiApp.ViewModels;

namespace CManager.Presentation.GuiApp;

public partial class MainPage : ContentPage
{
    // Vi tar in MainViewModel via Dependency Injection
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();

        // HÄR ÄR NYCKELN: Vi kopplar ihop XAML med koden
        BindingContext = viewModel;
    }
}
