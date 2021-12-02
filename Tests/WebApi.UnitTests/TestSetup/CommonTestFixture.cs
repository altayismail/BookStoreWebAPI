using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace TestSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext Context {get; set;}
        public IMapper Mapper {get; set;}

        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName: "BookStoreDb").Options;
            Context = new BookStoreDbContext(options);
            
            Context.Database.EnsureCreated();
            Context.AddBook();
            Context.AddGenres();
            Context.AddAuthors();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => {cfg.AddProfile<MappingProfile>();}).CreateMapper();

        }
    }
}