using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookViewModel Model {get; set;}

        public int BookId { get; set; }
        private readonly IBookStoreDbContext _DbContext;

        public UpdateBookCommand(IBookStoreDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public void Handle()
        {
            var book = _DbContext.Books.SingleOrDefault(x => x.Id == BookId);

            if(book is null)
                throw new InvalidOperationException("Güncellenecek ilgili kitap bulunamadı.");

            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            //book.PageCount = Model.PageCount != default ? Model.PageCount : book.GenreId;
            //book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.AuthorId = Model.AuthorId != default ? Model.AuthorId : book.AuthorId;

            _DbContext.SaveChanges();

        }
    }

    public class UpdateBookViewModel
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
    }
}