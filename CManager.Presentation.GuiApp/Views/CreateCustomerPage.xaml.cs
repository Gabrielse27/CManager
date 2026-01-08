using CManager.Application.Interfaces;
using CManager.Presentation.GuiApp.ViewModels;


namespace CManager.Presentation.GuiApp.Views;

// "public partial class" betyder att denna klass är delad i två filer.
// Del 1 är denna fil (C#-koden).
// Del 2 genereras automatiskt av MAUI utifrån din XAML-design (knappar, färger, layout).
// Vi ärver från "ContentView" vilket betyder att detta är en komponent/vy som kan visas inuti en annan sida.

// ContentView är inte en fil du själv har skapat, utan det är en inbyggd byggsten i .NET MAUI (Microsofts kod).
// Genom att ärva från ContentView talar du om för programmet att "Den här sidan är en bit grafik som kan återanvändas och visas inuti andra sidor".
public partial class CreateCustomerPage : ContentView
{

    // Konstruktorn: Körs när vi skapar en ny instans av denna vy CreateCustomerPage.
    public CreateCustomerPage()
	{

        // InitializeComponent() är en superviktig metod!
        // Den läser XAML-filen och bygger upp alla knappar och textrutor så att de syns på skärmen.
        // Utan denna rad skulle sidan vara helt tom/vit.
        InitializeComponent();
	
	}
}