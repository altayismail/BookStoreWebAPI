using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        private readonly IBookStoreDbContext _context;

        public CreateAuthorViewModel Model {get; set;}

        public CreateAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Name == Model.Name && x.LastName == Model.LastName);
            if(author is not null)
                throw new InvalidOperationException("Yazar zaten mevcut.");
            
            author = new Author();
            author.Name = Model.Name.Trim();
            author.LastName = Model.LastName.Trim();
            author.Birthday = Model.Birthday;
            _context.Authors.Add(author);
            _context.SaveChanges();
        }
    }

    public class CreateAuthorViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
    }
}