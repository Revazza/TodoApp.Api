using Azure.Core;
using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices;
using System.Web;
using TodoApp.Api.Auth;
using TodoApp.Api.Db;
using TodoApp.Api.Db.Entities;
using TodoApp.Api.Db.RequestEntities;
using TodoApp.Api.Models.Requests;

namespace TodoApp.Api.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterUserRequest request);
        Task<string> LoginAsync(LoginRequest request);
        Task ResetPasswordAsync(ResetPasswordRequest request);
        Task ConfirmEmailAsync(string id, string token);
        Task SaveChangesAsync();
    }
    public class AuthService : IAuthService
    {
        private const string URL = "https://localhost:7200";
        private readonly UserManager<UserEntity> _userManager;
        private readonly TodoAppDbContext _context;
        private readonly TokenGenerator _generator;

        public AuthService(
            UserManager<UserEntity> userManager,
            TodoAppDbContext context,
            TokenGenerator generator
            )
        {
            _userManager = userManager;
            _context = context;
            _generator = generator;
        }

        public async Task ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email!);

            if (user == null)
            {
                throw new ArgumentException("Incorrect email");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(token);



            var url = $"{URL}/Authentication/reset-password?userId={user.Id}&token={encodedToken}";
            var resetPasswordRequest = new SendResetPasswordRequestEntity()
            {
                Subject = "Reset Password",
                Body = $"Please click this link to reset password - {url}",
                ToAddress = user.Email,
                UserId = user.Id.ToString(),
                NewPassword = request.NewPassword,
            };

            await _context.SendResetPasswordRequests.AddAsync(resetPasswordRequest);

        }
        public async Task ConfirmEmailAsync(string id, string token)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new ArgumentException("User doesn't exist");
            }

            if (user.EmailConfirmed)
            {
                throw new Exception("Email already confirmed");
            }

            var response = await _userManager.ConfirmEmailAsync(user, token);

            if (!response.Succeeded)
            {
                throw new Exception(response.Errors.First().Description);
            }
        }
        public async Task<string> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email!);
            if (user == null)
            {
                throw new ArgumentException("Incorrect Credentials");
            }


            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, request.Password!);

            if (!isPasswordCorrect)
            {
                throw new ArgumentException("Incorrect Credentials");
            }

            var userClaims = await _userManager.GetClaimsAsync(user);

            var jwtToken = _generator.GenerateToken(user, userClaims.ToList());

            return jwtToken;
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

            await _userManager.AddToRoleAsync(newUser, request.Role!);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var encodedToken = HttpUtility.UrlEncode(token);
            var url = $"{URL}/Authentication/confirm-email?userId={newUser.Id}&token={encodedToken}";

            var sendEmailRequest = new SendEmailRequestEntity()
            {
                Subject = "Email Confirmation",
                Body = $"Please click link to confirm - {url}",
                ToAddress = newUser.Email,
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
