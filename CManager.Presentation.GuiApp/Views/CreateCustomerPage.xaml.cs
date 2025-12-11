using CManager.Application.Interfaces;
using CManager.Presentation.GuiApp.ViewModels;


namespace CManager.Presentation.GuiApp.Views;

public partial class CreateCustomerPage : ContentPage
{
	public CreateCustomerPage(ICustomerService service)
	{
		InitializeComponent();
		BindingContext = new CreateCustomerViewModel(service);
	}
}