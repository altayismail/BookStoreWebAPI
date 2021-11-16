using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        public int BookId {get; set;}
        private readonly BookStoreDbContext _DbContext;

        public GetBookDetailQuery(BookStoreDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public BookDetailViewModel Handle()
        {
            var bookList = _DbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();

            if(bookList is null)
                throw new InvalidOperationException("Kitab bulunamadı.");

            BookDetailViewModel vm = new BookDetailViewModel();
            vm.Title = bookList.Title;
            vm.PageCount = bookList.PageCount;
            vm.PublishDate = bookList.PublishDate.Date.ToString("dd/MM/yyyy");
            vm.Genre = ((GenreEnum)bookList.GenreId).ToString();

            return vm;
        }
    }
    
    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string Genre { get; set; }
        public string PublishDate { get; set; }
    }
}