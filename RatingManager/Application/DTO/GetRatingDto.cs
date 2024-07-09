using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class GetRatingDto
    {
        public int RatingId { get; set; }
        public string Status { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ModifiedByUserName { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string CallId { get; set; }
        public string UserName { get; set; }
        public string? Safety { get; set; }
        public string? Knowledge { get; set; }
        public string? Communication { get; set; }
        public string? Creativity { get; set; }
        public string? TechnicalAspects { get; set; }
        public int Result { get; set; }
        public string CategoryName { get; set; }
    }

}
