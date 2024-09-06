using System;
namespace BankMegaTechTest.Models.DTO
{
	public class LoginResponseDTO
	{
        public string UserName { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string Message { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}

