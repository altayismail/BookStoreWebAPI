using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

                context.AddRange
                (
                    new Book
                    {
                        //Id = 1,
                        Title = "Babı Esrar",
                        GenreId = 1,
                        PageCount = 375,
                        PublishDate = new System.DateTime(2000,09,25)
                    },
                    new Book
                    {
                        //Id = 2,
                        Title = "Nutuk",
                        GenreId = 2,
                        PageCount = 550,
                        PublishDate = new System.DateTime(1938,11,10)
                    },
                    new Book
                    {
                        //Id = 3,
                        Title = "Elon Musk",
                        GenreId = 2,
                        PageCount = 330,
                        PublishDate = new System.DateTime(2015,05,15)
                    }
                );

                context.SaveChanges();
            };
        }
    }
}