using FluentValidation;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.Name).MinimumLength(3).When(x => x.Model.Name.Trim() != string.Empty);
        }
    }
}