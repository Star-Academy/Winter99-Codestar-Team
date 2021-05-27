namespace Back_End.Users
{
    public class Session
    {
        public string SessionId { get; set; }

        public Session(string sessionId)
        {
            SessionId = sessionId;
        }
    }
}