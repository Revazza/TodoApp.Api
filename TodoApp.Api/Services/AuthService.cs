using Microsoft.AspNetCore.Identity;
using TodoApp.Api.Db;
using TodoApp.Api.Db.Entities;
using TodoApp.Api.Models.Requests;

namespace TodoApp.Api.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterUserRequest request);
        Task LoginAsync(LoginRequest request);
        Task ChangePasswordAsync(ChangePasswordRequest request);
    }
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly TodoAppDbContext _context;

        public AuthService(
            UserManager<UserEntity> userManager,
            TodoAppDbContext context
            )
        {
            _userManager = userManager;
            _context = context;
        }

        public Task ChangePasswordAsync(ChangePasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public Task LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task RegisterAsync(RegisterUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
