using FluentValidation;
using SlackApi.Data.Dto.RequestDto;
using SlackApi.Data.Model;

namespace SocialTree.Utils.Validator
{
    public class UserValidator : AbstractValidator<UserAuthDto>
    {

        private bool BeAbove12YearsOld(DateTime dateOfBirth)
        {
            // Calculate age
            int age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-age)) age--;

            return age > 12;
        }
        public UserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(3).MaximumLength(16).Matches("^[a-zA-Z0-9_]*$");
            RuleFor(x => x.UserEmail).NotEmpty().EmailAddress();
            RuleFor(x => x.UserPhone).NotEmpty().Matches(@"^\d{10}$");
            RuleFor(x => x.DateOfBirth).Must(BeAbove12YearsOld).WithMessage("Age must be above 12 years old");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).WithMessage("Password must be at least 8 characters long");


        }
    }
}
