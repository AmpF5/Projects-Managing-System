using FluentValidation;
using ProjectsManagingSystem.Entities;
using ProjectsManagingSystem.Models.Member;

namespace ProjectsManagingSystem.Models.Validators
{
    public class RegisterMemberDtoValidator : AbstractValidator<MemberDto>
    {
        public RegisterMemberDtoValidator(ProjectSystemDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);

            //RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Members.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });
        }
    }
}
