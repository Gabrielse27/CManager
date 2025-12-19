using CManager.Application.Helpers;
using CManager.Application.Interfaces;
using CManager.Application.Services;
using CManager.Domain;
using CManager.Infrastructure;
using CManager.Infrastructure.Repositories;
using CManager.Presentation.GuiApp.ViewModels;
using CManager.Presentation.GuiApp.Views;
using CommunityToolkit.Mvvm;
using Microsoft.Extensions.Logging;




namespace CManager.Presentation.GuiApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // 1. Services & Repositories (Dessa var korrekta)
            builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
            builder.Services.AddSingleton<ICustomerService, CustomerService>();

            // 2. VIKTIGT: Huvudstrukturen (Dessa saknades!)
            // MainViewModel är "trafikpolisen", den måste finnas.
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();

            // 3. ViewModels (Registrera bara en gång per ViewModel)
            builder.Services.AddTransient<CustomersPageViewModel>();
            builder.Services.AddTransient<CreateCustomerViewModel>();
            builder.Services.AddTransient<CustomerDetailViewModel>();

            // 4. Views (Bra att ha kvar, även om vi kör ContentControl)
            builder.Services.AddTransient<CustomersPage>();
            builder.Services.AddTransient<CreateCustomerPage>();
            builder.Services.AddTransient<CustomerDetailPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
