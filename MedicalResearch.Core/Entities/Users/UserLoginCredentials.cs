namespace UserManaging.Domain.Entities.Users
{
    public class UserLoginCredentials
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public UserLoginCredentials(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
