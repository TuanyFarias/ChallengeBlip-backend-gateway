namespace ChallengeBlip.Models;
using System.Text.Json.Serialization;

public class WeatherResponse
{
    [JsonPropertyName("current")]
    public Current? Current { get; set; }
}

public class Current
{
    [JsonPropertyName("last_updated")]
    public string LastUpdated { get; set; } = string.Empty;

    [JsonPropertyName("temp_c")]
    public decimal TempC { get; set; }

    [JsonPropertyName("feelslike_c")]
    public decimal FeelsLikeC { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    [JsonPropertyName("cloud")]
    public int Cloud { get; set; }

    [JsonPropertyName("is_day")]
    public int IsDay { get; set; }

    [JsonPropertyName("condition")]
    public Condition? Condition { get; set; }
}

public class Condition
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("icon")]
    public string Icon { get; set; } = string.Empty;

    [JsonPropertyName("code")]
    public int Code { get; set; }
}
