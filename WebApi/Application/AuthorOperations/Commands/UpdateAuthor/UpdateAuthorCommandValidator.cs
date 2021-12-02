using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorId).GreaterThan(0);
            RuleFor(command => command.Model.Name).NotNull().MinimumLength(2);
            RuleFor(command => command.Model.LastName).NotNull().MinimumLength(2);
            RuleFor(command => command.Model.Birthday).LessThan(System.DateTime.Today.AddYears(-18));
        }
    }
}