using CManager.Application.Interfaces;
using CManager.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CManager.Presentation.GuiApp.ViewModels
{
    public partial class CustomerDetailViewModel : ObservableObject
    {
        private readonly ICustomerService _service;

        public CustomerDetailViewModel(ICustomerService service)
        {
            _service = service;
        }

        // ---- Input från UI ----
        [ObservableProperty]
        private string email = string.Empty;

        // ---- Kunddata som ska visas ----
        
        [ObservableProperty] private string firstName = string.Empty;
        [ObservableProperty] private string lastName = string.Empty;
        [ObservableProperty] private string phone = string.Empty;
        [ObservableProperty] private string street = string.Empty;
        [ObservableProperty] private string postalCode = string.Empty;
        [ObservableProperty] private string city = string.Empty;
        [ObservableProperty] private string customerId = string.Empty;

        // ---- Kommandon ----
        [RelayCommand]
        private void LoadCustomer()
        {
            var customer = _service.GetCustomerByEmail(email);

            if (customer == null)
            {
                // Töm fälten om ingen kund hittas
                FirstName = LastName = Phone =
                Street = PostalCode = City = CustomerId = string.Empty;
                return;
            }

            // Fyll ViewModel med kunddata
            
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Phone = customer.Phone;
            Street = customer.Street;
            PostalCode = customer.PostalCode;
            City = customer.City;
            CustomerId = customer.Id.ToString();
        }

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync(".."); // Navigerar tillbaka
        }
    }
}