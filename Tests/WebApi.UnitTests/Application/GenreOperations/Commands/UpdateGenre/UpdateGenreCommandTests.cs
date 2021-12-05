using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateGenreCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }
        [Fact]
        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            UpdateGenreCommand command = new(_context);

            command.Model = new UpdateGenreViewModel(){Name = "Bu bir yeni tür ama id hatalı"};
            command.GenreId = -1;

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek kitap türü bulunamadı");
        }
        [Fact]
        public void WhenExistGenreNameIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            UpdateGenreCommand command = new(_context);
            var genre = new Genre(){Name = "ExistName"};
            _context.Genres.Add(genre);
            _context.SaveChanges();
            command.Model = new UpdateGenreViewModel(){Name = "ExistName"};
            command.GenreId = 1;

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu isimde kitap türü zaten mevcut");
        }
        [Fact]
        public void WhenValidInfosAreGiven_Genre_ShouldBeUpdated()
        {
            UpdateGenreCommand command = new(_context);
            var genre = new Genre(){Name = "ExistName"};
            _context.Genres.Add(genre);
            _context.SaveChanges();
            command.Model = new UpdateGenreViewModel(){Name = "NewName"};
            command.GenreId = genre.Id;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var result = _context.Genres.SingleOrDefault(genre => genre.Name == command.Model.Name);
            result.Should().NotBeNull();
            result.Name.Should().Be(command.Model.Name);
        }
    }
}