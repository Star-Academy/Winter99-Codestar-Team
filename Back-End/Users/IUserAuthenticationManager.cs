namespace Back_End.Users
{
    public interface IUserAuthenticationManager
    {
        string GenerateSessionId();
        string GenerateSalt();
        string GenerateHash(string pass, string salt);
    }
}