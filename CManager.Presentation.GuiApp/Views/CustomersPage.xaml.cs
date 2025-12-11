using CManager.Presentation.GuiApp.ViewModels;

namespace CManager.Presentation.GuiApp.Views;

public partial class CustomersPage : ContentPage
{
    public CustomersPage(CustomersPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}






















/*namespace CManager.Presentation.GuiApp.Views;

public partial class CustomersPage : ContentPage
{
	public CustomersPage()
	{
		InitializeComponent();
	}
}*/