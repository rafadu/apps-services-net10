namespace Northwind.Maui.Blazor.Client.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        // Register Blazor root component in code so XAML doesn't need to resolve the Razor-generated type
        blazorWebView.RootComponents.Add(new Microsoft.AspNetCore.Components.WebView.Maui.RootComponent()
        {
            Selector = "#app",
            ComponentType = typeof(Components.Routes)
        });
    }
}
