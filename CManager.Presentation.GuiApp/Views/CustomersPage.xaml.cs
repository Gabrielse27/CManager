using CManager.Presentation.GuiApp.ViewModels;

namespace CManager.Presentation.GuiApp.Views;

// Ändra till ContentView här också!
public partial class CustomersPage : ContentView
{
    public CustomersPage()
    {
        InitializeComponent();
    }

    // Vi behöver inte "OnAppearing" här längre, 
    // för din ViewModel laddar datan automatiskt i sin konstruktor!
}
