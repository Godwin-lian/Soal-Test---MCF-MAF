using System;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BankMegaTechTest.Models.DTO;
using Microsoft.AspNetCore.Authentication;
using BankMegaTechTest.Services.Contract;
using BankMegaTechTest.Data;
using Microsoft.EntityFrameworkCore;
using BankMegaTechTest.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace BankMegaTechTest.Services
{


    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LoginResponseDTO> AuthenticateByUsernamePassword(LoginRequestDTO authReqDto)
        {
            try
            {
                var user = await _context.MsUsers
                    .FirstOrDefaultAsync(u => u.UserName == authReqDto.UserName);

                // Check if user exists and passwords match
                if (user == null || user.Password != authReqDto.Password)
                {
                    throw new Exception($"Invalid username or password.");
                }
                var token = CreateJwtToken(user.UserName);

                // Authentication successful
                return new LoginResponseDTO
                {
                    UserName = authReqDto.UserName,
                    UserId = user.UserId.ToString(),
                    Message = "Authentication successful.",
                    Token = token
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to Authenticate By Username Password: {ex.Message}");
            }
        }

        public async Task<RegisterResponseDTO> Register(RegisterRequestDTO registerRequest)
        {
            try
            {
                var existingUser = await _context.MsUsers
                    .FirstOrDefaultAsync(u => u.UserName == registerRequest.UserName);

                if (existingUser != null)
                {
                    throw new Exception($"Username is already exist");
                }

                // here the password should be hashed, for testing purposes it will be not implemented based on requirement

                var newUser = new MsUser
                {
                    UserName = registerRequest.UserName,
                    Password = registerRequest.Password,
                    IsActive = true
                };

                _context.MsUsers.Add(newUser);
                await _context.SaveChangesAsync();

                return new RegisterResponseDTO
                {
                    UserName = registerRequest.UserName,
                    Message = "User Created Successfully!",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to register user: {ex.Message} {ex.InnerException}");
            }
        }

        private string CreateJwtToken(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("iasoj12idacoaskw12"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}

