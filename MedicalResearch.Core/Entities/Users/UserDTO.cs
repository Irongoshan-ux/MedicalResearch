using UserManaging.Domain.Entities.Images;

namespace UserManaging.Domain.Entities.Users
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
