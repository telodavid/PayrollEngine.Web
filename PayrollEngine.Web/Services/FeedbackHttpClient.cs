using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace PayrollEngine.Web.Services;

public sealed class FeedbackHttpClient(HttpClient http, FeedbackService feedback)
{
    public async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        string? fallbackSuccessMessage = null,
        string? fallbackErrorMessage = null)
    {
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        HttpResponseMessage response;
        try
        {
            response = await http.SendAsync(request);
        }
        catch (HttpRequestException)
        {
            feedback.ShowError("A szerver nem érhető el. Ellenőrizd a kapcsolatot, majd próbáld újra.");
            throw;
        }

        var message = await ReadMessageAsync(response);
        if (response.IsSuccessStatusCode)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                feedback.ShowSuccess(message);
            }
            else if (!string.IsNullOrWhiteSpace(fallbackSuccessMessage))
            {
                feedback.ShowSuccess(fallbackSuccessMessage);
            }
        }
        else
        {
            feedback.ShowError(message ?? fallbackErrorMessage ??
                (response.StatusCode == HttpStatusCode.Unauthorized
                    ? "A bejelentkezés szükséges vagy lejárt."
                    : $"A kérés sikertelen. HTTP {(int)response.StatusCode}."));
        }

        return response;
    }

    private static async Task<string?> ReadMessageAsync(HttpResponseMessage response)
    {
        if (response.Content is null) return null;

        var body = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(body)) return null;

        try
        {
            using var document = JsonDocument.Parse(body);
            if (document.RootElement.ValueKind == JsonValueKind.Object)
            {
                foreach (var property in document.RootElement.EnumerateObject())
                {
                    var recognized = property.Name.Equals("message", StringComparison.OrdinalIgnoreCase)
                        || property.Name.Equals("title", StringComparison.OrdinalIgnoreCase)
                        || property.Name.Equals("detail", StringComparison.OrdinalIgnoreCase);

                    if (recognized && property.Value.ValueKind == JsonValueKind.String)
                    {
                        var text = property.Value.GetString();
                        if (!string.IsNullOrWhiteSpace(text)) return text;
                    }
                }
            }
        }
        catch (JsonException)
        {
            return response.IsSuccessStatusCode ? null : body.Trim();
        }

        return null;
    }
}
