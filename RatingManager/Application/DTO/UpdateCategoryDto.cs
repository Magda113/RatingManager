using Domain.Models;
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
        [Display(Name = "Id")]
        public int CategoryId { get; set; }
        [Display(Name = "Nazwa")]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
