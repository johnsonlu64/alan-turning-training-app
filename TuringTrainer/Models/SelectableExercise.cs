using System.ComponentModel;

namespace TuringTrainer.Models;

public class SelectableExercise : INotifyPropertyChanged
{
    private bool _isSelected;

    public SelectableExercise(Exercise exercise)
    {
        Exercise = exercise;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public Exercise Exercise { get; }

    public string Name => Exercise.Name;

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected == value)
                return;

            _isSelected = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
        }
    }
}
