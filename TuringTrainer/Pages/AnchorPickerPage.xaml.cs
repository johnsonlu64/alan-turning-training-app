namespace TuringTrainer.Pages;
//Name: Luke Johnson

/// <summary>
/// The anchor picker. Picking an anchor hands its name back to the builder.
/// </summary>
public partial class AnchorPickerPage : ContentPage
{
    /// <summary>Raised with the name of the anchor the user picked.</summary>
    public event Action<string>? Picked;

    public AnchorPickerPage()
    {
        InitializeComponent();

        OptionsList.ItemsSource = new List<AnchorOption>
        {
            new AnchorOption { Name = "NONE", Detail = "SETS PERCENT OF ITS OWN 1RM" },
            new AnchorOption { Name = "BACK SQUAT", Detail = "90% OF 315 LB" },
            new AnchorOption { Name = "BENCH PRESS", Detail = "90% OF 225 LB" },
            new AnchorOption { Name = "POWER CLEAN", Detail = "90% OF 155 LB" },
        };
    }

    private async void OnPickClicked(object? sender, EventArgs e)
    {
        if (((Button?)sender)?.CommandParameter is not AnchorOption option)
            return;

        Picked?.Invoke(option.Name);

        await Navigation.PopModalAsync();
    }

    private async void OnCancelClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}

/// <summary>
/// A row in the anchor picker.
/// </summary>
public class AnchorOption
{
    /// <summary>Anchor name, for example "BACK SQUAT".</summary>
    public string Name { get; set; } = "";

    /// <summary>How the anchor's working max reads out.</summary>
    public string Detail { get; set; } = "";
}
