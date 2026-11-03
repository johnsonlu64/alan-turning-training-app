namespace TuringTrainer.Pages;
//Name: Luke Johnson

/// <summary>
/// The header at the top of a screen: a back button, the app name and the screen title.
/// </summary>
public partial class PageHeader : ContentView
{
    public PageHeader()
    {
        InitializeComponent();
    }

    /// <summary>The screen title shown under the app name.</summary>
    public string Title
    {
        get => TitleLabel.Text;
        set => TitleLabel.Text = value;
    }

    /// <summary>The short label shown under the screen title.</summary>
    public string RightLabel
    {
        get => RightLabelText.Text;
        set
        {
            RightLabelText.Text = value;
            RightLabelText.IsVisible = !string.IsNullOrEmpty(value);
        }
    }

    private async void OnBackClicked(object? sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync("..");
        }
        catch
        {
            await Shell.Current.GoToAsync(nameof(Homepage));
        }
    }
}
