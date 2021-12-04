using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }
        
        [Fact]
        public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldReturn()
        {
            UpdateAuthorCommand command = new(_context);
            command.AuthorId = -1;

            command.Model = new UpdateAuthorViewModel(){Name = "İsmail", LastName = "Altay", Birthday = System.DateTime.Now.AddYears(-21)};

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().
                                            And.Message.Should().Be("Güncellencek yazar bulunamadı.");
        }
        [Fact]
        public void WhenExistAuthorIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            UpdateAuthorCommand command = new(_context);
            var author = new Author(){Name = "İsmail", LastName = "Altay",Birthday = DateTime.Now.AddYears(-21)};
            _context.Authors.Add(author);
            _context.SaveChanges();

            command.Model = new UpdateAuthorViewModel(){Name = "İsmail", LastName = "Altay", Birthday = DateTime.Now.AddYears(-25).AddHours(-3)};
            command.AuthorId = author.Id;

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().
                                            And.Message.Should().Be("İlgili yazar zaten bulunuyor.");
        }
        [Fact]
        public void WhenValidAuthorIdIsGiven_Author_ShouldBeUpdated()
        {
            UpdateAuthorCommand command = new(_context);
            var author = new Author(){Name = "İsmail", LastName = "Altay",Birthday = DateTime.Now.AddYears(-21)};
            _context.Authors.Add(author);
            _context.SaveChanges();

            command.Model = new UpdateAuthorViewModel(){Name = "Marek", LastName = "Hamsik", Birthday = DateTime.Now.AddYears(-30).AddHours(-3)};
            command.AuthorId = author.Id;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var result = _context.Authors.SingleOrDefault(x => x.Id == author.Id);
            result.Should().NotBeNull();
            author.Name.Should().Be(command.Model.Name);
            author.LastName.Should().Be(command.Model.LastName);
            author.Birthday.Should().Be(command.Model.Birthday);
        }
    }
}