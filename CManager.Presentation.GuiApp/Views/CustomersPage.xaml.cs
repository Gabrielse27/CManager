
using CManager.Domain;
using CManager.Presentation.GuiApp.ViewModels;


namespace CManager.Presentation.GuiApp.Views;

public partial class CustomersPage : ContentView
{
    public CustomersPage()
    {
        InitializeComponent();
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var customer = button?.BindingContext as Customer;

        if (customer == null) return;

        // Vi skriver "Microsoft.Maui.Controls.Application" för att undvika krockar
        bool answer = await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(
            "Ta bort kund",
            $"Vill du ta bort {customer.FirstName}?",
            "Ja",
            "Nej"
        );

        if (!answer) return;

        if (this.BindingContext is CustomersPageViewModel vm)
        {
            await vm.DeleteFromList(customer);
        }
    }


}
