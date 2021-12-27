using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommand
    {
        public CreateUserViewModel viewModel {get; set;}
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == viewModel.Email);

            if(user is not null)
                throw new InvalidOperationException("Bu emaile sahip kullanıcı bulunmaktadır.");

            user = _mapper.Map<User>(viewModel);

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }

    public class CreateUserViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}