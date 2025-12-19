using System.Globalization;
using CManager.Presentation.GuiApp.ViewModels;
using CManager.Presentation.GuiApp.Views;

namespace CManager.Presentation.GuiApp;

public class ViewModelToViewConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // Här bestämmer vi vilken sida som ska visas beroende på ViewModel
        if (value is CustomersPageViewModel)
        {
            return new CustomersPage() { BindingContext = value };
        }
        else if (value is CreateCustomerViewModel)
        {
            return new CreateCustomerPage() { BindingContext = value };
        }
        else if (value is CustomerDetailViewModel)
        {
            return new CustomerDetailPage() { BindingContext = value };
        }

        // Om vi inte hittar någon matchning
        return new Label { Text = "Ingen vy hittades för: " + value?.GetType().Name };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}