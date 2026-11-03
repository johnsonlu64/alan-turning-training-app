namespace TuringTrainer.Pages;
//Name: Luke Johnson

/// <summary>
/// The notification inbox. The items are literal content; the ones that name a routine
/// open its builder, the rest are informational.
/// </summary>
public partial class NotificationsPage : ContentPage
{
    public NotificationsPage()
    {
        InitializeComponent();

        List<NotificationItem> items = new List<NotificationItem>
        {
            new NotificationItem
            {
                Badge = "PR",
                BadgeFill = "#332B12",
                BadgeColor = "#F5C542",
                Headline = "Alex Morgan hit a new deadlift PR",
                Detail = "180 lb, up from 175 lb",
                Time = "12 MIN AGO",
                TypeWord = "NEW PR",
            },
            new NotificationItem
            {
                Badge = "RT",
                BadgeFill = "#2A2408",
                BadgeColor = "#DEB200",
                Headline = "Coach Rivera updated Upper Body Strength",
                Detail = "2 exercises changed, sets reduced on Bench Press",
                Time = "3 H AGO",
                TypeWord = "ROUTINE",
                RoutineId = "upperBody",
            },
            new NotificationItem
            {
                Badge = "RM",
                BadgeFill = "#232323",
                BadgeColor = "#FFFFFF",
                Headline = "Lower Body Strength is scheduled today",
                Detail = "3 exercises, about 45 minutes, starts 4:00 PM",
                Time = "5 H AGO",
                TypeWord = "REMINDER",
                RoutineId = "lowerBody",
            },
        };

        NotificationList.ItemsSource = items;
        UnreadCountLabel.Text = "2 NEW";
    }

    private async void OnNotificationTapped(object? sender, TappedEventArgs e)
    {
        if (((Border?)sender)?.BindingContext is not NotificationItem item)
            return;

        if (item.RoutineId == "")
        {
            return;
        }

        await Shell.Current.GoToAsync($"{nameof(RoutineBuilderPage)}?routineId={item.RoutineId}");
    }

    private void OnMarkAllReadClicked(object? sender, EventArgs e)
    {
        // The inbox is a visual mock-up, so marking the items read changes nothing.
    }
}

/// <summary>
/// A row in the notification inbox.
/// </summary>
public class NotificationItem
{
    /// <summary>Two-letter badge shown in the tile, for example "PR".</summary>
    public string Badge { get; set; } = "";

    /// <summary>Background colour of the badge tile.</summary>
    public string BadgeFill { get; set; } = "";

    /// <summary>Text colour of the badge tile.</summary>
    public string BadgeColor { get; set; } = "";

    /// <summary>Headline of the notification.</summary>
    public string Headline { get; set; } = "";

    /// <summary>Second line of the notification.</summary>
    public string Detail { get; set; } = "";

    /// <summary>Relative timestamp, for example "12 MIN AGO".</summary>
    public string Time { get; set; } = "";

    /// <summary>Type of the notification, for example "NEW PR".</summary>
    public string TypeWord { get; set; } = "";

    /// <summary>Routine to open, or empty when the notification is informational.</summary>
    public string RoutineId { get; set; } = "";
}
