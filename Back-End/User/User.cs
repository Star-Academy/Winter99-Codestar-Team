namespace Back_End.Models
{
    public class User
    {
        public string UserId { get; set; }

        public string Email { get; set; }

        public string Salt { get; set; }
        
        public string Hashed { get; set; }
        
    }
}