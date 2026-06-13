using System.Collections.ObjectModel;
using System.Linq;
using Northwind.DesktopApp.ViewModels;
using Northwind.EntityModels;

namespace Northwind.DesktopApp.Services;

/// <summary>
/// Handles loading category data from the Northwind database.
/// </summary>
public static class CategoryDataProvider
{
    public static void LoadCategories(ObservableCollection<CategoryViewModel> categories)
    {
        using NorthwindContext db = new();
        CategoryViewModel[]? dbCategories = db.Categories.Select(c => new CategoryViewModel
        {
            CategoryId = c.CategoryId,
            CategoryName = c.CategoryName,
            Description = c.Description,
            Picture = c.Picture,
            Products = c.Products
        }).ToArray();

        if (dbCategories is null || dbCategories.Length == 0)
            return;

        foreach (var category in dbCategories)
        {
            categories.Add(category);
        }
    }
}
