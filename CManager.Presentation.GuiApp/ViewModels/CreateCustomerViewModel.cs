using CManager.Application.Interfaces;
using CManager.Application.Services;
using CManager.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;




namespace CManager.Presentation.GuiApp.ViewModels;

public partial class CreateCustomerViewModel : ObservableObject
{
    private readonly ICustomerService _service;
    private readonly MainViewModel _mainViewModel; 

    public CreateCustomerViewModel(ICustomerService service, MainViewModel mainViewModel)
    {
        _service = service;
        _mainViewModel = mainViewModel; 
    }

    [ObservableProperty] private string firstName = string.Empty;
    [ObservableProperty] private string lastName = string.Empty;
    [ObservableProperty] private string email = string.Empty;
    [ObservableProperty] private string phone = string.Empty;

    [ObservableProperty] private string street = string.Empty;
    [ObservableProperty] private string postalCode = string.Empty;
    [ObservableProperty] private string city = string.Empty;

    [RelayCommand]
    private async Task SaveCustomer()
    {
        var newCustomer = new Customer
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Phone = Phone,
            Street = Street,
            PostalCode = PostalCode,
            City = City
        };

        await _service.SaveCustomerAsync(newCustomer);

        // Gå tillbaka till listan via MainViewModel
        _mainViewModel.CurrentViewModel = new CustomersPageViewModel(_service, _mainViewModel);
    }

    [RelayCommand]
    private void Cancel()
    {
        // Gå tillbaka utan att spara
        _mainViewModel.CurrentViewModel = new CustomersPageViewModel(_service, _mainViewModel);
    }
}
