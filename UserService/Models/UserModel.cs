namespace UserService.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public User() { }
        
        public User(string username, string firstName, string lastName)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}