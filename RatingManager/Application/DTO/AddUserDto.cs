using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.DTO
{
    public class AddUserDto
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Imię i nazwisko użytkownika")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        [Display(Name = "Adres mailowy")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Rola")]
        public string Role { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Departament")]
        public string Department { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name = "Hasło")]
        public string PasswordHash { get; set; }
    }
}
