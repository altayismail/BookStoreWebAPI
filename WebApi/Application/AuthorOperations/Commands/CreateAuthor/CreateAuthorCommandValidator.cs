using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotNull().MinimumLength(2);
            RuleFor(command => command.Model.LastName).NotNull().MinimumLength(2);
            RuleFor(command => command.Model.Birthday).LessThan(System.DateTime.Today.AddYears(-18));
        }
    }
}