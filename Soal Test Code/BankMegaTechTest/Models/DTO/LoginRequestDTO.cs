using System;
namespace BankMegaTechTest.Models.DTO
{
	public class LoginRequestDTO
	{
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}

