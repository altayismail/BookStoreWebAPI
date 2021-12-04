using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidAuthorIdIsGiven_Validator_ShouldReturnError()
        {
            UpdateAuthorCommand command = new(null);
            command.AuthorId = -1;
            command.Model = new UpdateAuthorViewModel(){Name = "İsmail",LastName = "Altay",Birthday = System.DateTime.Now.AddYears(-31)};

            UpdateAuthorCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Theory]
        [InlineData("","")]
        [InlineData("İsmail","")]
        [InlineData("","Altay")]
        [InlineData("İ","")]
        [InlineData("","A")]
        [InlineData("İ","A")]
        public void WhenInvalidAuthorInfoIsGiven_Validator_ShouldReturnError(string name,string lastName)
        {
            UpdateAuthorCommand command = new(null);
            command.Model = new UpdateAuthorViewModel(){Name = name,LastName = lastName,Birthday = System.DateTime.Now.AddYears(-30)};
            command.AuthorId = 1;

            UpdateAuthorCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenInvalidBirthdayIsGiven_Validator_ShouldReturnError()
        {
            UpdateAuthorCommand command = new(null);
            command.Model = new UpdateAuthorViewModel(){Name = "İsmail",LastName = "Altay",Birthday = System.DateTime.Now};
            command.AuthorId = 1;

            UpdateAuthorCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            UpdateAuthorCommand command = new(null);
            command.Model = new UpdateAuthorViewModel(){Name = "İsmail",LastName = "Altay",Birthday = System.DateTime.Now.AddYears(-30)};
            command.AuthorId = 1;

            UpdateAuthorCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
        
    }
}