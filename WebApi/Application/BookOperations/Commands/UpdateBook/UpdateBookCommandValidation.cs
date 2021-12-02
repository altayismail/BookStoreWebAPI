using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidation : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidation()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
            RuleFor(command => command.Model.Title).NotNull().MinimumLength(3);
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.AuthorId).GreaterThan(0);
        }
    }
}