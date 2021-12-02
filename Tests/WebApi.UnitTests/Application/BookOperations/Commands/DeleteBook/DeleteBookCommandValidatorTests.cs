using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTestsValidator : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldReturnErrors()
        {
            DeleteBookCommand command = new(null);
            var bookId = 0;
            command.BookId = bookId;

            DeleteBookCommandValidation validator = new();
            var result = validator.Validate(command);
            
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenInvalidInptuIsGiven_Validator_ShouldNotReturnErrors()
        {
            DeleteBookCommand command = new(null);
            var bookId = 1;
            command.BookId = bookId;

            DeleteBookCommandValidation validator = new();
            var result = validator.Validate(command);
            
            result.Errors.Count.Should().Equals(0);
        }
    }
}