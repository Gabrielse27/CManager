using Microsoft.Maui.Controls;

namespace CManager.Presentation.GuiApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Registrera routes (bra för Shell-navigation)
        Routing.RegisterRoute("customers", typeof(Views.CustomersPage));
        Routing.RegisterRoute("create-customer", typeof(Views.CreateCustomerPage));
    }
}
