using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange
            (
                new Author{Name = "İsmail",LastName = "Altay",Birthday = new DateTime(2000,09,25)},
                new Author{Name = "Ahmet",LastName = "Ümit",Birthday = new DateTime(1945,02,13)},
                new Author{Name = "George",LastName = "Thomas",Birthday = new DateTime(1976,01,01)}
            );
        }
    }
}