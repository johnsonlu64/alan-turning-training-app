using System.Collections.ObjectModel;
namespace MovieExtravaganza2.Infrastructure;

public class Database : IDatabase
{
    private static System.Random rng = new();
    private static string url = "https://vgkqpjftsjqugpajftfd.supabase.co";
    private static string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZna3FwamZ0c2pxdWdwYWpmdGZkIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTQzMzkzOTcsImV4cCI6MjA2OTkxNTM5N30.qYueHJ5naZg2LlS0qus9dF9qXCdT7R9Wg8oURM9zo-Q";
    private Supabase.Client client;

    public Database()
    {
        client = new Supabase.Client(url, key);
    }

}
