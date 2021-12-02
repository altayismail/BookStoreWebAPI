using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
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
            UpdateBookViewModel model = new UpdateBookViewModel() 
            {
                Title = "Game of Thrones",
                GenreId = 1,
                AuthorId = 3
            };

            command.Model = model;
            command.BookId = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);
            book.Should().NotBeNull();
            book.Title.Should().Be(model.Title);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);

        }
    }
}