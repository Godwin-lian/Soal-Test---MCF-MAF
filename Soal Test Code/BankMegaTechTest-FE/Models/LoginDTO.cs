namespace BankMegaTechTest_FE.Models;

public class LoginRequestDTO
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class LoginResponseDTO
{
    public string UserName { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string Message { get; set; } = default!;
    public string Token { get; set; } = default!;
}

