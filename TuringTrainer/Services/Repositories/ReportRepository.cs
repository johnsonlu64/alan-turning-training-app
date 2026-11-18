using System.Text.Json;
using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

/// <summary>Loads report data by calling the athlete_report and report_partials functions in Supabase.</summary>
public class ReportRepository
{
    private readonly Supabase.Client _client = Database.getClient();

    public async Task<ReportResult> BuildReportAsync(string title, Guid? classId, Guid? sportId, DateOnly start, DateOnly end)
    {
        var args = new Dictionary<string, object?>
        {
            ["p_class_id"] = classId?.ToString(),
            ["p_sport_id"] = sportId?.ToString(),
            ["p_start"] = start.ToString("yyyy-MM-dd"),
            ["p_end"] = end.ToString("yyyy-MM-dd"),
        };

        var athletesResponse = await _client.Rpc("athlete_report", args);
        var partialsResponse = await _client.Rpc("report_partials", args);

        return new ReportResult
        {
            Title = title,
            Start = start,
            End = end,
            Athletes = Parse<AthleteReportRow>(athletesResponse.Content),
            Partials = Parse<PartialReasonRow>(partialsResponse.Content),
        };
    }

    private static List<T> Parse<T>(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return new List<T>();

        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }
}
