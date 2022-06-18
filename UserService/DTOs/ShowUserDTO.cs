namespace UserService.DTOs
{
    public class ShowUserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GravatarUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
