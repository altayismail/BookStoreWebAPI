using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public CreateAuthorCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }
        
        [Fact]
        public void WhenExistAuthorNameAndSurnameIsGiven_InvalidOperationException_ShouldBeThrown()
        {

            var author = new Author(){Name = "Burak",LastName = "Yılmaz",Birthday = System.DateTime.Now.AddYears(-34)};
            CreateAuthorCommand command = new(_context);
            _context.Authors.Add(author);
            _context.SaveChanges();

            var model = new CreateAuthorViewModel(){Name = author.Name,LastName = author.LastName};
            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut.");
        }

        [Fact]
        public void WhenValidAuthorInfoIsGiven_Author_ShouldBeCreated()
        {
            CreateAuthorCommand command = new(_context);

            var model = new CreateAuthorViewModel()
            {
                Name = "Ugurcan",
                LastName = "Cakır",
                Birthday = DateTime.Now.AddYears(-24)
            };
            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var result = _context.Authors.SingleOrDefault(x => x.Name == model.Name);

            result.Should().NotBeNull();
            result.Name.Should().Be(model.Name);
            result.LastName.Should().Be(model.LastName);
            result.Birthday.Should().Be(model.Birthday);
        }
    }
}