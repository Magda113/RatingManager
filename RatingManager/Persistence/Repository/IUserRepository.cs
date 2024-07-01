using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
//using IUser = Domain.Models.IUser;

namespace Persistence.Repository
{
    public interface IUserRepository
    {
        Task<User?> Authenticate(string userName, string password);
    }
}
