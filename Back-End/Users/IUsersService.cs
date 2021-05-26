namespace Back_End.Users
{
    public interface IUsersService
    {
        public void AddUser(User user);

        public User GetUser(string userId);
    }
}