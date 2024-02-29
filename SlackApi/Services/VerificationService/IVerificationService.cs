namespace SocialTree.Services.VerificationService
{
    public interface IVerificationService
    {
        Task<bool> IsUserVerifiedAsync(int userId);

        Task<string> GenerateEmailVerificationCodeAsync(int userId);

        Task<bool> VerifyEmailVerificationCodeAsync(int userId, string verificationCode);

   
    }
}
