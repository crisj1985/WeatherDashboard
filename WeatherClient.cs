using System.Globalization;
using System.Net.Http.Json;

public sealed class WeatherClient
{
    private const string GeocodingUrl = "https://geocoding-api.open-meteo.com/v1/search";
    private const string ForecastUrl = "https://api.open-meteo.com/v1/forecast";

    private readonly HttpClient _http;

    public WeatherClient(HttpClient http)
    {
        _http = http ?? throw new ArgumentNullException(nameof(http));
    }

    /// <summary>Returns the best match for the city, or null if nothing was found.</summary>
    public async Task<GeocodingResult?> GeocodeAsync(string city, CancellationToken ct)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(city);

        var url = $"{GeocodingUrl}?name={Uri.EscapeDataString(city)}&count=1";

        var response = await _http.GetFromJsonAsync<GeocodingResponse>(url, ct);
        return response?.Results?.FirstOrDefault();
    }

    public async Task<CurrentWeather?> GetCurrentWeatherAsync(double lat, double lon, CancellationToken ct)
    {
        // Invariant culture so "44,80" never ends up in the query string on a comma-decimal locale.
        var latitude = lat.ToString(CultureInfo.InvariantCulture);
        var longitude = lon.ToString(CultureInfo.InvariantCulture);

        var url = $"{ForecastUrl}?latitude={latitude}&longitude={longitude}" +
                  "&current=temperature_2m,relative_humidity_2m,wind_speed_10m,weather_code" +
                  "&timezone=auto";

        var response = await _http.GetFromJsonAsync<WeatherResponse>(url, ct);
        return response?.Current;
    }
}