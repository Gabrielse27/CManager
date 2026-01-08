
using CManager.Presentation.GuiApp.ViewModels;

namespace CManager.Presentation.GuiApp;


// "public partial class" betyder att klassen hänger ihop med "MainPage.xaml".
// Vi ärver från "ContentPage", vilket betyder att detta är en hel sida som fyller skärmen.
public partial class MainPage : ContentPage
{
    // Konstruktorn: Körs när appen startar och ska visa huvudfönstret.
    // Vi använder Dependency Injection för att få in en färdig "MainViewModel" automatiskt.
    public MainPage (MainViewModel viewModel)
    {
        // Läser in XAML-filen och ritar upp layouten (menyn, loggan osv).
        InitializeComponent();

        // HÄR ÄR NYCKELN: Vi kopplar ihop XAML med koden
        // "BindingContext" är det objekt som XAML-filen lyssnar på.
        // Genom att sätta den här, kan vi i XAML skriva "{Binding CurrentViewModel}" och det fungerar direkt.
        BindingContext = viewModel;
    }
}
