using CManager.Application.Interfaces;
using CManager.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using CManager.Application.Helpers;



namespace CManager.Presentation.GuiApp.ViewModels;

public partial class CustomerDetailViewModel : ObservableObject
{
    private readonly ICustomerService _customerService;
    private readonly MainViewModel _mainViewModel; // <--- Lägg till denna

    // Uppdatera konstruktorn att ta emot MainViewModel
    public CustomerDetailViewModel(ICustomerService customerService, MainViewModel mainViewModel)
    {
        _customerService = customerService;
        _mainViewModel = mainViewModel;
    }

    [ObservableProperty]
    private Customer customer;

    [RelayCommand]
    private async Task Save()
    {
        if (Customer != null)
        {
            await _customerService.UpdateCustomerAsync(Customer);

            // Gå tillbaka till listan genom att byta vy i MainViewModel
            _mainViewModel.CurrentViewModel = new CustomersPageViewModel(_customerService, _mainViewModel);
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        // Gå tillbaka utan att spara
        _mainViewModel.CurrentViewModel = new CustomersPageViewModel(_customerService, _mainViewModel);
    }

    // Delete-logiken kan du behålla som den var, men ändra navigeringen på slutet:
    [RelayCommand]
    private async Task DeleteCustomer()
    {
        if (Customer == null) return;
        await _customerService.DeleteCustomerAsync(Customer.Id);

        // Gå tillbaka
        _mainViewModel.CurrentViewModel = new CustomersPageViewModel(_customerService, _mainViewModel);
    }
}