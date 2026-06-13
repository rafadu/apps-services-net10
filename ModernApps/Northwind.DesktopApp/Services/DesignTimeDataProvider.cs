using System.Collections.ObjectModel;
using Northwind.DesktopApp.ViewModels;

namespace Northwind.DesktopApp.Services;

/// <summary>
/// Provides sample data for design-time (previewer) use in Avalonia.
/// </summary>
public static class DesignTimeDataProvider
{
    public static void LoadSampleCategories(ObservableCollection<CategoryViewModel> categories)
    {
        var sampleCategory = new CategoryViewModel
        {
            CategoryId = 1,
            CategoryName = "Beverages",
            Description = "Soft drinks, coffees, teas, beers, and ales"
        };
        categories.Add(sampleCategory);

        var sampleCategory2 = new CategoryViewModel
        {
            CategoryId = 2,
            CategoryName = "Condiments",
            Description = "Sweet and savory sauces, relishes, spreads, and seasonings"
        };
        categories.Add(sampleCategory2);
    }
}
