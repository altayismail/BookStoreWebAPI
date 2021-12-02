using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }
        public GetAuthorDetailViewModel Model {get ; set;}

        public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GetAuthorDetailViewModel Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);

            if(author is null)
                throw new InvalidOperationException("İlgili yazar bulunamadı.");
            
            GetAuthorDetailViewModel vm = _mapper.Map<GetAuthorDetailViewModel>(author);
            return vm;
        }
    }

    public class GetAuthorDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
    }
}