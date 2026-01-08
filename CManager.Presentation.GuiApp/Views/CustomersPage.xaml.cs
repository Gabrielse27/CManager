
using CManager.Domain;
using CManager.Presentation.GuiApp.ViewModels;


namespace CManager.Presentation.GuiApp.Views;

// Vi ärver från ContentView eftersom detta är en del-vy som visas inuti huvudfönstret.
public partial class CustomersPage : ContentView
{
    // Konstruktorn som körs när sidan skapas.
    public CustomersPage()
    {
        // Läser in XAML-filen och ritar upp alla knappar och listor på skärmen.
        InitializeComponent();
    }
    // Detta är en "Event Handler" som körs när man klickar på "Ta bort"-knappen i listan.
    private async void OnDeleteClicked (object sender, EventArgs e)
    {
        // "sender" är objektet som startade händelsen(alltså Knappen vi klickade på).
        // Vi omvandlar (castar) det till en Button så vi kan använda den.
        var button = sender as Button;

        // Eftersom knappen ligger inuti en ListView/ CollectionView, så är dess "BindingContext"
        // inte hela sidan, utan just den specifika KUNDEN (Customer) som ligger på den raden.
        var customer = button?.BindingContext as Customer;

        // Om något gick fel och vi inte fick tag på kunden, avbryt.
        if (customer == null) return;

        // Vi skriver "Microsoft.Maui.Controls.Application" för att undvika krockar.
        // Vi visar en varningsruta(Pop-up) för användaren.
        // Vi skriver hela sökvägen "Microsoft.Maui.Controls.Application" för att inte datorn ska blanda ihop det med din "CManager.Application".
        bool answer = await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(
            "Ta bort kund",
            $"Vill du ta bort {customer.FirstName}?",
            "Ja",
            "Nej"
        );

        // Om användaren svarade "Nej", avbryt.
        if (!answer) return;

        // Nu måste vi prata med ViewModelen för att faktiskt ta bort kunden.
        // Vi hämtar sidans BindingContext och kollar om det är vår "CustomersPageViewModel".
        if (this.BindingContext is CustomersPageViewModel vm)
        {
            // Vi anropar ViewModelens metod för att ta bort kunden.
            await vm.DeleteFromList(customer);
        }
    }
}
