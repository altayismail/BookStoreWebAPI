using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteAuthorCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }
        [Fact]
        public void WhenInvalidBookIdIsGiven_InvalidOperaionException_ShouldBeThrown()
        {
            DeleteAuthorCommand command = new(_context);
            var author = new Author()
            {
                Name = "İsmail",
                LastName = "Altay",
                Birthday = System.DateTime.Now.AddYears(-21)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            command.AuthorId = -1;

            var result = _context.Authors.FirstOrDefault(x => x.Id == command.AuthorId);

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("İlgili yazar zaten bulunmamaktadır.");
        }
        [Fact]
        public void WhenValidAuthorIdThatIsTheAuthorsBookHasAlreadyPublishedIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            DeleteAuthorCommand command = new(_context);
            var author = new Author()
            {
                Name = "İsmail",
                LastName = "Altay",
                Birthday = DateTime.Now.AddYears(-21)
            };
            
            _context.Authors.Add(author);
            _context.SaveChanges();
            
            var book = new Book()
            {
                Title = "Game of Thrones",
                PageCount = 1100,
                PublishDate = DateTime.Now.AddYears(-3),
                GenreId = 1,
                AuthorId = author.Id
            };
            _context.Books.Add(book);
            _context.SaveChanges();
            command.AuthorId = author.Id;
            var result = _context.Books.SingleOrDefault(x => x.AuthorId == author.Id);

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("İlgili yazarın, yayında kitabı bulunmaktadır.");
        }
        [Fact]
        public void WhenValidBookIdIsGiven_Author_ShouldBeDeleted()
        {
            DeleteAuthorCommand command = new(_context);
            var author = new Author(){Name = "İsmail",LastName = "Altay",Birthday = DateTime.Now.AddYears(-21)};
            _context.Authors.Add(author);
            _context.SaveChanges();
            command.AuthorId = author.Id;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var result = _context.Authors.SingleOrDefault(x => x.Id == author.Id);

            result.Should().BeNull();
        }
    }
}