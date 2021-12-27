using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.DBOperations;
using Xunit;

namespace Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("","","","")]
        [InlineData("","Altay","","")]
        [InlineData("","","@mail.com","")]
        [InlineData("","","","123456789")]
        [InlineData("","Altay","@mail.com","123456789")]
        [InlineData("","Altay","@mail.com","")]
        [InlineData("","","@mail.com","123456789")]
        [InlineData("İsmail","","","")]
        [InlineData("İsmail","Altay","","")]
        [InlineData("İsmail","","@mail.com","")]
        [InlineData("İsmail","","","123456789")]
        [InlineData("İsmail","Altay","@mail.com","")]
        [InlineData("İsmail","","@mail.com","123456789")]
        public void WhenInvalidInputIsGiven_Validator_ShouldReturnError(string name, string surname, string email, string password)
        {
            CreateUserCommand command = new(null,null);
            CreateUserViewModel viewModel = new(){Name = name, Surname = surname, Email = email, Password = password};
            command.viewModel = viewModel;
            
            CreateUserCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotReturnError()
        {
            CreateUserCommand command = new(null,null);
            CreateUserViewModel viewModel = new(){Name = "İsmail", Surname = "Altay", Email = "ismailaltay@mail.com", Password = "123456789"};
            command.viewModel = viewModel;
            
            CreateUserCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}