using SlackApi.Data.Model;
using System.ComponentModel.DataAnnotations;

namespace SocialTree.Data.Model
{
    public class UserVerification
    {
        [Key]
        public int VerificationId { get; set; }

        // Foreign key to relate to the User entity
        public long UserId { get; set; }
        public User User { get; set; }

        public bool IsEmailVerified { get; set; }
        public string EmailVerificationToken { get; set; }
        public DateTime? EmailVerificationTimestamp { get; set; }

    }
}
