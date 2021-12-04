using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTest(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_ExpectedInvalidOperation_ShouldBeGiven()
        {
            var book = new Book(){Title = "Test_WhenAlreadyExistBookTitleIsGiven_ExpectedInvalidOperation_ShouldBeGiven", PageCount = 100, GenreId = 1, PublishDate = new System.DateTime(1990,01,01), AuthorId = 1};
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new(_context,_mapper);
            command.Model = new CreateViewModel() {Title = book.Title};

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut.");
        }

        [Fact]
        public void WhenValidUnptuIsGiven_Book_ShouldBeCreated()
        {
            CreateBookCommand command = new(_context,_mapper);
            CreateViewModel model = new CreateViewModel() 
            {
                Title = "Game of Thrones", PageCount = 1000, PublishDate = DateTime.Now.Date.AddYears(-10), GenreId = 1, AuthorId = 1
            };
            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.FirstOrDefault(book => book.Title == model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);
        }
    }
}