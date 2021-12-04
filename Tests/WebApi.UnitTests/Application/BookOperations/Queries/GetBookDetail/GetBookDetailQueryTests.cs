using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        
        public GetBookDetailQueryTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }
        [Fact]
        public void WhenInvalidBookIdIsGiven_InvalidOperation_ShouldBeReturned()
        {
            GetBookDetailQuery query = new(_context,_mapper);
            var bookId = -1;
            query.BookId = bookId;

            var result = _context.Books.SingleOrDefault(x => x.Id == query.BookId);

            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitab bulunamadı.");
        }
        [Fact]
        public void WhenValidBookIdIsGiven_Book_ShouldBeReturn()
        {
            GetBookDetailQuery query = new(_context,_mapper);
            var book = new Book()
            {Title = "Get Out Of Here",PageCount = 11, GenreId = 1,AuthorId = 1,PublishDate = DateTime.Now.AddYears(-20)};
            _context.Books.Add(book);
            _context.SaveChanges();
            query.BookId = book.Id;

            FluentActions.Invoking(() => query.Handle()).Invoke();

            var result = _context.Books.FirstOrDefault(x => x.Id == book.Id);

            result.Should().NotBeNull();
        }
    }
}