namespace PayrollEngine.Web.Services;

public sealed class FeedbackService
{
    public event Action? Changed;
    public string? SuccessMessage { get; private set; }
    public string? ErrorMessage { get; private set; }

    public void ShowSuccess(string message)
    {
        if (string.IsNullOrWhiteSpace(message)) return;
        SuccessMessage = message;
        ErrorMessage = null;
        Changed?.Invoke();
    }

    public void ShowError(string message)
    {
        if (string.IsNullOrWhiteSpace(message)) return;
        SuccessMessage = null;
        ErrorMessage = message;
        Changed?.Invoke();
    }

    public void DismissSuccess()
    {
        SuccessMessage = null;
        Changed?.Invoke();
    }

    public void DismissError()
    {
        ErrorMessage = null;
        Changed?.Invoke();
    }
}
