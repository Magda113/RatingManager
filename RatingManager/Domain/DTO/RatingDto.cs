using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class RatingDto
    {
        public int RatingId { get; set; }
        public string? Safety { get; set; }
        public string? Knowledge { get; set; }
        public string? Communication { get; set; }
        public string? Creativity { get; set; }
        public string? TechnicalAspects { get; set; }
        public int Result { get; set; }
    }
}
