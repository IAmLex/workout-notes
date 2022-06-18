using WorkoutService.Models;

namespace WorkoutService.Interfaces
{
    public interface IUserRepository
    {
        List<User> Get();
        User Get(int id);
    }
}