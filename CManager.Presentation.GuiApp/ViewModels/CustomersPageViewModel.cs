using CManager.Application.Interfaces;
using CManager.Domain;
using CManager.Presentation.GuiApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;




namespace CManager.Presentation.GuiApp.ViewModels;

// "partial" är nödvändigt för att CommunityToolkit ska kunna generera kod åt oss.
// "ObservableObject" är basen som gör att gränssnittet uppdateras när data ändras (INotifyPropertyChanged).
public partial class CustomersPageViewModel : ObservableObject
{
    // Vi behöver servicen för att kunna hämta och ta bort kunder från databasen/filen.
    private readonly ICustomerService _customerService;
   

    // Vi behöver MainViewModel för att kunna byta sida (Navigera).
    private readonly MainViewModel _mainViewModel;
    

    // Konstruktorn: Körs när denna sida laddas.
    // Här sker Dependency Injection: Vi får in servicen och huvud-viewmodelen utifrån.
    public CustomersPageViewModel(ICustomerService customerService, MainViewModel mainViewModel)
    {
        _customerService = customerService;
        _mainViewModel = mainViewModel; // Vi sparar referensen
        
        // Vi startar hämtningen av kunder direkt när sidan öppnas.
        // Eftersom konstruktorer inte kan vara async, anropar vi metoden så här.
        LoadCustomersAsync();
        
    }
    // "ObservableCollection" är en special-lista. 
    // Om vi lägger till eller tar bort en kund ur denna lista, uppdateras skärmen (ListView/CollectionView) AUTOMATISKT.
    // En vanlig List<Customer> hade inte gjort det.
    [ObservableProperty]
    private ObservableCollection<Customer> customers = new();

    // Metod för att hämta data.
    public async Task LoadCustomersAsync()
    {
        // 1. Vi ber servicen hämta listan från filen (detta kan ta lite tid, därför async).
        var list = await _customerService.GetCustomersAsync();

        // 2. Vi konverterar den vanliga listan till en ObservableCollection så att UI:t förstår den.
        Customers = new ObservableCollection<Customer>(list);
    }

    // [RelayCommand] skapar ett kommando som vi kan binda knappar till i XAML.
    // Denna metod navigerar till "Skapa Kund"-sidan.
    [RelayCommand]
    private void GoToCreateCustomerPage()
    {
        // Navigerings-strategi: Vi byter ut "CurrentViewModel" i föräldern (MainViewModel).
        // Då kommer vår Converter automatiskt byta ut grafiken på skärmen.
        _mainViewModel.CurrentViewModel = new CreateCustomerViewModel(_customerService, _mainViewModel);
        
    }

    // Navigera till kunddetaljer
    [RelayCommand]
    private void GoToDetails(Customer customer)
    {
        // Säkerhetskoll så vi inte klickade på en tom rad.
        if (customer != null) 
        {
            // Vi skapar nästa sidas  ViewModel
                  var detailViewModel = new CustomerDetailViewModel(_customerService, _mainViewModel);

            //var detailViewModel = _serviceProvider.GetRequiredService<CustomerDetailViewModel>();
            //  Vi skickar med kunden vi klickade på till nästa sida.
            detailViewModel.Customer = customer;

            // Vi byter sida.
            _mainViewModel.CurrentViewModel = detailViewModel;
        }
    }


    [RelayCommand]
    public async Task DeleteFromList(Customer customer)
    {
        if (customer == null) return;

        // 1. Fråga användaren först 
        bool answer = await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(
    "Ta bort",
    $"Vill du verkligen ta bort {customer.FirstName} {customer.LastName}?",
    "Ja", "Nej");

        if (!answer) return;

        
        // 2. Ta bort från databasen/filen via servicen
        await _customerService.DeleteCustomerAsync(customer.Id);

        // 3. Ta bort från listan som syns på skärmen direkt.
        Customers.Remove(customer);
    }
}