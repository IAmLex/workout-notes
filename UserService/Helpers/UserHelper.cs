using UserService.DTOs;
using UserService.Models;

namespace UserService.Helpers 
{
    public static class UserHelper
    {
        public static UserDTO ToDTO(User user)
        {
            return new UserDTO(user.Id, user.Username, user.Email, user.FirstName, user.LastName);
        }

        public static ShowUserDTO ToShowDTO(User user)
        {
            return new ShowUserDTO() {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt
            };
        }
    }
}