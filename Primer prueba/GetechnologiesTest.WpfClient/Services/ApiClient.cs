using System.Net.Http;
using System.Net.Http.Json;

namespace GetechnologiesTest.WpfClient.Services;

public class ApiClient
{
    private readonly HttpClient _http;

    public ApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<int> CrearPersonaAsync(object request)
    {
        var resp = await _http.PostAsJsonAsync("api/personas", request);
        if (!resp.IsSuccessStatusCode)
        {
            var body = await resp.Content.ReadAsStringAsync();
            throw new InvalidOperationException(body);
        }

        var result = await resp.Content.ReadFromJsonAsync<CreatedPersonaResponse>();
        return result?.PersonaId ?? 0;
    }

    private sealed class CreatedPersonaResponse
    {
        public int PersonaId { get; set; }
    }

    public async Task<int> CrearFacturaAsync(object request)
    {
        var resp = await _http.PostAsJsonAsync("api/facturas", request);
        if (!resp.IsSuccessStatusCode)
        {
            var body = await resp.Content.ReadAsStringAsync();
            throw new InvalidOperationException(body);
        }

        var result = await resp.Content.ReadFromJsonAsync<CreatedFacturaResponse>();
        return result?.FacturaId ?? 0;
    }

    private sealed class CreatedFacturaResponse
    {
        public int FacturaId { get; set; }
    }

}
