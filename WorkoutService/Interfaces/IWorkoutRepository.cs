using WorkoutService.Models;

namespace WorkoutService.Interfaces
{
    public interface IWorkoutRepository
    {
        List<Workout> Get();
        Workout? Get(int id);
        Workout Post(Workout workout);
    }
}