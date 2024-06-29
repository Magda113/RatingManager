using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class CategoryAddDto
    {
        [StringLength(255)]
        public string Name { get; set; }
        public int CreatedBy { get; set; }
    }
}
