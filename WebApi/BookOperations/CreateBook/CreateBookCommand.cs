using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateViewModel Model {get; set;}
        private readonly BookStoreDbContext _DbContext;

        public CreateBookCommand(BookStoreDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        
        public void Handle()
        {
            var book = _DbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

            if(book is not null)
                throw new InvalidOperationException("Kitap zaten mevcut.");

            book = new Book();
            book.Title = Model.Title;
            book.GenreId = Model.GenreId;
            book.PublishDate = Model.PublishDate;
            book.PageCount = Model.PageCount;

            _DbContext.Books.Add(book);

            _DbContext.SaveChanges();
        }
        
    }

    public class CreateViewModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        
    }
}