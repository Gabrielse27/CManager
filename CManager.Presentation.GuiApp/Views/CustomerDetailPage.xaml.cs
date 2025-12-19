using CManager.Presentation.GuiApp.ViewModels;


namespace CManager.Presentation.GuiApp.Views;

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