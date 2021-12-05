using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Queries.GetGenreDetail
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateGenreCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }
        [Fact]
        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            GetGenreDetailQuery query = new(_context,_mapper);
            query.GenreId = -1;

            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı.");
        }
        [Fact]
        public void WhenValidGenreIdIsGiven_InvalidOperationException_ShouldNotBeThrown()
        {
            GetGenreDetailQuery query = new(_context,_mapper);
            query.GenreId = 1;

            FluentActions.Invoking(() => query.Handle()).Invoke();
            
            var result = _context.Genres.SingleOrDefault(x => x.Id == 1);
            result.Should().NotBeNull();
        }
    }
}