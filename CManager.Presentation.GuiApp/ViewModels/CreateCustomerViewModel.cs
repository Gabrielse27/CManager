using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CManager.Application.Interfaces;
using CManager.Domain;

namespace CManager.Presentation.GuiApp.ViewModels;

public partial class CreateCustomerViewModel : ObservableObject
{
    private readonly ICustomerService _service;

    public CreateCustomerViewModel(ICustomerService service)
    {
        _service = service;
    }

    // ---- Form fields ----
    [ObservableProperty] private string firstName = string.Empty;
    [ObservableProperty] private string lastName = string.Empty;
    [ObservableProperty] private string email = string.Empty;
    [ObservableProperty] private string phone = string.Empty;

    [ObservableProperty] private string street = string.Empty;
    [ObservableProperty] private string postalCode = string.Empty;
    [ObservableProperty] private string city = string.Empty;

    // ---- Command to create new customer ----
    [RelayCommand]
    private async Task SaveCustomer()
    {
        _service.CreateCustomer(
            FirstName,
            LastName,
            Email,
            Phone,
            Street,
            PostalCode,
            City);
        

        // Navigate back to customer list
        await Shell.Current.GoToAsync("..");
    }
}

