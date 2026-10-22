namespace TuringTrainer.Pages;
//Name: Luke Johnson

/// <summary>
/// The exercise library. Picking an exercise hands its name back to the builder.
/// </summary>
public partial class AddExercisePage : ContentPage
{
    /// <summary>Raised with the name of the exercise the user picked.</summary>
    public event Action<string>? Picked;

    public AddExercisePage()
    {
        InitializeComponent();

        LibraryList.ItemsSource = new List<LibraryEntry>
        {
            new LibraryEntry { Name = "Back Squat", Category = "Squat" },
            new LibraryEntry { Name = "Front Squat", Category = "Squat" },
            new LibraryEntry { Name = "Romanian Deadlift", Category = "Deadlift" },
            new LibraryEntry { Name = "Bench Press", Category = "Bench" },
            new LibraryEntry { Name = "Overhead Press", Category = "Bench" },
            new LibraryEntry { Name = "Power Clean", Category = "Olympic" },
        };
    }

    private async void OnAddClicked(object? sender, EventArgs e)
    {
        if (((Button?)sender)?.CommandParameter is not LibraryEntry entry)
            return;

        Picked?.Invoke(entry.Name);

        await Navigation.PopModalAsync();
    }

    private async void OnCancelClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}

/// <summary>
/// A row in the exercise library.
/// </summary>
public class LibraryEntry
{
    /// <summary>Exercise name.</summary>
    public string Name { get; set; } = "";

    /// <summary>Category the exercise belongs to.</summary>
    public string Category { get; set; } = "";
}
