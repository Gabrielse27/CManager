using CManager.Application.Helpers;
using CManager.Application.Interfaces;
using CManager.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Application = Microsoft.Maui.Controls.Application;
using Button = Microsoft.Maui.Controls.Button;
































namespace CManager.Presentation.GuiApp.ViewModels;

// "partial" krävs för att CommunityToolkit ska kunna generera bakgrundskod (som INotifyPropertyChanged).
// Vi ärver från ObservableObject för att koppla ihop data med gränssnittet (Binding).
public partial class CustomerDetailViewModel : ObservableObject
{
    // Beroendeinjektion: Vi behöver servicen för att kunna spara ändringar till filen.
    private readonly ICustomerService _customerService;
   

    // Beroendeinjektion: Vi behöver MainViewModel för att kunna navigera tillbaka till listan.
    private readonly MainViewModel _mainViewModel;
   
    // Uppdatera konstruktorn att ta emot MainViewModel
    public CustomerDetailViewModel(ICustomerService customerService, MainViewModel mainViewModel)
    {
        _customerService = customerService;
        _mainViewModel = mainViewModel;
        
    }


    // [ObservableProperty] gör att vi slipper skriva långa "get/set"-metoder manuellt.
    // Detta objekt håller all data om kunden vi just nu tittar på/redigerar.
    // Om vi ändrar något här (t.ex. namn), uppdateras textrutorna på skärmen automatiskt.
    [ObservableProperty]
    private Customer customer;

    // [RelayCommand] gör att vi kan binda en knapp i XAML (t.ex. "Spara") till denna metod.
    // Vi använder "async Task" för att operationen inte ska låsa gränssnittet.
    [RelayCommand]
    private async Task Save()
    {
        // Säkerhetskoll: Vi sparar bara om det faktiskt finns en kund laddad.
        if ( Customer != null )
        {
            // Anropar servicen för att spara ändringarna till filen (UpdateAsync i repot).
            // Eftersom kunden redan har ett ID, kommer servicen förstå att det är en uppdatering.
            await _customerService.SaveCustomerAsync(Customer);

            // Navigering: När vi är klara går vi tillbaka till startsidan (listan).
            // Vi gör detta genom att byta ut den aktiva ViewModelen i MainViewModel.
            _mainViewModel.CurrentViewModel = new CustomersPageViewModel(_customerService, _mainViewModel);
            
        }
    }

    [RelayCommand]
    public async Task DeleteCustomer()
    {
        // 1. Säkerhetskoll
        if (Customer == null) return;

        // 2. Fråga användaren
        bool answer = await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(
    "Varning",
    $"Vill du ta bort {Customer.FirstName}?",
    "Ja",
    "Nej");

        if (!answer) return;

        // 3. Ta bort kunden via servicen (Filen)
        await _customerService.DeleteCustomerAsync(Customer.Id);

        // 4. DEN NYA LÖSNINGEN:
        // Vi ber MainViewModel att byta tillbaka till översikten.
        // Eftersom vi injicerat _mainViewModel i konstruktorn kan vi använda den här.
        _mainViewModel.GoToOverview();
    }




















    /*
        [RelayCommand]
         public async Task DeleteCustomer()
         {
             // 1. Fråga användaren först (säkerhet)
             bool answer = await Shell.Current.DisplayAlert("Varning", $"Vill du ta bort {Customer.FirstName}?", "Ja", "Nej");

             if (!answer) return; // Om de svara Nej, gör inget.

             // 2. Ta bort kunden via servicen
             if (Customer != null)
             {
                 await _customerService.DeleteCustomerAsync(Customer.Id);
             }

             // 3. Navigera tillbaka till listan
             // Eftersom du redan har _mainViewModel injicerad (jag ser den på din bild!), använd den:
             _mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<CustomersPageViewModel>();
         }  */





    [RelayCommand]
    private void Cancel()
    {
        // Gå tillbaka utan att spara
        // Vi navigerar tillbaka till listan direkt, utan att anropa SaveCustomerAsync.
        // Eventuella ändringar som gjorts i textrutorna sparas alltså inte till filen.
        _mainViewModel.CurrentViewModel = new CustomersPageViewModel(_customerService, _mainViewModel);
        //_mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<CustomersPageViewModel>();
    }

    // Knapp för att gå tillbaka (t.ex. via en "Tillbaka")
    [RelayCommand]
    public void GoBack()
    {

        // Byter vy tillbaka till listan över kunder.
        _mainViewModel.CurrentViewModel = new CustomersPageViewModel(_customerService, _mainViewModel);
        //_mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<CustomersPageViewModel>();
    }
}