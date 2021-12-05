using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidGenreIdIsGiven_Validator_ShouldReturnError()
        {
            DeleteGenreCommand command = new(null,null);
            command.Id = 0;

            DeleteGenreValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidGenreIdIsGiven_Validator_ShouldNotReturnError()
        {
            DeleteGenreCommand command = new(null,null);
            command.Id = 1;

            DeleteGenreValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}