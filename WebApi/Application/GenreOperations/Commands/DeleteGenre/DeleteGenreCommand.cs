using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int Id {get; set;}

        public DeleteGenreCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == Id);
            if(genre is null)
                throw new InvalidOperationException("Bu kitap türü zaten bulunmamaktadır");
            
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}