using System;
namespace BankMegaTechTest.Models.DTO
{
    public class RegisterResponseDTO
    {
        public string UserName { get; set; } = String.Empty;
        public string Message { get; set; } = String.Empty;
        public bool Success { get; set; }
    }
}

