using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Models;

namespace Application.Services
{
    public interface IUserService
    {
        Task<User?> Authenticate(string userName, string password);
        Task<User> AddUserAsync(AddUserDto userDto);
        Task<IEnumerable<GetUserDto>> GetAllAsync();
        Task<GetUserDto> GetUserByIdAsync(int userId);
        Task<bool> UpdateUserAsync(int userId, UpdateUserDto updateDto);
        Task<bool> DeleteUserAsync(int userId);
    }
}
