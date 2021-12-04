using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DBOperations;
using Xunit;

namespace Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorDetailTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }
        
        [Fact]
        public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            GetAuthorDetailQuery query = new(_context,_mapper);
            query.AuthorId = -1;

            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("İlgili yazar bulunamadı.");
        }
        [Fact]
        public void WhenValidAuthorIdIsGiven_InvalidOperationException_ShouldNotBeReturned()
        {
            GetAuthorDetailQuery query = new(_context,_mapper);
            query.AuthorId = 1;

            FluentActions.Invoking(() => query.Handle()).Invoke();

            var result = _context.Authors.SingleOrDefault(x => x.Id == query.AuthorId);
            result.Should().NotBeNull();
        }
    }
}