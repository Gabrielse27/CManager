using CManager.Application.Helpers;
using CManager.Application.Interfaces;
using CManager.Application.Services;
using CManager.Domain;
using CManager.Infrastructure;
using CManager.Infrastructure.Repositories;
using CManager.Presentation.GuiApp.ViewModels;
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




            builder.Services.AddSingleton<GuidFactory>();
            builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
            builder.Services.AddSingleton<ICustomerService, CustomerService>();

            builder.Services.AddSingleton<Views.CustomersPage>();
            builder.Services.AddSingleton<CustomersPageViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
