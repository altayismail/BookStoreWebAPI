using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidation : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidation()
        {
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            RuleFor(command => command.Model.PublishDate).NotEmpty().LessThan(System.DateTime.Now.Date);
            RuleFor(commnad => commnad.Model.Title).NotNull().MinimumLength(3);
            RuleFor(command => command.Model.AuthorId).GreaterThan(0);
        }
    }
}