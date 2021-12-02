using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.DBOperations;

namespace WebApi.Controller
{
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new(_context,_mapper);
            return Ok(query.Handle());
        }

        [HttpGet("{id}")]

        public IActionResult GetAuthor(int id)
        {
            GetAuthorDetailQuery query = new(_context,_mapper);
            query.AuthorId = id;
            GetAuthorDetailQueryValidator validator = new();
            validator.ValidateAndThrow(query);
            
            return Ok(query.Handle());
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] CreateAuthorViewModel newAuthor)
        {
            CreateAuthorCommand command = new(_context);
            command.Model = newAuthor;

            CreateAuthorCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")]

        public IActionResult UpdateAuthor([FromQuery] UpdateAuthorViewModel newAuthor, int id)
        {
            UpdateAuthorCommand command = new(_context);
            command.AuthorId = id;
            command.Model = newAuthor;
            
            UpdateAuthorCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new(_context);
            command.AuthorId = id;

            DeleteAuthorCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}