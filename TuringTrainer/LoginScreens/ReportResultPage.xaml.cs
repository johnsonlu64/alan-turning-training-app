using System.Text;
using TuringTrainer.Models;

namespace TuringTrainer;

/// <summary>Shows a generated report and lets the coach export it as a CSV file.</summary>
public partial class ReportResultsPage : ContentPage, IQueryAttributable
{
    private ReportResult? _report;

    public ReportResultsPage()
    {
        InitializeComponent();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("report", out object? value) && value is ReportResult report)
        {
            _report = report;
            Show(report);
        }
    }

    private void Show(ReportResult report)
    {
        TitleLabel.Text = report.Title;
        RangeLabel.Text = $"{report.Start:MMM d, yyyy} – {report.End:MMM d, yyyy}";

        CompletionValue.Text = report.OverallPercent;
        AthletesValue.Text = report.Athletes.Count.ToString();
        AssignedValue.Text = report.TotalAssigned.ToString();
        CompletedValue.Text = report.TotalCompleted.ToString();
        PartialValue.Text = report.TotalPartial.ToString();
        MissedValue.Text = report.TotalMissed.ToString();

        BindableLayout.SetItemsSource(AthleteLayout, report.Athletes);
        EmptyLabel.IsVisible = report.Athletes.Count == 0;

        BindableLayout.SetItemsSource(PartialsLayout, report.Partials);
        PartialsHeader.IsVisible = report.Partials.Count > 0;
    }

    private async void OnExportClicked(object? sender, EventArgs e)
    {
        if (_report == null)
            return;

        try
        {
            string fileName = $"report_{_report.Start:yyyyMMdd}_{_report.End:yyyyMMdd}.csv";
            string path = Path.Combine(FileSystem.CacheDirectory, fileName);
            await File.WriteAllTextAsync(path, BuildCsv(_report), new UTF8Encoding(true));

            await Share.Default.RequestAsync(new ShareFileRequest
            {
                Title = "Export report",
                File = new ShareFile(path),
            });
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Export failed", ex.Message, "OK");
        }
    }

    private static string BuildCsv(ReportResult report)
    {
        StringBuilder csv = new();
        csv.AppendLine($"{Escape(report.Title)},{report.Start:yyyy-MM-dd} to {report.End:yyyy-MM-dd}");
        csv.AppendLine();
        csv.AppendLine("Athlete,Assigned,Completed,Partial,Missed,Completion");

        foreach (AthleteReportRow a in report.Athletes)
        {
            csv.AppendLine($"{Escape(a.FullName)},{a.Assigned},{a.Completed},{a.Partial},{a.Missed},{a.PercentText}");
        }

        if (report.Partials.Count > 0)
        {
            csv.AppendLine();
            csv.AppendLine("Partial workouts");
            csv.AppendLine("Athlete,Date,Routine,Reason");

            foreach (PartialReasonRow p in report.Partials)
            {
                csv.AppendLine($"{Escape(p.FullName)},{p.WorkoutDate:yyyy-MM-dd},{Escape(p.RoutineName)},{Escape(p.Reason)}");
            }
        }

        return csv.ToString();
    }

    /// <summary>Quotes a CSV field when needed, and defuses spreadsheet formulas.</summary>
    private static string Escape(string? value)
    {
        string text = value ?? "";

        if (text.Length > 0 && "=+-@".Contains(text[0]))
            text = "'" + text;

        if (text.Contains(',') || text.Contains('"') || text.Contains('\n') || text.Contains('\r'))
            text = "\"" + text.Replace("\"", "\"\"") + "\"";

        return text;
    }
}