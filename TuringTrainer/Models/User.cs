
namespace MovieExtravaganza2.Model;

using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;


[Table("user")]
public class User : ObservableBaseModel
{
    String _user_id = "";
    String _title = "";
    int _numStars;
    int _year;

    [PrimaryKey("user_id"), Column("user_id")]
    public String UserId
    {
      get { return _user_id; }
    }

    [Column("title")]
    public String Title
    {
        get { return _title; }
        set
        {
            SetProperty(ref _title, value);
        }
    }
    [Column("year")]
    public int Year
    {
        get { return _year; }
        set
        {
            SetProperty(ref _year, value);
        }
    }
    [Column("num_stars")]
    public int NumStars
    {
        get
        {
            return _numStars;
        }
        set
        {
            if (value >= 1 && value <= 5) // Hmmm... hardcoding :-()
            {
                SetProperty(ref _numStars, value);
            }
        }
    }

    public Movie(String imdbId, String title, int year, int numStars)
    {
        ImdbId = imdbId;
        Title = title;
        Year = year;
        NumStars = numStars;
    }

    public Movie() { }
    public override String ToString()
    {
        return String.Format("Imdb_id: {0}, Title: {1}, Year: {2}, NumStars: {3}", ImdbId, Title, Year, NumStars);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Movie other = (Movie)obj;
        return ImdbId == other.ImdbId;
    }

    public override int GetHashCode()
    {
        return ImdbId.GetHashCode();
    }
}
