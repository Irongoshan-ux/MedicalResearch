using UserManaging.Domain.Entities.Users;

namespace UserManaging.API.DTOs.AuthModel
{
    public class AuthModel
    {
        public string Token { get; set; }
        public UserDTO User { get; set; }

        public AuthModel(string token, UserDTO user)
        {
            Token = token;
            User = user;
        }
    }
}
