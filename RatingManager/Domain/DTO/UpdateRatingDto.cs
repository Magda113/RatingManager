﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class UpdateRatingDto
    {
        [Required]
        public int RatingId { get; set; }
        [Required]
        public int ModifiedBy { get; set; }
        [Required]
        [Display(Name = "Numer rozmowy")]
        public string CallId { get; set; }
        [Required]
        public int UserId { get; set; }
        [MaxLength(1000)]
        [Display(Name = "Bezpieczeństwo")]
        public string? Safety { get; set; }
        [MaxLength(1000)]
        [Display(Name = "Wiedza")]
        public string? Knowledge { get; set; }
        [Display(Name = "Komunikacja")]
        [MaxLength(1000)]
        public string? Communication { get; set; }
        public string? Creativity { get; set; }
        [MaxLength(1000)]
        public string? TechnicalAspects { get; set; }
        [Required]
        public int Result { get; set; }
        public int CategoryId { get; set; }
        public RatingStatus Status { get; set; }
    }
}