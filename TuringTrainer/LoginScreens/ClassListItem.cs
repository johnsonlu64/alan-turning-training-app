using TuringTrainer.Models;

namespace TuringTrainer;

public sealed class ClassListItem
{
    public TrainingClass TrainingClass { get; }
    public string Name => TrainingClass.Name;
    public string DisplayNumber { get; }

    public ClassListItem(TrainingClass trainingClass, int displayNumber)
    {
        TrainingClass = trainingClass;
        DisplayNumber = displayNumber.ToString("D2");
    }
}
