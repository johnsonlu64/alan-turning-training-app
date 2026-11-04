using TuringTrainer.BusinessLogic;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer;
/*
 * Name : Bakary Ceesay
 * This is the code-behind file for the AddClass.xaml page.
 * It contains the logic for handling user interactions on the AddClass page.
 */
public partial class AddClass : ContentPage
{
    private readonly IUserService _userService = new UserService(new UserRepository());
    private readonly ClassRepository _classRepository = new();

    public AddClass()
    {
        InitializeComponent();
    }

    private async void OnAddClassClicked(object? sender, EventArgs e)
    {
        string className = ClassNameEntry.Text?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(className))
        {
            await DisplayAlertAsync("Class name required", "Enter a name for this class.", "OK");
            return;
        }

        if (!Guid.TryParse(_userService.CurrentUserId(), out Guid coachId))
        {
            await DisplayAlertAsync("Sign in required", "Sign in before creating a class.", "OK");
            return;
        }

        CreateClassButton.IsEnabled = false;
        try
        {
            await _classRepository.CreateClassAsync(className, coachId);
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Unable to create class", ex.Message, "OK");
        }
        finally
        {
            CreateClassButton.IsEnabled = true;
        }
    }

    private async void OnCancelClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
