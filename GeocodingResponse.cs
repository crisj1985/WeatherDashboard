using System.Text.Json.Serialization;

// ---------- Geocoding response ----------

public record GeocodingResponse(
    [property: JsonPropertyName("results")] List<GeocodingResult> Results
);

public record GeocodingResult(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("latitude")] double Latitude,
    [property: JsonPropertyName("longitude")] double Longitude,
    [property: JsonPropertyName("elevation")] double Elevation,
    [property: JsonPropertyName("country")] string Country,
    [property: JsonPropertyName("country_code")] string CountryCode,
    [property: JsonPropertyName("timezone")] string Timezone,
    [property: JsonPropertyName("population")] int Population
);

// ---------- Weather response ----------

public record WeatherResponse(
    [property: JsonPropertyName("latitude")] double Latitude,
    [property: JsonPropertyName("longitude")] double Longitude,
    [property: JsonPropertyName("current")] CurrentWeather Current
);

public record CurrentWeather(
    [property: JsonPropertyName("time")] DateTime Time,
    [property: JsonPropertyName("temperature_2m")] double Temperature,
    [property: JsonPropertyName("relative_humidity_2m")] int RelativeHumidity,
    [property: JsonPropertyName("wind_speed_10m")] double WindSpeed,
    [property: JsonPropertyName("weather_code")] int WeatherCode
);