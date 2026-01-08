
using CManager.Application.Helpers;
using CManager.Application.Interfaces;
using CManager.Application.Services;
using CManager.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;




namespace CManager.Presentation.GuiApp.ViewModels;

// "partial" är jätteviktigt här! Det låter CommunityToolkit generera extra kod i bakgrunden.
// Vi ärver från ObservableObject för att automatiskt få stöd  (så gränssnittet uppdateras).

public partial class CreateCustomerViewModel : ObservableObject
{
    // Vi skapar privata fält för våra beroenden.
    // _service behövs för att kunna spara kunden till filen.
    private readonly ICustomerService _service;

    // _mainViewModel behövs för att vi ska kunna "byta sida" (navigera) tillbaka till listan.
    private readonly MainViewModel _mainViewModel;
    
    // Konstruktorn: Här sker Dependency Injection.
    // När vi navigerar till denna sida skickar vi med Servicen och Huvud-ViewModelen.
    public CreateCustomerViewModel(ICustomerService service, MainViewModel mainViewModel)
    {
        _service = service;
        _mainViewModel = mainViewModel;
        
    }

    // [ObservableProperty] har en bra koppling med  CommunityToolkit.
    // När du skriver detta, skapar verktyget automatiskt en public property (t.ex. Public "FirstName") 
    // som meddelar gränssnittet (UI) varje gång du skriver en bokstav.

    [ObservableProperty] private string firstName = string.Empty;
    [ObservableProperty] private string lastName = string.Empty;
    [ObservableProperty] private string email = string.Empty;
    [ObservableProperty] private string phone = string.Empty;

    //[ObservableProperty] private string address = string.Empty;
    [ObservableProperty] private string street = string.Empty;
    [ObservableProperty] private string postalCode = string.Empty;
    [ObservableProperty] private string city = string.Empty;



    // [RelayCommand] gör om denna metod till ett "ICommand" som knappar i XAML kan binda till.
    // Eftersom vi ska spara till fil (vilket tar tid), gör vi metoden "async".

    [RelayCommand]
    private async Task SaveCustomer()
    {
        var customer = new Customer
        {
            // Vi använder din GuidFactory här - det räcker som "Factory Pattern"!
           // Id = GuidFactory.CreateId(),

            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Phone = Phone,
            //Address = Address

            
        };

        // Vi anropar servicen för att spara kunden(servicen sköter ID - generering och filskrivning).
        await _service.SaveCustomerAsync(customer);

        // Gå tillbaka till listan via MainViewModel
        // Navigering: När vi sparat klart vill vi gå tillbaka till listan.
        // Vi gör detta genom att byta ut "CurrentViewModel" i MainViewModel.
        _mainViewModel.CurrentViewModel = new CustomersPageViewModel(_service, _mainViewModel);
        
    }

    [RelayCommand]
    private void Cancel()
    {    // Vi gör exakt samma navigering som ovan, fast utan att spara först.
        // Detta tar oss tillbaka till startsidan/listan.
        // Gå tillbaka utan att spara
        _mainViewModel.CurrentViewModel = new CustomersPageViewModel(_service, _mainViewModel);
        
    }
}
