using System.Collections.ObjectModel;
namespace TuringTrainer.Services;

public class Database : IDatabase
{
    private static System.Random rng = new();
    private static string url = "https://llbxqwnjlcotarcnwxtm.supabase.co";
    private static string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImxsYnhxd25qbGNvdGFyY253eHRtIiwicm9sZSI6ImFub24iLCJpYXQiOjE3OTMxMzUwNTAsImV4cCI6MjEwODcxMTA1MH0.GfT4CWb6oCvEVbWyOJHTfCUGTPJut6wXOnGjiQkf95E";
    private Supabase.Client client;

    public Database()
    {
        client = new Supabase.Client(url, key);
    }

}
