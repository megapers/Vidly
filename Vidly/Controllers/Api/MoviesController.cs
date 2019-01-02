using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Data;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    //[Authorize(Policy = "CanManageMovies")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[ApiController]
    public class MoviesController : ControllerBase
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MoviesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper; //injected automapper
        }

        //GET /api/movies
        [HttpGet]
        public async Task<IActionResult> GetMovies(string query = null)
        {
            using (_context)
            {
                var moviesQuery =  _context.Movies
                    .Include(m => m.Genre)
                    .Where(m => m.Available > 0)
                    .AsQueryable();//await is not working to fetch from Database

                if (!String.IsNullOrWhiteSpace(query))
                {
                    moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));
                }

                var movieDtos = moviesQuery.ToList();

                return Ok(movieDtos.Select(_mapper.Map<Movie, MovieDto>));
            }
        }

        //GET /api/movies/1
        [HttpGet("{id}", Name = "GetMovie")]
        public async Task<IActionResult> GetMovie(int id)
        {
            using (_context)
            {
                var movie = await _context.Movies.SingleOrDefaultAsync(c => c.Id == id);

                if (movie == null)
                {
                    NotFound();
                }
                return Ok(_mapper.Map<Movie, MovieDto>(movie));
            }
        }

        //POST /api/movies
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody]MovieDto movieDto)
        {
            using (_context)
            {
                if (!ModelState.IsValid)
                {
                    BadRequest();
                }

                var movie = _mapper.Map<MovieDto, Movie>(movieDto);

                await _context.Movies.AddAsync(movie);
                await _context.SaveChangesAsync();

                movieDto.Id = movie.Id;

                return CreatedAtRoute("GetMovie", new { id = movie.Id }, movieDto);
            }
        }

        //PUT /api/movies/id
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPut("{id}")]
        public async Task UpdateMovie(int id, [FromBody]MovieDto movieDto)
        {
            using (_context)
            {
                if (!ModelState.IsValid)
                {
                    NotFound();
                }
                var movieInDb = await _context.Movies.SingleOrDefaultAsync(c => c.Id == id);
                if (movieInDb == null)
                {
                    NotFound();  // This returns HTTP 404    
                }
                _mapper.Map(movieDto, movieInDb);
                await _context.SaveChangesAsync();
            }
        }

        //DELETE /api/movies/id
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpDelete("{id}")]
        public async Task DeleteMovie(int id)
        {
            using (_context)
            {
                var movieInDb = await _context.Movies.SingleOrDefaultAsync(c => c.Id == id);
                if (movieInDb == null)
                {
                    NotFound();
                }
                _context.Movies.Remove(movieInDb);
                await _context.SaveChangesAsync();
            }
        }

    }
}
