using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace TuringTrainer;
// dasha
public partial class MetricsPage : ContentPage
{
    public MetricsPage()
    {
        InitializeComponent();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }


    // When a item from the drop down is selected this method is triggered and
    // fills in the weights depending on which exercise was selected calls 
    // update chart method
    private void ExercisePicker_SelectedIndexChanged(object? sender, EventArgs e)
    {
        double[] weights;

        if (ExercisePicker.SelectedIndex == 0)
        {
            weights = new double[] { 145, 155, 160, 175, 185 };
        }
        else if (ExercisePicker.SelectedIndex == 1)
        {
            weights = new double[] { 225, 235, 245, 260, 275 };
        }
        else if (ExercisePicker.SelectedIndex == 2)
        {
            weights = new double[] { 265, 275, 290, 300, 315 };
        }
        else
        {
            return;  // if nothing is selected yet don't display anything currently
        }

        UpdateChart(weights);
        
    }

    // This method takes in the weight points for the chart and uses those to make a 
    // line then sets the xasis as custom month names. The y axis is automatically 
    // made based off the values in the LineSeries
    private void UpdateChart(double[] weights)
    {
        StrengthChart.Series = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = weights,
            }
        };
        
        StrengthChart.XAxes = new Axis[]
        {
            new Axis
            {
                Labels = new string[] {"Sept", "Oct", "Nov", "Dec", "Jan" }
            }
        };
    }

}
