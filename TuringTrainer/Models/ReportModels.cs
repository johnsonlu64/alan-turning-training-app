using System.Text.Json.Serialization;

namespace TuringTrainer.Models;

/// One athlete's line in a report. (a row returned by athlete_report)
public class AthleteReportRow
{
    [JsonPropertyName("user_id")] public Guid UserId { get; set; }
    [JsonPropertyName("first_name")] public string? FirstName { get; set; }
    [JsonPropertyName("last_name")] public string? LastName { get; set; }
    [JsonPropertyName("assigned_count")] public int Assigned { get; set; }
    [JsonPropertyName("completed_count")] public int Completed { get; set; }
    [JsonPropertyName("partial_count")] public int Partial { get; set; }
    [JsonPropertyName("missed_count")] public int Missed { get; set; }

    [JsonIgnore]
    public string FullName => $"{FirstName} {LastName}".Trim();

    /// <Completed workouts divided by assigned workouts, 0 to 1.
    [JsonIgnore]
    public double Progress => Assigned == 0 ? 0 : (double)Completed / Assigned;

    [JsonIgnore]
    public string PercentText => Assigned == 0 ? "–" : $"{Math.Round(Progress * 100)}%";

    [JsonIgnore]
    public string Summary => Assigned == 0
        ? "No workouts assigned in this period"
        : $"{Completed} completed  •  {Partial} partial  •  {Missed} missed  (of {Assigned})";
}

///A student's reason for a partial workout (a row returned by report_partials
public class PartialReasonRow
{
    [JsonPropertyName("first_name")] public string? FirstName { get; set; }
    [JsonPropertyName("last_name")] public string? LastName { get; set; }
    [JsonPropertyName("workout_date")] public DateTime WorkoutDate { get; set; }
    [JsonPropertyName("routine_name")] public string? RoutineName { get; set; }
    [JsonPropertyName("reason")] public string? Reason { get; set; }

    [JsonIgnore]
    public string FullName => $"{FirstName} {LastName}".Trim();

    [JsonIgnore]
    public string Heading => $"{FullName}  •  {WorkoutDate:MMM d}  •  {RoutineName}";

    [JsonIgnore]
    public string ReasonText => string.IsNullOrWhiteSpace(Reason) ? "(no reason given)" : Reason!;
}

/// Everything the results page needs to show and export a report
public class ReportResult
{
    public string Title { get; set; } = "";
    public DateOnly Start { get; set; }
    public DateOnly End { get; set; }
    public List<AthleteReportRow> Athletes { get; set; } = new();
    public List<PartialReasonRow> Partials { get; set; } = new();

    public int TotalAssigned => Athletes.Sum(a => a.Assigned);
    public int TotalCompleted => Athletes.Sum(a => a.Completed);
    public int TotalPartial => Athletes.Sum(a => a.Partial);
    public int TotalMissed => Athletes.Sum(a => a.Missed);

    public string OverallPercent => TotalAssigned == 0
        ? "–"
        : $"{Math.Round(100.0 * TotalCompleted / TotalAssigned)}%";
}
