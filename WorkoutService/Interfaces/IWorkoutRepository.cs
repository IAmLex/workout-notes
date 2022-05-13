using WorkoutService.Models;

namespace WorkoutService.Interfaces
{
    public interface IWorkoutRepository
    {
        List<Workout> Get();
        Workout Post(Workout workout);
    }
}