﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class UpdateCategoryDto
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public CategoryStatus Status { get; set; }
    }
}