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
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[ApiController]
    public class NewRentalController : ControllerBase
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public NewRentalController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper; //injected automapper
        }


        //GET /api/newRental
        [HttpGet]
        public async Task<IActionResult> GetRentals()
        {
            using (_context)
            {
                var rentals = await _context.Rental
                    .Include(r => r.Customer)
                    .Include(r => r.Movie)
                    .ToListAsync();
                return Ok(rentals);
            }
        }


        //GET /api/newRental/1
        [HttpGet("{id}", Name = "GetNewRental")]
        public async Task<IActionResult> GetNewRental(int id)
        {
            using (_context)
            {
                var newRental = await _context.Rental
                    .Include(r => r.Customer)
                    .Include(r => r.Movie)
                    .SingleOrDefaultAsync(r => r.Id == id);

                if (newRental == null)
                {
                    NotFound();
                }
                return Ok(newRental);
            }
        }


        //POST /api/newRental
        [HttpPost]
        public async Task<IActionResult> CreateNewRental(NewRentalDto newRental)
        {
            using (_context)
            {
                if (!ModelState.IsValid)
                {
                    BadRequest();
                }

                var customer = _context.Customers.Single(c => c.Id == newRental.CustomerId);

                var movies = _context.Movies
                    .Where(m => newRental.MoviesId.Contains(m.Id)).ToList();     
                
                foreach(var movie in movies)
                {
                    if(movie.Available == 0)
                    {
                        return BadRequest("Movie is not available");
                    }

                    movie.Available--;
                    var rental = new Rental
                    {
                        Customer = customer,
                        Movie = movie,
                        DateRented = DateTime.Now
                    };
                await _context.Rental.AddAsync(rental);
                }
                
                await _context.SaveChangesAsync();

                return Ok();
            }
        }

        //DELETE /api/newRental/id
        [HttpDelete("{id}")]
        public async Task DeleteRental(int id)
        {
            using (_context)
            {
                var rentalInDb = await _context.Rental
                    .Include(r => r.Movie)
                    .Include(r => r.Customer )
                    .SingleOrDefaultAsync(r => r.Id == id);

                if (rentalInDb == null)
                {
                    NotFound();
                }
                rentalInDb.Movie.Available++;
                _context.Rental.Remove(rentalInDb);
                await _context.SaveChangesAsync();
            }
        }
    }
}
