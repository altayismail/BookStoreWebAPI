using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        public int BookId { get; set; }
        private readonly BookStoreDbContext _DbContext;

        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public void Handle()
        {
            var book = _DbContext.Books.SingleOrDefault(x => x.Id == BookId);

            if(book is null)
                throw new InvalidOperationException("Silinecek ilgili kitap bulunamadı.");

            _DbContext.Books.Remove(book);

            _DbContext.SaveChanges();
        }
    }
}