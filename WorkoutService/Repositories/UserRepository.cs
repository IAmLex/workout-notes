using WorkoutService.Contexts;
using WorkoutService.Interfaces;
using WorkoutService.Models;

namespace WorkoutService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WorkoutContext _context;

        public UserRepository(WorkoutContext context)
        {
            _context = context;
        }

        public List<User> Get()
        {
            throw new NotImplementedException();
        }

        public User? Get(int id)
        {
            return _context.Users.Find(id);
        }
    }
}