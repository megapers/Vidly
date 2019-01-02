using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Models;

namespace Vidly.DTOs
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<MembershipType, MembershipTypeDto>().ReverseMap();
            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<Rental, NewRentalDto>().ReverseMap();
        }
    }
}
