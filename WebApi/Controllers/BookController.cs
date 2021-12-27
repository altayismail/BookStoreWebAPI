using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DBOperations;

namespace WebApi.Controller
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        { 
            BookDetailViewModel result;
            
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            GetBookDetailQueryValidation validator = new GetBookDetailQueryValidation();
            query.BookId = id;
            validator.ValidateAndThrow(query);
            result = query.Handle();

            return Ok(result);
        }
        /*
        [HttpGet]
        public IActionResult Get([FromQuery] string id)
        {
            GetBookFromQueryViewModel result;

            GetBookFromQuery query = new GetBookFromQuery(_context);
            query.BookId = int.Parse(id);
            result = query.Handle();

            return Ok(result);
        }*/

        [HttpPost]

        public IActionResult AddBook([FromBody] CreateViewModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            CreateBookCommandValidation validator = new CreateBookCommandValidation(); 
            
            command.Model = newBook;
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")]

        public IActionResult UpdateBook(int id,[FromBody] UpdateBookViewModel updateBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookCommandValidation validator = new UpdateBookCommandValidation();

            command.BookId = id;
            command.Model = updateBook;
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            DeleteBookCommandValidation validator = new DeleteBookCommandValidation();

            command.BookId = id;
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

    }
}