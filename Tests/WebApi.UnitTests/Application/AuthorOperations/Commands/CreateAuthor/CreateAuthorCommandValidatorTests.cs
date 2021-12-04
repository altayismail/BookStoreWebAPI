using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("","")]
        [InlineData("İsmail","")]
        [InlineData("","Altay")]
        [InlineData("İ","A")]
        [InlineData("İ","")]
        [InlineData("","A")]
        public void WhenInvalidInputIsGiven_Validator_ShouldReturnErros(string name, string lastName)
        {
            CreateAuthorCommand command = new(null);
            command.Model = new CreateAuthorViewModel()
            {
                Name = name,
                LastName = lastName,
                Birthday = DateTime.Now.AddYears(-20)
            };

            CreateAuthorCommandValidator validator = new();
            var result = validator.Validate(command);

               result.Errors.Count.Should().BeGreaterThan(0);
            
        }
        [Fact]
        public void WhenDateTimeInputIsGivenLessThanEighteen_Validator_ShouldReturnError()
        {
            CreateAuthorCommand command = new(null);
            command.Model = new CreateAuthorViewModel()
            {
                Name = "İsmail",
                LastName = "Altay",
                Birthday = DateTime.Now.AddYears(-10)
            };
            
            CreateAuthorCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotReturnError()
        {
            CreateAuthorCommand command = new(null);
            command.Model = new CreateAuthorViewModel()
            {
                Name = "İsmail",
                LastName = "Altay",
                Birthday = DateTime.Now.AddYears(-21)
            };

            CreateAuthorCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}