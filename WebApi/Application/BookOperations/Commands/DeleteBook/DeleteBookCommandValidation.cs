using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidation : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidation()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}