using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public interface IUserService
    {
        Task<User?> Authenticate(string userName, string password);
    }
}
