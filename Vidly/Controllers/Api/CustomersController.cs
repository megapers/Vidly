using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;
using Vidly.DTOs;
using Vidly.Models;


namespace Vidly.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[ApiController]
    public class CustomersController : ControllerBase
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;
     
        public CustomersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper; //injected automapper
        }
        //[AllowAnonymous]
        //GET /api/customers
        [HttpGet]
        public async Task<IActionResult> GetCustomers(string query = null)
        {
            using (_context)
            {
                var custsomersQuery =  _context.Customers.Include(c => c.MembershipType).AsQueryable();//await is not working to fetch from Database

                if (!String.IsNullOrWhiteSpace(query))
                {
                    custsomersQuery = custsomersQuery.Where(c => c.Name.Contains(query));
                }

                var customerDtos = custsomersQuery.ToList();

                return Ok(customerDtos.Select(_mapper.Map<Customer, CustomerDto>));
            }
        }

        //GET /api/customers/1
        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            using (_context)
            {
                var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);

                if (customer == null)
                {
                    NotFound();
                }
                return Ok(_mapper.Map<Customer, CustomerDto>(customer));
            }
        }

        //POST /api/customers
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody]CustomerDto customerDto)
        {
            using (_context)
            {
                if (!ModelState.IsValid)
                {
                    BadRequest();
                }

                var customer = _mapper.Map<CustomerDto, Customer>(customerDto);

                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();

                customerDto.Id = customer.Id;
                
                return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customerDto);
            }
        }

        //PUT /api/customers/id
        [HttpPut("{id}")]
        public async Task UpdateCustomer(int id, [FromBody]CustomerDto customerDto)
        {
            using (_context)
            {
                if (!ModelState.IsValid)
                {
                    NotFound();
                }
                var customerInDb = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
                if (customerInDb == null)
                {
                    NotFound();  // This returns HTTP 404    
                }
                _mapper.Map(customerDto, customerInDb);
                await _context.SaveChangesAsync();
            }
        }

        //DELETE /api/customers/id
        [HttpDelete("{id}")]
        public async Task DeleteCustomer(int id)
        {
            using (_context)
            {
                var customerInDb = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
                if (customerInDb == null)
                {
                    NotFound();
                }
                _context.Customers.Remove(customerInDb);
                await _context.SaveChangesAsync();
            }
        }
    }
}