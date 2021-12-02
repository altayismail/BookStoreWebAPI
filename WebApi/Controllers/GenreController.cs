using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DBOperations;

namespace WebApi.Controller
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new(_context,_mapper);
            return Ok(query.Handle());
        }

        [HttpGet("{id}")]
        public IActionResult GetGenre(int id)
        {
            GetGenreDetailQuery query = new(_context,_mapper);
            query.GenreId = id;
            GetGenreDetailQueryValidator validator = new();
            validator.ValidateAndThrow(query);
            return Ok(query.Handle());
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreViewModel newGenre)
        {
            CreateGenreCommand command = new(_context);
            command.Model = newGenre;
            CreateGenreCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre([FromQuery] UpdateGenreViewModel updateGenre, int id)
        {
            UpdateGenreCommand command = new(_context);
            command.GenreId = id;
            command.Model = updateGenre;

            UpdateGenreCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new(_context,_mapper);
            command.Id = id;
            DeleteGenreValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

    }
}