using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;
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
            var bookId = 1;
            query.BookId = bookId;

            var result = _context.Books.SingleOrDefault(x => x.Id == query.BookId);

            FluentActions.Invoking(() => query.Handle()).Invoke();

            result.Should().NotBeNull();
        }
    }
}