using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CManager.Application.Interfaces;


namespace CManager.Presentation.GuiApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    // Här skapar vi fälten så att klassen "känner igen" dem
    private readonly ICustomerService _customerService;

    [ObservableProperty]
    private ObservableObject currentViewModel;

    // Konstruktorn
    public MainViewModel(ICustomerService customerService)
    {
        _customerService = customerService;

        // Nu startar vi startsidan. "this" skickar med hela MainViewModel.
        CurrentViewModel = new CustomersPageViewModel(_customerService, this);
    }

    

}
