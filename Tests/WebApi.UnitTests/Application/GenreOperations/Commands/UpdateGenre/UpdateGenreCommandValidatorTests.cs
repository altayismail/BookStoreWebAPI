using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using Xunit;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateGenreCommandValidatorTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }
        [Fact]
        public void WhenInvalidGenreIdIsGiven_Validator_ShouldReturnError()
        {
            UpdateGenreCommand command = new(_context);
            command.Model = new UpdateGenreViewModel(){Name = "Güncellencek Kitap Türü"};
            command.GenreId = -1;

            UpdateGenreCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenInvalidGenreNameIsGiven_Validator_ShouldReturnError()
        {
            UpdateGenreCommand command = new(_context);
            command.Model = new UpdateGenreViewModel(){Name = "A"};
            command.GenreId = 1;

            UpdateGenreCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenValidGenreIdIsGiven_Validator_ShouldNotReturnError()
        {
            UpdateGenreCommand command = new(_context);
            command.Model = new UpdateGenreViewModel(){Name = "Güncellencek Kitap Türü"};
            command.GenreId = 1;

            UpdateGenreCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}