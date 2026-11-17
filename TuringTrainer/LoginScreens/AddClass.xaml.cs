namespace TuringTrainer;

/*
 * Name : Bakary Ceesay
 * This is the code-behind file for the AddClass.xaml page.
 * It contains the logic for handling user interactions on the AddClass page.
 */
public partial class AddClass : ContentPage
{
    private static readonly Guid CoachId = Guid.Parse("4daa0811-e8c6-431a-b4f8-21b05ce2d78f");
    private readonly Services.Repositories.IClassRepository _classRepository = new Services.Repositories.ClassRepository();

    public AddClass()
    {
        InitializeComponent();
    }

    private async void OnAddClassClicked(object sender, EventArgs e)
    {
        string className = ClassNameEntry.Text?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(className))
        {
            await DisplayAlert("Error", "Class name cannot be empty.", "OK");
            return;
        }

        try
        {
            var newClass = new Models.Class(Guid.NewGuid(), CoachId, className);
            await _classRepository.AddClassAsync(newClass);
            await DisplayAlert("Success", "Class added successfully.", "OK");
            ClassNameEntry.Text = "";
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not add class: {ex.Message}", "OK");
        }
    }
}
