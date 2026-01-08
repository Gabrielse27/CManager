using CManager.Application.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace CManager.Presentation.GuiApp.ViewModels;

// "partial" måste vara med för att CommunityToolkit ska fungera (den genererar kod i bakgrunden).
// Vi ärver från ObservableObject för att kunna meddela UI:t när vi byter sida (INotifyPropertyChanged).

public partial class MainViewModel : ObservableObject
{
    // Vi behåller en referens till servicen (vårt verktyg för att spara/hämta data).
    // MainViewModel använder den inte själv, men den måste "äga" den för att kunna skicka den vidare till undersidorna.
    private readonly ICustomerService _customerService;
    private readonly IServiceProvider _serviceProvider;


    // Detta är den absolut viktigaste raden för Navigering
    // [ObservableProperty] skapar automatiskt en property som heter "CurrentViewModel".
    // Beroende på VILKET objekt som ligger i denna variabel, kommer Convertern (som vi kollade på förut) 
    // att välja vilken snygg vy (Sida) som ska visas på skärmen.

    [ObservableProperty]
    private ObservableObject currentViewModel;

    // Konstruktorn: körs en ändagång när appen startar
    // Här tar vi emot servicen via Dependency Injection.

    public void GoToOverview()
    {
        // Eftersom MainViewModel redan har _serviceProvider kan den skapa sidan korrekt!
        CurrentViewModel = _serviceProvider.GetRequiredService<CustomersPageViewModel>();
    }


    public MainViewModel(ICustomerService customerService, IServiceProvider serviceProvider)
    {
        _customerService = customerService;
        _serviceProvider = serviceProvider;

        // Nu startar vi startsidan. "this" skickar med hela MainViewModel.
        // Här bestämmer vi startsidan.
        // Vi skapar en ny "CustomersPageViewModel" (Listan med kunder) och lägger den i CurrentViewModel.
        // VIKTIGT: Vi skickar med "this" (oss själva) till barnet.
        // Det gör att barnet (CustomersPage) får en "fjärrkontroll" till MainViewModel och kan byta kanal (byta sida) senare.
        CurrentViewModel = new CustomersPageViewModel(_customerService, this);
        
    }

}
