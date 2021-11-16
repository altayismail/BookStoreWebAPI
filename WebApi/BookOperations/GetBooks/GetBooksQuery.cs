using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _DbContext;

        public GetBooksQuery(BookStoreDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _DbContext.Books.OrderBy(id => id.Id).ToList<Book>();
            List<BooksViewModel> vm = new List<BooksViewModel>();
            foreach (var book in bookList)
            {
                vm.Add
                (
                    new BooksViewModel
                    {
                        Title = book.Title,
                        PageCount = book.PageCount,
                        PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
                        Genre = ((GenreEnum)book.GenreId).ToString()
                    }
                );
            }
            return vm;
        }
    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}