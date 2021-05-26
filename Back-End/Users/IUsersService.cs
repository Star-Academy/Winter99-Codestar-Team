namespace Back_End.Users
{
    public interface IUsersService
    {
        public void AddUser(User user);

        public User GetUser(string userId);

        public bool Exists(string field, string value);

        public bool CheckUser(string userId, string password);
    }
}