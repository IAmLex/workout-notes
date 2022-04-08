using UserService.DTOs;
using UserService.Models;

namespace UserService.Helpers 
{
    public static class UserHelper
    {
        public static UserDTO ToDTO(this User user)
        {
            return new UserDTO(user.Id, user.Username, user.Email, user.FirstName, user.LastName);
        }
    }
}