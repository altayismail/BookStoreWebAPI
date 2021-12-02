using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("", 0, 0, 0)]
        [InlineData("Game of Thrones", 0, 0, 0)]
        [InlineData("Game of Thrones", 110, 0, 0)]
        [InlineData("Game of Thrones", 0, 1, 0)]
        [InlineData("Game of Thrones", 0, 0, 1)]
        [InlineData("Game of Thrones", 110, 1, 0)]
        [InlineData("Game of Thrones", 110, 0, 1)]
        [InlineData("Game of Thrones", 0, 1, 1)]
        [InlineData("", 110, 0, 0)]
        [InlineData("", 0, 1, 0)]
        [InlineData("", 110, 1, 0)]
        [InlineData("", 0, 0, 1)]
        [InlineData("", 110, 0, 1)]
        [InlineData("", 110, 1, 1)]
        [InlineData("", 0, 1, 1)]
        public void WhenInvalidInputIsGiven_Validator_ShouldReturnErrors(string title, int pageCount, int genreId, int authorId)
        {
            CreateBookCommand command = new(null,null);
            command.Model = new CreateViewModel ()
            {
                Title = title,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                PageCount = pageCount,
                GenreId = genreId,
                AuthorId = authorId
            };
            
            CreateBookCommandValidation validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeInputIsGivenNow_Validator_ShouldReturnError()
        {
            CreateBookCommand command = new(null,null);
            command.Model = new CreateViewModel ()
            {
                Title = "Game of Thrones",
                PublishDate = DateTime.Now.Date,
                PageCount = 1130,
                GenreId = 1,
                AuthorId = 1
            };
            
            CreateBookCommandValidation validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotReturnError()
        {
            CreateBookCommand command = new(null,null);
            command.Model = new CreateViewModel ()
            {
                Title = "Game of Thrones",
                PublishDate = DateTime.Now.Date.AddYears(-1),
                PageCount = 1130,
                GenreId = 1,
                AuthorId = 1
            };
            
            CreateBookCommandValidation validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}