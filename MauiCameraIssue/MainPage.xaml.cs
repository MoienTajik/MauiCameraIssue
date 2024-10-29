using CommunityToolkit.Maui.Views;

namespace MauiCameraIssue;

public partial class MainPage
{
    private ImageSource _capturedImage;
    public ImageSource CapturedImage
    {
        get => _capturedImage;
        set
        {
            _capturedImage = value;
            OnPropertyChanged(nameof(CapturedImage));
        }
    }

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this; // Set the BindingContext to this class
    }

    protected override async void OnAppearing()
    {
    }

    private async void OnCaptureButtonClicked(object sender, EventArgs e)
    {
        if (!CameraView.IsAvailable)
        {
            await DisplayAlert("Camera Unavailable", "Camera is not available.", "OK");
            return;
        }

        try
        {
            await CameraView.CaptureImage(CancellationToken.None);
        }
        catch
        {
            // Ignore exceptions
        }
    }

    private async void OnMediaCaptured(object? sender, MediaCapturedEventArgs e)
    {
        try
        {
            using var stream = (MemoryStream)e.Media;
            var imageData = stream.ToArray();
            
            // Update the captured image
            CapturedImage = ImageSource.FromStream(() => new MemoryStream(imageData));
        }
        catch
        {
            // Ignore exceptions
        }
    }
}