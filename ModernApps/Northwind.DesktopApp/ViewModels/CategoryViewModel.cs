using Northwind.EntityModels;

namespace Northwind.DesktopApp.ViewModels
{
    public class CategoryViewModel : Category
    {
        public string? PicturePath
        {
            get => $"avares://Northwind.DesktopApp/Assets/category{CategoryId}-small.jpeg";
        }
    }
}