namespace Northwind.Maui.Blazor.Client.Views;

public partial class EmployeesPage : ContentPage
{
	public EmployeesPage()
	{
		InitializeComponent();
	}

    private async void CopyToClibpboardButton_Clicked(object sender, EventArgs e)
    {
        await Clipboard.Default.SetTextAsync(NotesTextBox.Text);
    }

    private async void PasteToClipboardButton_Clicked(object sender, EventArgs e)
    {
        if (Clipboard.HasText)
        {
            NotesTextBox.Text = await Clipboard.GetTextAsync();
        }
    }

    private async void PickTextFileButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            FilePickerFileType textFileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.iOS, new[] { "public.plain-text" } }, // Uniform Type Identifier for plain text files
                { DevicePlatform.Android, new[] { "text/plain" } }, // MIME type for plain text files
                { DevicePlatform.WinUI, new[] { ".txt" } }, // File extension for plain text files
                { DevicePlatform.Tizen, new[] { "*/*" } },
                { DevicePlatform.macOS, new[] { "txt" } }, // MIME type for plain text files
            });

            PickOptions options = new PickOptions()
            {
                PickerTitle = "Pick a text file",
                FileTypes = textFileTypes
            };

            FileResult result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                await using var stream = await result.OpenReadAsync();
                FileContentsLabel.Text = new StreamReader(stream).ReadToEnd();
            }
            FilePathLabel.Text = result?.FullPath;
        }
        catch (Exception ex) 
        {
            await DisplayAlertAsync(title: "Exception", message: ex.Message, cancel: "OK");
        }
    }

    private async void PickImageButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Use FilePicker to pick a single image to avoid platform-specific MediaPicker issues on Android
            FileResult photo = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Select a photo",
                FileTypes = FilePickerFileType.Images
            });

            if (photo == null)
            {
                await DisplayAlertAsync(title: "No photo", message: "No photo was picked.", cancel: "OK");
                return;
            }

            if (!string.IsNullOrEmpty(photo.FullPath))
            {
                FileImage.Source = ImageSource.FromFile(photo.FullPath);
                FilePathLabel.Text = photo.FullPath;
            }
            else
            {
                // Some providers don't expose a FullPath on Android; load into memory stream instead
                await using var input = await photo.OpenReadAsync();
                var ms = new System.IO.MemoryStream();
                await input.CopyToAsync(ms);
                ms.Position = 0;
                FileImage.Source = ImageSource.FromStream(() => ms);
                FilePathLabel.Text = "(stream)";
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync(title: "Exception", message: ex.Message, cancel: "OK");
        }
    }

    private async void TakePhotoButton_Clicked(object sender, EventArgs e)
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            try
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
                if (photo == null)
                {
                    await DisplayAlertAsync(title: "No photo", message: "Photo was not captured.", cancel: "OK");
                    return;
                }

                if (!string.IsNullOrEmpty(photo.FullPath))
                {
                    FileImage.Source = ImageSource.FromFile(photo.FullPath);
                    FilePathLabel.Text = photo.FullPath;
                }
                else
                {
                    await using var input = await photo.OpenReadAsync();
                    var ms = new System.IO.MemoryStream();
                    await input.CopyToAsync(ms);
                    ms.Position = 0;
                    FileImage.Source = ImageSource.FromStream(() => ms);
                    FilePathLabel.Text = "(stream)";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync(title: "Exception", message: ex.Message, cancel: "OK");
            }
        }
        else
        {
            await DisplayAlertAsync(title: "Sorry", message: "Image capture is not supported on this device.", cancel: "OK");
        }
    }
}