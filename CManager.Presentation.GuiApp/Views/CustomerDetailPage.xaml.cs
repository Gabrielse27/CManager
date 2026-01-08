using CManager.Presentation.GuiApp.ViewModels;


namespace CManager.Presentation.GuiApp.Views;

// "partial class" betyder att klassen hänger ihop med XAML-filen med samma namn.
// Vi ärver från "ContentView", vilket betyder att detta är en återanvändbar komponent (en del av en sida).
public partial class CustomerDetailPage : ContentView
{

    // Vi tar in ViewModel via konstruktorn Dependency Injection
    public CustomerDetailPage()
	{
		InitializeComponent();

        // Vi behöver inte sätta BindingContext här. 
        // Det kommer automatiskt från MainPage via DataTemplate.
    }
}