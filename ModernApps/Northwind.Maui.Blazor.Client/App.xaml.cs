namespace Northwind.Maui.Blazor.Client
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell()) { Title = "Northwind.Maui.Blazor.Client" };
        }
    }
}
