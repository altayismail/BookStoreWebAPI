using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using Xunit;

namespace Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldReturnError()
        {
            GetBookDetailQuery query = new(null,null);
            var bookId = -1;
            query.BookId = bookId;

            GetBookDetailQueryValidation validator = new();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotReturnError()
        {
            GetBookDetailQuery query = new(null,null);
            var bookId = 1;
            query.BookId = bookId;

            GetBookDetailQueryValidation validator = new();
            var result = validator.Validate(query);

            result.Errors.Count.Should().Equals(0);
        }
    }
}