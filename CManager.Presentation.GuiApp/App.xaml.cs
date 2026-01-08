using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace CManager.Presentation.GuiApp;

// "partial class" betyder att denna fil hänger ihop med "App.xaml" (där man ofta lägger globala stilar/färger).
// Vi ärver från "Application", vilket betyder att detta objekt representerar hela den körande appen.
public partial class App : Microsoft.Maui.Controls.Application

{
    // Vi skapar ett privat fält för att spara referensen till vår Huvudsida (MainPage).
    // readonly: betyder att vi bara får sätta värdet en gång (i konstruktorn).
    private readonly MainPage _mainPage;

    // Konstruktorn: Körs exakt en gång när du klickar på app-ikonen.
    // Här använder vi Dependency Injection igen! Vi ber om att få en färdig "MainPage" inskickad.
    public App(MainPage mainPage)
    {
        // Laddar in resurser (t.ex. färger och fonter) från App.xaml.
        InitializeComponent();

        // Vi sparar den inskickade sidan i vårt fält så vi kan använda den strax nedanför.
        _mainPage = mainPage;
    }

     // Den bestämmer hur själva App-fönstret ska se ut när det öppnas.
    // Vi "överskuggar" (override) standardmetoden för att lägga in vår egen logik.
    protected override Window CreateWindow(IActivationState? activationState)
    {
        // Här skapar vi det fysiska fönstret som användaren ser.
        // Vi stoppar in vår "_mainPage" i fönstret så att det är den som visas först.
        return new Window(_mainPage);
    }
}
