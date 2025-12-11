using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CManager.Application.Interfaces;
using CManager.Domain;








namespace CManager.Presentation.GuiApp.ViewModels
{
    public partial class CustomersPageViewModel : ObservableObject
    {
        private readonly ICustomerService _service;


            public CustomersPageViewModel(ICustomerService service)
            {
            _service = service;
                LoadCustomers();
            }
        [ObservableProperty]
        ObservableCollection<Customer> customers = new();
        //private ObservableCollection<Customer> Customers;

        [RelayCommand]
        private void LoadCustomers()
        {
            var list = _service.GetAllCustomers();
            Customers = new ObservableCollection<Customer>(list);
        }


    }
}
