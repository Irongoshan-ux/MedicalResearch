using System.ComponentModel.DataAnnotations;

namespace UserManaging.API.DTOs.Users
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "No email provided")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "No username provided")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "No phone number provided")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "No password provided")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        public string ConfirmPassword { get; set; }
    }
}
