using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateBookCommandValidatorTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Theory]
        [InlineData("",0,0)]
        [InlineData("Game of Thrones",1,0)]
        [InlineData("Game of Thrones",0,1)]
        [InlineData("",1,1)]
        [InlineData("",1,0)]
        [InlineData("",0,1)]
        public void WhenInvalidInputIsGiven_Validator_ShouldReturnError(string title, int genreId, int authorId)
        {
            UpdateBookCommand command = new(_context);
            var bookId = 1;
            command.BookId = bookId;
            command.Model = new UpdateBookViewModel()
            {
                Title = title,
                GenreId = genreId,
                AuthorId = authorId
            };

            UpdateBookCommandValidation validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotReturnError()
        {
            UpdateBookCommand command = new(_context);
            var bookId = 1;
            command.BookId = bookId;
            command.Model = new UpdateBookViewModel()
            {
                Title = "Game of Thrones",GenreId = 1,AuthorId= 1
            };

            UpdateBookCommandValidation validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}