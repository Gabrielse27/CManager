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
    // "static" betyder att vi inte behöver skapa en instans av denna klass ("new MauiProgram()").
    // Den innehåller start-metoden som operativsystemet (Android/Windows) anropar.
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            // Skapar en "Builder". Det är ett verktyg som vi använder för att konfigurera appen steg för steg.
            var builder = MauiApp.CreateBuilder();

            // Berättar vilken klass som är själva APPEN (startpunkten). 
            // "App" är filen App.xaml.cs som vi kollade på tidigare.
            builder
                .UseMauiApp<App>()

                // Konfigurerar typsnitt så att vi kan använda dem i XAML.
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold", "OpenSansSemibold");
                });

            // --- DEPENDENCY INJECTION (DI) KONFIGURATION ---
            // Här registrerar vi alla klasser så att appen kan skapa dem automatiskt åt oss.

            // "AddSingleton": Skapar EN enda instans (objekt) som lever hela tiden appen är igång.
            // Om två olika sidor ber om "ICustomerRepository", får de exakt samma objekt.
            // Detta är viktigt för att listan med kunder ska vara likadan överallt.
            builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
            builder.Services.AddSingleton<ICustomerService, CustomerService>();


            // MainViewModel är "trafikpolisen", den måste finnas.
            // MainViewModel måste vara Singleton eftersom den håller koll på "CurrentViewModel".
            // Om vi skapade en ny MainViewModel varje gång, skulle navigeringen nollställas.
            builder.Services.AddSingleton<MainViewModel>();


            // MainPage är vårt huvudfönster, det ska också bara finnas ett av.
            builder.Services.AddSingleton<MainPage>();

            // "AddTransient": Skapar ett NYTT objekt varje gång någon ber om det.
            // Detta är bra för undersidor och deras ViewModels. 
            // När vi lämnar sidan kan minnet städas, och nästa gång vi går dit får vi en fräsch sida.
            builder.Services.AddTransient<CustomersPageViewModel>();
            builder.Services.AddTransient<CreateCustomerViewModel>();
            builder.Services.AddTransient<CustomerDetailViewModel>();



            // Vi registrerar även Vyerna (Views) som Transient.
            builder.Services.AddTransient<CustomersPage>();
            builder.Services.AddTransient<CreateCustomerPage>();
            builder.Services.AddTransient<CustomerDetailPage>();

            // "#if DEBUG" betyder att denna kod bara körs när vi utvecklar, inte när appen släpps till kunder.
#if DEBUG

            // Lägger till loggning så vi kan se felmeddelanden i Visual Studios "Output"-fönster.
            builder.Logging.AddDebug();
#endif
            // Bygg klart appen med alla inställningar och returnera den till systemet.
            return builder.Build();
        }
    }
}
