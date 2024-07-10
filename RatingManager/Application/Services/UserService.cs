using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Auth;
using Application.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Persistence.Repository;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly Auth.JwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository, JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<User?> Authenticate(string userName, string password)
        {
            return await _userRepository.Authenticate(userName, password);
        }

        public async Task<User> AddUserAsync(AddUserDto user)
        {
            int userId = _jwtTokenService.GetUserIdFromToken(_httpContextAccessor.HttpContext);

            // Map role name to UserRole enum
            if (!Enum.TryParse<UserRole>(user.Role, out var userRole))
            {
                throw new ArgumentException($"Nie ma takiej roli: {user.Role}");
            }

            var newUser = new User()
            {
                UserName = user.UserName,
                Email = user.Email,
                Role = userRole,
                Department = user.Department,
                PasswordHash = user.PasswordHash,
                CreatedBy = userId,
                CreatedAt = DateTime.Now
            };

            newUser.UserId = await _userRepository.AddAsync(newUser);
            return newUser;
        }

        public async Task<IEnumerable<GetUserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var usersDto = new List<GetUserDto>();

            foreach (var user in users)
            {
                usersDto.Add(await MapUserToDto(user));
            }

            return usersDto;
        }

        public async Task<GetUserDto> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            return await MapUserToDto(user);
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateUserDto updateDto)
        {
            int userId = _jwtTokenService.GetUserIdFromToken(_httpContextAccessor.HttpContext);
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            // Update user fields
            user.UserName = updateDto.UserName;
            user.Email = updateDto.Email;
            user.Department = updateDto.Department;
            user.ModifiedBy = userId;
            user.ModifiedAt = DateTime.Now;

            // Map role name to UserRole enum
            if (!Enum.TryParse<UserRole>(updateDto.Role, out var userRole))
            {
                throw new ArgumentException($"Nie ma takiej roli: {updateDto.Role}");
            }
            user.Role = userRole;

            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await _userRepository.DeleteAsync(userId);
        }

        private async Task<GetUserDto> MapUserToDto(User user)
        {
            string createdByFullName = await GetFullNameById(user.CreatedBy);

            string modifiedByFullName = user.ModifiedBy.HasValue ? await GetFullNameById(user.ModifiedBy.Value) : null;

            return new GetUserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role.ToString(),
                Department = user.Department,
                CreatedByFullName = createdByFullName,
                CreatedAt = user.CreatedAt,
                ModifiedByFullName = modifiedByFullName,
                ModifiedAt = user.ModifiedAt
            };
        }

        private async Task<string> GetFullNameById(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                return user.UserName; 
            }
            else
            {
                return "Nieznany użytkownik";
            }
        }
    }
}
