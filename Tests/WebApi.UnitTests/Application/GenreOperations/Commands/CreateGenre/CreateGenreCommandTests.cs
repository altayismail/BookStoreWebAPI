using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public CreateGenreCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }
        [Fact]
        public void WhenExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            CreateGenreCommand command = new(_context);
            var genre = new Genre(){Name = "Cartoon"};
            _context.Genres.Add(genre);
            _context.SaveChanges();
            command.Model = new CreateGenreViewModel()
            {
                Name = "Cartoon"
            };

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
                                        .And.Message.Should().Be("Bu kitap türü zaten mevcut.");

        }
        [Fact]
        public void WhenValidGenreNameIsGiven_Genre_ShouldBeCreated()
        {
            CreateGenreCommand command = new(_context);
            command.Model = new CreateGenreViewModel()
            {
                Name = "Cartoon Network"
            };

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var result = _context.Genres.SingleOrDefault(x => x.Name == command.Model.Name);
            result.Should().NotBeNull();
        }
    }
}