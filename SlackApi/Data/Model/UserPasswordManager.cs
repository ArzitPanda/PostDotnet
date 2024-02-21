using System.ComponentModel.DataAnnotations;

namespace SlackApi.Data.Model
{
    public class UserPasswordManager
    {
        [Key]
        public long Id { get; set; }
        public User User { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}
