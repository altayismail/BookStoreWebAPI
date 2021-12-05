using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using Xunit;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData("A")]
        public void WhenInvalidGenreNameIsGiven_Validator_ShouldReturnError(string name)
        {
            CreateGenreCommand command = new(null);
            command.Model = new CreateGenreViewModel(){Name = name};

            CreateGenreCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidGenreNameIsGiven_Validator_ShouldNotReturnError()
        {
            CreateGenreCommand command = new(null);
            command.Model = new CreateGenreViewModel(){Name = "Cartoon"};

            CreateGenreCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}