using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string CallId { get; set; }
        public int UserId { get; set; }
        [MaxLength(1000)]
        public string? Safety { get; set; }
        [MaxLength(1000)]
        public string? Knowledge { get; set; }
        [MaxLength(1000)]
        public string? Communication { get; set; }
        [MaxLength(1000)]
        public string? Creativity { get; set; }
        [MaxLength(1000)]
        public string? TechnicalAspects { get; set; }
        public int Result { get; set; }
        public int CategoryId { get; set; }
    }
}
