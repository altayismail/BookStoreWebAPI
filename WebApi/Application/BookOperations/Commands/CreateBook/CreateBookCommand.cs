using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommand
    {
        public CreateViewModel Model {get; set;}
        private readonly IBookStoreDbContext _DbContext;
        private readonly IMapper _mapper;

        public CreateBookCommand(IBookStoreDbContext DbContext, IMapper mapper)
        {
            _DbContext = DbContext;
            _mapper = mapper;
        }
        
        public void Handle()
        {
            var book = _DbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

            if(book is not null)
                throw new InvalidOperationException("Kitap zaten mevcut.");

            book = _mapper.Map<Book>(Model);

            _DbContext.Books.Add(book);

            _DbContext.SaveChanges();
        }
        
    }

    public class CreateViewModel
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        
    }
}