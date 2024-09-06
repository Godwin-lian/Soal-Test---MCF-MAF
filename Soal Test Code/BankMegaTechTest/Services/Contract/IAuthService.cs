using System;
using BankMegaTechTest.Models.DTO;

namespace BankMegaTechTest.Services.Contract
{
	public interface IAuthService
	{
        public Task<LoginResponseDTO> AuthenticateByUsernamePassword(
            LoginRequestDTO request
        );

        public Task<RegisterResponseDTO> Register(
            RegisterRequestDTO request
        );
    }
}

