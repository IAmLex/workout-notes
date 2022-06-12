namespace UserService.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // TODO: Add created at and updated at

        public User(string username, string firstName, string lastName)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}