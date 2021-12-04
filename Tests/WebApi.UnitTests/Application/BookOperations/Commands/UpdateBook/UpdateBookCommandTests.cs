using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidBookIdIsGiven_InvalidOperaionException_ShouldBeReturn()
        {
            UpdateBookCommand command = new(_context);
            var bookId = 0;
            command.BookId = bookId;
            
            var result = _context.Books.FirstOrDefault(book => book.Id != command.BookId);

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek ilgili kitap bulunamadı.");
        }
        [Fact]
        public void WhenValidBookIdIsGiven_Book_ShouldBeUpdated()
        {
            UpdateBookCommand command = new(_context);
            var book = new Book()
            {
                Title = "Get out of Here",PageCount = 10,GenreId = 1,AuthorId = 1,PublishDate = DateTime.Now.AddYears(-31)
            };
            _context.Books.Add(book);
            _context.SaveChanges();
            command.Model = new UpdateBookViewModel(){Title = "Kırmızı Başlıklı Kız",GenreId = 2,AuthorId =2};
            command.BookId = book.Id;

            FluentActions.Invoking(() => command.Handle()).Invoke();
            var result = _context.Books.FirstOrDefault(x => x.Id == book.Id);
            result.Should().NotBeNull();
            result.Title.Should().Be(command.Model.Title);
            result.GenreId.Should().Be(command.Model.GenreId);
            result.AuthorId.Should().Be(command.Model.AuthorId);

        }
    }
}