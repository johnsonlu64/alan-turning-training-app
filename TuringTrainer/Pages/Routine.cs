namespace TuringTrainer.Pages;
//Name: Luke Johnson

/// <summary>
/// A routine as shown in the routine lists.
/// </summary>
public class Routine
{
    /// <summary>Stable routine id used for navigation.</summary>
    public string Id { get; set; } = "";

    /// <summary>Zero-padded ordinal badge, for example "01".</summary>
    public string Index { get; set; } = "";

    /// <summary>Routine title.</summary>
    public string Title { get; set; } = "";

    /// <summary>Short focus line, for example "Squat and hinge".</summary>
    public string Subtitle { get; set; } = "";

    /// <summary>How many exercises the routine has, for example "3 EXERCISES".</summary>
    public string ExerciseCount { get; set; } = "";

    /// <summary>How long the routine takes, for example "45 MIN".</summary>
    public string Duration { get; set; } = "";

    /// <summary>The routines shown in the routine lists.</summary>
    public static List<Routine> All { get; } = new List<Routine>
    {
        new Routine
        {
            Id = "lowerBody",
            Index = "01",
            Title = "Lower Body Strength",
            Subtitle = "Squat and hinge",
            ExerciseCount = "3 EXERCISES",
            Duration = "45 MIN",
        },
        new Routine
        {
            Id = "upperBody",
            Index = "02",
            Title = "Upper Body Strength",
            Subtitle = "Press and pull",
            ExerciseCount = "3 EXERCISES",
            Duration = "40 MIN",
        },
        new Routine
        {
            Id = "fullBody",
            Index = "03",
            Title = "Full Body Power",
            Subtitle = "Power cycle",
            ExerciseCount = "3 EXERCISES",
            Duration = "35 MIN",
        },
    };
}
