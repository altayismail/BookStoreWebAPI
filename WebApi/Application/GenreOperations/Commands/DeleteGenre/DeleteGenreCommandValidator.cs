using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreValidator : AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }
}