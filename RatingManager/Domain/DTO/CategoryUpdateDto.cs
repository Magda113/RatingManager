using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class CategoryUpdateDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public CategoryStatus Status { get; set; }
        public int? ModifiedBy { get; set; }

    }
}
