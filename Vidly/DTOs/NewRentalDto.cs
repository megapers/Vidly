﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Models;

namespace Vidly.DTOs
{
    public class NewRentalDto
    {
        public int CustomerId { get; set; }
        public List<int> MoviesId { get; set; }

    }
}
