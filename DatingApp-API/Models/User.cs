namespace DatingApp_API.Models
{
    public class User
    {
        //4 properties

        public int Id { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}