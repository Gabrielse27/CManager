
using CManager.Presentation.GuiApp.ViewModels;
using CManager.Presentation.GuiApp.Views;
using System.Globalization;

namespace CManager.Presentation.GuiApp;


// Denna klass fungerar som en brygga mellan logiken(ViewModel) och gränssnittet(View).
// Den implementerar IValueConverter för att kunna användas direkt i XAML för att automatiskt byta vy.

public class ViewModelToViewConverter : IValueConverter
{
    // Convert-metoden körs automatiskt när Binding-motorn upptäcker att CurrentViewModel har ändrats.
    // Parametern 'value' är den ViewModel som vi vill visa just nu.

    public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
    {

        // Här sker logiken för att matcha rätt ViewModel med rätt Sida (View).
        // Här bestämmer vi vilken sida som ska visas beroende på ViewModel
        // Om datat vi fick in är "CustomersPageViewModel" (Startsidan)...
        if (value is CustomersPageViewModel)
        {
            // Så skapar vi och returnerar en ny "CustomersPage
            // Vi sätter 'BindingContext = value' för att koppla ihop vyn med datan direkt.
            return new CustomersPage { BindingContext = value};
        }
        // Om datat är "CreateCustomerViewModel"(Skapa - sidan)
        else if (value is CreateCustomerViewModel)
        {
            // så visar vi sidan för att skapa en ny kund.
            return new CreateCustomerPage() { BindingContext = value };
        }
        // Om datat är "CustomerDetailViewModel" (Detaljsidan)...
        else if (value is CustomerDetailViewModel)
        {
            // Så visar vi detaljsidan för den specifika kunden.
            return new CustomerDetailPage() { BindingContext = value };
        }

        // Om vi inte hittar någon matchning , returnerar en enkel Label med felmeddelande.
        return new Label { Text = "Ingen vy hittades för: " + value?.GetType().Name };
    }
    // ConvertBack krävs av IValueConverter-interfacet, men vi använder det inte.
    // Eftersom vi bara konverterar FRÅN ViewModel TILL View (enkelriktat), 
    // behöver vi ingen logik för att gå åt andra hållet.
    // ConvertBack , används om man vill gå från Vy tillbaka till ViewModel.
    // Eftersom vi aldrig gör det i detta flöde, lämnar vi den oimplementerad.
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}