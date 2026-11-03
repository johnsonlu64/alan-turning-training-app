using System.Collections.ObjectModel;
//Name: Luke Johnson

using System.ComponentModel;

namespace TuringTrainer.Pages;

/// <summary>
/// One exercise in a routine, as shown in the builder's exercise list.
/// </summary>
public class BuilderExercise
{
    /// <summary>Zero-padded ordinal badge, for example "01".</summary>
    public string Index { get; set; } = "";

    /// <summary>Exercise name.</summary>
    public string Name { get; set; } = "";

    /// <summary>Short sub-line, for example "PRIMARY 90% OF 1RM".</summary>
    public string Focus { get; set; } = "";

    /// <summary>The load of the working max, used to work out the load of each set.</summary>
    public double BaseLoad { get; set; }

    /// <summary>The working sets of this exercise.</summary>
    public ObservableCollection<BuilderSet> Sets { get; set; } = new ObservableCollection<BuilderSet>();
}

/// <summary>
/// One working set of an exercise. It tells the list when a value changes, so the
/// screen shows the new number straight away.
/// </summary>
public class BuilderSet : INotifyPropertyChanged
{
    private int _percent = 70;
    private int _reps = 5;
    private string _load = "";

    /// <summary>Set label, for example "SET 1".</summary>
    public string Label { get; set; } = "";

    /// <summary>The load of the working max, used to work out the load of this set.</summary>
    public double BaseLoad { get; set; }

    /// <summary>Intensity of the set, for example 70.</summary>
    public int Percent
    {
        get => _percent;
        set
        {
            _percent = value;
            UpdateLoad();
            Changed();
        }
    }

    /// <summary>Repetition count of the set, for example 5.</summary>
    public int Reps
    {
        get => _reps;
        set
        {
            _reps = value;
            Changed();
        }
    }

    /// <summary>Load of the set, for example "198.5 LB".</summary>
    public string Load
    {
        get => _load;
        set
        {
            _load = value;
            Changed();
        }
    }

    /// <summary>The intensity as it is shown, for example "70%".</summary>
    public string PercentText => Percent + "%";

    /// <summary>The reps as they are shown, for example "x 5".</summary>
    public string RepsText => "x " + Reps;

    /// <summary>Raised when a value changes.</summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>Works out the load of this set from the percent and the working max.</summary>
    public void UpdateLoad()
    {
        double load = BaseLoad * Percent / 100.0;
        Load = Math.Round(load, 1, MidpointRounding.AwayFromZero) + " LB";
    }

    private void Changed()
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
    }
}
