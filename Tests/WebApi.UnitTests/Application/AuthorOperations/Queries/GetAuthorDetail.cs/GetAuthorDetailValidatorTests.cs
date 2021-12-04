using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using Xunit;

namespace Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidAuthorIdIsGiven_Validator_ShouldReturnError()
        {
            GetAuthorDetailQuery query = new(null,null);
            query.AuthorId = -1;

            GetAuthorDetailQueryValidator validator = new();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidAuthorIdIsGiven_Validator_ShouldNotReturnError()
        {
            GetAuthorDetailQuery query = new(null,null);
            query.AuthorId = 1;

            GetAuthorDetailQueryValidator validator = new();
            var result = validator.Validate(query);

            result.Errors.Count.Should().Equals(0);
        }
    }
}