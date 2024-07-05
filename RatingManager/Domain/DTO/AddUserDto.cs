using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.DTO
{
    public class AddUserDto
    {
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
    }
}
