using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices;
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
        Task ConfirmEmailAsync(string id, string token);
        Task SaveChangesAsync();
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

        public async Task ConfirmEmailAsync(string id, string token)
        {

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new ArgumentException("User doesn't exist");
            }

            var response = await _userManager.ConfirmEmailAsync(user, token);

            if (!response.Succeeded)
            {
                throw new Exception(response.Errors.First().Description);
            }

        }

        public Task LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterAsync(RegisterUserRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email!);

            if (user != null)
            {
                throw new ArgumentException("Email already registered");
            }

            var newUser = new UserEntity()
            {
                UserName = request.UserName,
                Email = request.Email,
            };

            var response = await _userManager.CreateAsync(newUser, request.Password!);

            if (!response.Succeeded)
            {
                throw new Exception(response.Errors.First().Description);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var url = $"https://localhost:7200/Authentication/confirm-email?userId={newUser.Id}&token={token}";

            var sendEmailRequest = new SendEmailRequestEntity()
            {
                Subject = "Email Confirmation",
                Body = $"Please click link to confirm - {url}",
                ToAddress = request.Email,
                ConfirmationToken = token,
                UserId = newUser.Id.ToString(),
            };

            await _context.SendEmailRequests.AddAsync(sendEmailRequest);

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
