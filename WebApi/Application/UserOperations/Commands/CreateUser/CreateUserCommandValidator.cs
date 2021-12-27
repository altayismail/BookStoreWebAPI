using FluentValidation;

namespace WebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.viewModel.Email).NotNull().NotEmpty().MinimumLength(12);
            RuleFor(command => command.viewModel.Name).NotNull().NotEmpty().MinimumLength(2);
            RuleFor(command => command.viewModel.Password).NotEmpty().NotNull().MinimumLength(7);
            RuleFor(command => command.viewModel.Surname).NotNull().NotEmpty().MinimumLength(2);
        }
    }
}