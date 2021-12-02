using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                    return;

                context.Authors.AddRange
                (
                    new Author{Name = "İsmail",LastName = "Altay",Birthday = new DateTime(2000,09,25)},
                    new Author{Name = "Ahmet",LastName = "Ümit",Birthday = new DateTime(1945,02,13)},
                    new Author{Name = "George",LastName = "Thomas",Birthday = new DateTime(1976,01,01)}
                );
                context.Genres.AddRange
                (
                    new Genre{Name = "Personal Growth"},
                    new Genre{Name = "Science Fiction"},
                    new Genre{Name = "Romance"}
                );
                context.Books.AddRange
                (
                    new Book{Title = "Babı Esrar",AuthorId = 1,GenreId = 1,PageCount = 375,PublishDate = new DateTime(2000,09,25)},
                    new Book{Title = "Nutuk",AuthorId = 2,GenreId = 2,PageCount = 550,PublishDate = new DateTime(1938,11,10)},
                    new Book{Title = "Elon Musk",AuthorId = 3,GenreId = 2,PageCount = 330,PublishDate = new DateTime(2015,05,15)}
                );

                context.SaveChanges();
            };
        }
    }
}