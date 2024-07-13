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
        public RatingStatus Status { get; set; }
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

        public Rating(int ratingId, RatingStatus status, int createdBy, DateTime createdAt, string callId, int userId, int result, int categoryId,
            string? safety = null, string? knowledge = null, string? communication = null, string? creativity = null, string? technicalAspects = null,
            int? modifiedBy = null, DateTime? modifiedAt = null)
        {
            RatingId = ratingId;
            Status = status;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            CallId = callId;
            UserId = userId;
            Result = result;
            CategoryId = categoryId;
            Safety = safety;
            Knowledge = knowledge;
            Communication = communication;
            Creativity = creativity;
            TechnicalAspects = technicalAspects;
            ModifiedBy = modifiedBy;
            ModifiedAt = modifiedAt;
        }

        public Rating()
        {
        }
    }
}
