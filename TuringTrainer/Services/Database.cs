namespace TuringTrainer.Services;

public static class Database
{
    private static System.Random rng = new();
    private static string url = "https://llbxqwnjlcotarcnwxtm.supabase.co";
    private static string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImxsYnhxd25qbGNvdGFyY253eHRtIiwicm9sZSI6ImFub24iLCJpYXQiOjE3OTMxMzUwNTAsImV4cCI6MjEwODcxMTA1MH0.GfT4CWb6oCvEVbWyOJHTfCUGTPJut6wXOnGjiQkf95E";
    private static readonly Supabase.Client client = new(url, key);
    public static Supabase.Client getClient()
    {
        return client;
    }
}
