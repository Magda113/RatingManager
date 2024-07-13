﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class AddCategoryDto
    {
        [StringLength(255)]
        [Required]
        [Display(Name = "Nazwa kategorii")]
        public string Name { get; set; }
    }
}
