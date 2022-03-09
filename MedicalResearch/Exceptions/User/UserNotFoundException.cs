namespace UserManaging.API.Exceptions.User
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base()
        {
        }

        public UserNotFoundException(string? message) : base(message)
        {
        }
    }
}
