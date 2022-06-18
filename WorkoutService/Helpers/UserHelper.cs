using WorkoutService.DTOs;
using WorkoutService.Models;

namespace WorkoutService.Helpers
{
    public class UserHelper
    {
        public static ShowUserDTO ToDTO(User user)
        {
            return new ShowUserDTO
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}