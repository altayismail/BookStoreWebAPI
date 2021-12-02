using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        public int AuthorId {get; set;}
        public UpdateAuthorViewModel Model { get; set; }

        public UpdateAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Güncellencek yazar bulunamadı.");

            if(_context.Authors.Any(x => x.Name == Model.Name && x.LastName == Model.LastName))
                throw new InvalidOperationException("İlgili yazar zaten bulunuyor.");
            
            author.Name = Model.Name.Trim() == default ? author.Name : Model.Name;
            author.LastName = Model.LastName.Trim() == default ? author.LastName : Model.LastName;
            author.Birthday = Model.Birthday == default ? author.Birthday : Model.Birthday;

            _context.SaveChanges();
        }
    }

    public class UpdateAuthorViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
    }
}