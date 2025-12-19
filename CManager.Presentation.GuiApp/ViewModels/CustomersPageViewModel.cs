using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CManager.Application.Interfaces;
using CManager.Domain;
using CManager.Presentation.GuiApp.Views;




namespace CManager.Presentation.GuiApp.ViewModels;

public partial class CustomersPageViewModel : ObservableObject
{
    private readonly ICustomerService _customerService;
    private readonly MainViewModel _mainViewModel; 

    // Vi tar in MainViewModel här i konstruktorn
    public CustomersPageViewModel(ICustomerService customerService, MainViewModel mainViewModel)
    {
        _customerService = customerService;
        _mainViewModel = mainViewModel; // Vi sparar referensen
        LoadCustomersAsync();
    }

    [ObservableProperty]
    private ObservableCollection<Customer> customers = new();

    public async Task LoadCustomersAsync()
    {
        var list = await _customerService.GetAllCustomersAsync();
        Customers = new ObservableCollection<Customer>(list);
    }

    // Gå till "Skapa kund"-sida
    [RelayCommand]
    private void GoToCreateCustomerPage()
    {
        // Här byter vi vy via MainViewModel istället för Shell
        _mainViewModel.CurrentViewModel = new CreateCustomerViewModel(_customerService, _mainViewModel);
    }

    // Navigera till kunddetaljer
    [RelayCommand]
    private void GoToDetails(Customer customer)
    {
        if (customer != null)
        {
            // Vi skapar detaljvyn och skickar med både Service och MainViewModel
            var detailVm = new CustomerDetailViewModel(_customerService, _mainViewModel);
            detailVm.Customer = customer;
            _mainViewModel.CurrentViewModel = detailVm;
        }
    }

    [RelayCommand]
    private void DeleteCustomer(Customer customer)
    {
        if (customer != null)
        {
            _customerService.DeleteCustomerAsync(customer.Id);
            Customers.Remove(customer);
        }
    }
}
