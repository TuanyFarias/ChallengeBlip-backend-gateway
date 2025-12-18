namespace ChallengeBlip.Services;

using System;
using System.Globalization;
using System.Net;
using System.Net.Http;    
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ChallengeBlip.Models;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseURL;

    public WeatherService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseURL = configuration["WeatherApi:BaseUrl"] ?? "";
    }

    public async Task<string> GetWeatherAsync(
        string city,
        DateTime date,
        string header,
        string lang)
    {

        city = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(city.ToLowerInvariant());
        if (city.Length < 4)
        {
            throw new ArgumentException("O nome da cidade deve ter pelo menos 4 caracteres.");
        }
        var url = $"{_baseURL}/current.json?key={header}&q={city}&lang={lang}&dt={date:yyyy-MM-dd}";

        try
        {
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();

        }

        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            throw new  InvalidOperationException($"Cidade {city} não encontrada");
        }

        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new InvalidOperationException("Requisição inválida! Tente novamente");
        }

        catch (Exception)
        {
            throw;
        }
    }


    public WeatherResponse JsonFormatted(string result)
    {
        if (!string.IsNullOrEmpty(result))
        {
            var weather = JsonSerializer.Deserialize<WeatherResponse>(result);
            return weather;

        }

        else
        {
            throw new InvalidOperationException("Retorno Vazio");
        } 
    }
}


