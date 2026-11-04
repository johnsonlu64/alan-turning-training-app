using TuringTrainer.Models;

namespace TuringTrainer;

public partial class ClassDetailsPage : ContentPage, IQueryAttributable
{
    public ClassDetailsPage()
    {
        InitializeComponent();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("Class", out object? value) ||
            value is not TrainingClass trainingClass)
        {
            throw new ArgumentException("A class is required to show class details.", nameof(query));
        }

        ClassNameLabel.Text = trainingClass.Name;
        ClassIdLabel.Text = trainingClass.ClassId.ToString();
        GroupIdLabel.Text = trainingClass.GroupId?.ToString() ?? "None";
        CoachIdLabel.Text = trainingClass.CoachId.ToString();
    }

    private async void OnBackClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
