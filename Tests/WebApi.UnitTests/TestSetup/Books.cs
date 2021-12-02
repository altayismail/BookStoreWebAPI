using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBook(this BookStoreDbContext context)
        {
            context.Books.AddRange
            (
                new Book{Title = "Babı Esrar",AuthorId = 1,GenreId = 1,PageCount = 375,PublishDate = new DateTime(2000,09,25)},
                new Book{Title = "Nutuk",AuthorId = 2,GenreId = 2,PageCount = 550,PublishDate = new DateTime(1938,11,10)},
                new Book{Title = "Elon Musk",AuthorId = 3,GenreId = 2,PageCount = 330,PublishDate = new DateTime(2015,05,15)}
            );
        }
    }
}