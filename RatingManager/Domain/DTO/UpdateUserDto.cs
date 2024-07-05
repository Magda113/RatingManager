﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class UpdateUserDto
    {
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        public UserRole Role { get; set; }
        [Required]
        [StringLength(100)]
        public string Department { get; set; }
        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }
        public int? ModifiedBy { get; set; }
    }
}