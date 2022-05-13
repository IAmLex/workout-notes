using WorkoutService.Models;

namespace WorkoutService.Interfaces
{
    public interface IExerciseRepository
    {
        List<Exercise> Get();
        Exercise Get(int id);
        Exercise Post(Exercise exercise);
    }
}