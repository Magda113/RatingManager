using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class LoginModel
    {
        [Display(Name = "Imię i nazwisko")]
        public string UserName { get; set; }
        [Display(Name = "Hasło")]
        public string Password { get; set; }
    }
}
