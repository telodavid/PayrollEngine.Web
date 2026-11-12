namespace PayrollEngine.Web.Models;

public sealed class LoginRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public sealed class LoginResponse
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public List<LoginRole> Roles { get; set; } = [];
    public List<LoginApp> Apps { get; set; } = [];
}

public sealed class LoginRole
{
    public string Code { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
}

public sealed class LoginApp
{
    public string Code { get; set; } = string.Empty;
    public string AppName { get; set; } = string.Empty;
}