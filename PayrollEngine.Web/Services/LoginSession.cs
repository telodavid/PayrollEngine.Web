using PayrollEngine.Web.Models;

namespace PayrollEngine.Web.Services;

public sealed class LoginSession
{
    public LoginResponse? CurrentUser { get; private set; }

    public void SetUser(LoginResponse user)
    {
        CurrentUser = user;
    }

    public void Clear()
    {
        CurrentUser = null;
    }
}