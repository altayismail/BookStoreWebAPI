using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _context;

        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        
        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("İlgili yazar zaten bulunmamaktadır.");
            var book = _context.Books.SingleOrDefault(x => x.AuthorId == author.Id);
            if(book is not null)
                throw new InvalidOperationException("İlgili yazarın, yayında kitabı bulunmaktadır.");
            
            _context.Authors.Remove(author);
            _context.SaveChanges();
            
        }
    }
}