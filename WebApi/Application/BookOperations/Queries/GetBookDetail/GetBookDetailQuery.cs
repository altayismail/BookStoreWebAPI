using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
        public int BookId {get; set;}
        private readonly IBookStoreDbContext _DbContext;

        private readonly IMapper _mapper;

        public GetBookDetailQuery(IBookStoreDbContext DbContext, IMapper mapper)
        {
            _DbContext = DbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var bookList = _DbContext.Books.Include(x => x.Genre).Include(x => x.Author).Where(book => book.Id == BookId).SingleOrDefault();

            if(bookList is null)
                throw new InvalidOperationException("Kitab bulunamadı.");

            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(bookList);

            return vm;
        }
    }
    
    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Author { get ; set; }
        public int PageCount { get; set; }
        public string Genre { get; set; }
        public string PublishDate { get; set; }
    }
}