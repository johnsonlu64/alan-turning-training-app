using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public class ExerciseRepository : IExerciseRepository
{

    private readonly Supabase.Client _client = Database.getClient();


    public ObservableCollection<Exercise> Exercises { get; private set; } = new();

    public async Task SelectAllExercises()
    {
        try
        {
            var response = await _client.From<Exercise>().Get();
            Exercises.Clear();

            foreach (var exercise in response.Models)
            {
                Exercises.Add(exercise);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching exercises: {ex.Message}");
        }
    }

    public async Task<Exercise?> SelectExercise(Guid exerciseId)
    {
        var response = await _client.From<Exercise>().Where(exercise => exercise.ExerciseId == exerciseId).Get();
        if (response != null)
        {
            return response!.Models.FirstOrDefault();
        }
        return null;
    }

    public async Task<ExerciseAdditionError> InsertExercise(Exercise exercise)
    {
        try
        {
            var response = await _client.From<Exercise>().Insert(exercise);
            if (response.Models.Any())
            {
                Exercises.Add(response.Models.First());
            }
            else
            {
                return ExerciseAdditionError.DuplicateExerciseId;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Insert failed, {0}", ex);
            return ex.Message.Contains("already exists") ? ExerciseAdditionError.DuplicateExerciseId : ExerciseAdditionError.DBAdditionError;
        }
        return ExerciseAdditionError.NoError;
    }

    public async Task<ExerciseEditError> UpdateExercise(Exercise exerciseToUpdate, String name)
    {
        try
        {
            exerciseToUpdate.Name = name;
            var response = await _client.From<Exercise>().Update(exerciseToUpdate);
            if (response.Models.Any())
            {
                var updatedExercise = response.Models.First();
                var index = Exercises.IndexOf(exerciseToUpdate);
                if (index >= 0)
                {
                    Exercises[index] = updatedExercise;
                }
            }
            else
            {
                return ExerciseEditError.ExerciseNotFound;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Update failed, {0}", ex);
            return ExerciseEditError.DBEditError;
        }
        return ExerciseEditError.NoError;
    }

    public async Task<ExerciseDeletionError> DeleteExercise(Exercise exerciseToDelete)
    {
        try
        {
            var response = await _client.From<Exercise>().Delete(exerciseToDelete);
            if (response.Models.Any())
            {
                Console.WriteLine($"{Exercises.Count}");
                Exercises.Remove(exerciseToDelete);
                Console.WriteLine($"{Exercises.Count}");
            }
            else
            {
                return ExerciseDeletionError.ExerciseNotFound;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Delete failed, {0}", ex);
            return ExerciseDeletionError.DBDeletionError;
        }
        return ExerciseDeletionError.NoError;
    }
}