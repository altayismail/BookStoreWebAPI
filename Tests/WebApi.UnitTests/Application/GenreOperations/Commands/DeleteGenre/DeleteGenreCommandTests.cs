using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteGenreCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }
        [Fact]
        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            DeleteGenreCommand command = new(_context,_mapper);
            command.Id = 0;

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu kitap türü zaten bulunmamaktadır");
        }
        [Fact]
        public void WhenValidGenreIdIsGiven_Genre_ShouldBeDeleted()
        {
            DeleteGenreCommand command = new(_context,_mapper);
            var genre = new Genre(){Name = "Cartoon"};
            _context.Genres.Add(genre);
            _context.SaveChanges();
            command.Id = genre.Id;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var result = _context.Genres.SingleOrDefault(x => x.Id == genre.Id);
            result.Should().BeNull();
        }
    }
}