namespace UserManaging.API
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public static class Authorization
        {
            public const string Login = Root + "/authorization/login";
            public const string Register = Root + "/authorization/register";
        }

        public static class Users
        {
            public const string FindAll = Root + "/users/findall";
            public const string Create = Root + "/users/create";
            public const string Update = Root + "/users/update";
            public const string FindByEmail = Root + "/users/findByEmail/{email}";
        }
    }
}
