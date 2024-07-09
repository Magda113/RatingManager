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
            var newUser = new User()
            {
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
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
            return users.Select(user => MapUserToDto(user));
        }

        public async Task<GetUserDto> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return MapUserToDto(user);
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateUserDto updateDto)
        {
            int userId = _jwtTokenService.GetUserIdFromToken(_httpContextAccessor.HttpContext);
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            user.UserName = updateDto.UserName;
            user.Email = updateDto.Email;
            user.Role = updateDto.Role;
            user.Department = updateDto.Department;
            user.ModifiedBy = userId;
            user.ModifiedAt = DateTime.Now;

            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await _userRepository.DeleteAsync(userId);
        }

        private GetUserDto MapUserToDto(User user)
        {
            return new GetUserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                Department = user.Department,
                CreatedBy = user.CreatedBy,
                CreatedAt = user.CreatedAt,
                ModifiedBy = user.ModifiedBy,
                ModifiedAt = user.ModifiedAt
            };
        }
    }


}
