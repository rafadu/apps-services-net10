using Avalonia.Controls;
using Northwind.DesktopApp.Services;
using Northwind.EntityModels;
using System.Collections.ObjectModel;

namespace Northwind.DesktopApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Apps and Services with .NET 10 with Avalonia!";
    public ObservableCollection<CategoryViewModel> Categories { get; } = [];
    public ObservableCollection<Product> Products { get; } = [];

    private Category? _selectedCategory;
    public Category? SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            _selectedCategory = value;
            Products.Clear();
            if (value != null)
            {
                foreach (var product in value.Products)
                    Products.Add(product);
            }
        }
    }

    public MainWindowViewModel()
    {
        LoadData();
    }

    private void LoadData()
    {
        if (Design.IsDesignMode)
        {
            DesignTimeDataProvider.LoadSampleCategories(Categories);
        }
        else
        {
            CategoryDataProvider.LoadCategories(Categories);
        }

        if (Categories.Count > 0)
        {
            SelectedCategory = Categories[0];
        }
    }
}
