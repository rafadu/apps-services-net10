using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using System;

namespace Northwind.Maui.Blazor.Client.Views.Orders
{
    internal partial class DeviceInfoViewModel : ObservableObject
    {
        public string DisplayPixelWidth =>
         $"{DeviceDisplay.Current.MainDisplayInfo.Width} pixel width";

        public string DisplayDensity =>
          $"{DeviceDisplay.Current.MainDisplayInfo.Density} pixel density";

        public string DisplayOrientation =>
          $"Orientation is {DeviceDisplay.Current.MainDisplayInfo.Orientation}";

        public string DisplayRotation =>
          $"Rotation is {DeviceDisplay.Current.MainDisplayInfo.Rotation}";

        public string DisplayRefreshRate =>
          $"{DeviceDisplay.Current.MainDisplayInfo.RefreshRate} Hz refresh rate";

        public string DeviceModel => DeviceInfo.Current.Model;

        public string DeviceType =>
          $"{DeviceInfo.Current.DeviceType} {DeviceInfo.Current.Idiom} device";

        public string DeviceVersion => DeviceInfo.Current.VersionString;

        public string DevicePlatform =>
          $"Platform is {DeviceInfo.Current.Platform}";

        [RelayCommand]
        private async Task NavigateToAsync(string pageName)
        {
            await Shell.Current.GoToAsync($"//{pageName}");
        }

        [RelayCommand]
        private async Task PopupToastAsync()
        {
            IToast toast = Toast.Make(message: "This toast pops up.", duration: ToastDuration.Short, textSize: 18);

            try
            {
                // Show on UI thread; Windows platform toast may throw for unpackaged apps
                await MainThread.InvokeOnMainThreadAsync(async () => await toast.Show());
            }
            catch (Exception)
            {
                // Fallback to in-app Snackbar if platform toast fails (e.g., "element not found")
                try
                {
                    var snack = Snackbar.Make("This toast pops up.", action: null, duration: TimeSpan.FromSeconds(2));
                    await MainThread.InvokeOnMainThreadAsync(async () => await snack.Show());
                }
                catch
                {
                    // Swallow: nothing further to do
                }
            }
        }
    }
}
