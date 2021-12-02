using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteBookCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            DeleteBookCommand command = new(_context);

            var book = _context.Books.FirstOrDefault(book => book.Id != command.BookId);

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek ilgili kitap bulunamadı."); 
        }
        [Fact]
        public void WhenValidBookIdIsGiven_Book_ShouldBeDeleted()
        {
            DeleteBookCommand command = new(_context);
            var bookId = 1;
            command.BookId = bookId;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var result = _context.Books.SingleOrDefault(book => book.Id == command.BookId);
            result.Should().BeNull();
        }
    }
}