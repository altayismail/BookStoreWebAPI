using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenExistsEmailIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            var user = new User(){Name = "İsmail",Surname = "Altay",Email = "ismailaltay@mail.com",Password = "123456789"};
            _context.Users.Add(user);
            _context.SaveChanges();

            CreateUserCommand command = new(_context,_mapper);

            CreateUserViewModel viewModel = new CreateUserViewModel(){Name = "Ugurcan",Surname = "Cakır",Email = "ismailaltay@mail.com" ,Password = "asdasdasdasd"};
            command.viewModel = viewModel;

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu emaile sahip kullanıcı bulunmaktadır.");
        }

        [Fact]
        public void WhenValidInputIsGiven_User_ShouldBeCreated()
        {
            CreateUserCommand command = new(_context,_mapper);

            CreateUserViewModel viewModel = new(){Name = "Bora",Surname = "Altay",Email = "boraaltay@mail.com",Password = "2132132132131"};
            command.viewModel = viewModel;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var user = _context.Users.SingleOrDefault(x => x.Email == viewModel.Email);

            user.Should().NotBeNull();
            user.Name.Should().Be(viewModel.Name);
            user.Surname.Should().Be(viewModel.Surname);
            user.Email.Should().Be(viewModel.Email);
            user.Password.Should().Be(viewModel.Password);
        }
    }
}