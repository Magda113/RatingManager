using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class AddCategoryDto
    {
        [StringLength(255)]
        [Required]
        public string Name { get; set; }
        [Required]
        public int CreatedBy { get; set; }
    }
}
