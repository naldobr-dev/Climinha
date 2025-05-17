using System.Text.Json;

namespace Climinha.Classes;

/// <summary>
/// Serviço responsável por obter dados meteorológicos da API OpenWeather.
/// </summary>
public class WeatherService
{
    private readonly HttpClient _httpClient; // Cliente HTTP usado para realizar requisições à API.
    public const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather"; // URL base da API.

    /// <summary>
    /// Construtor que inicializa o cliente HTTP.
    /// </summary>
    public WeatherService()
    {
        _httpClient = new HttpClient();
    }

    /// <summary>
    /// Obtém os dados meteorológicos de uma cidade específica.
    /// </summary>
    /// <param name="city">Nome da cidade para a qual os dados meteorológicos serão buscados.</param>
    /// <returns>Objeto <see cref="WeatherData"/> contendo as informações meteorológicas ou null se a requisição falhar.</returns>
    public async Task<WeatherData> GetWeatherAsync(string city)
    {
        // Chama o método para obter a chave da API de forma assíncrona.
        var apiKey = await ApiKeyProvider.GetOpenWeatherApiKeyAsync();

        // Monta a URL da requisição com os parâmetros necessários.
        string url = $"{BaseUrl}?q={Uri.EscapeDataString(city)}&appid={apiKey}&units=metric&lang=pt";

        // Realiza a requisição GET para a API.
        var response = await _httpClient.GetAsync(url);

        // Verifica se a resposta foi bem-sucedida. Caso contrário, retorna null.
        if (!response.IsSuccessStatusCode)
            return null;

        // Lê o conteúdo da resposta como string JSON.
        var json = await response.Content.ReadAsStringAsync();

        // Faz o parsing do JSON para extrair os dados necessários.
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;
        var weather = root.GetProperty("weather")[0];
        var main = root.GetProperty("main");
        var wind = root.GetProperty("wind");

        // Retorna um objeto WeatherData preenchido com os dados extraídos.
        return new WeatherData
        {
            City = root.GetProperty("name").GetString(), // Nome da cidade.
            Temperature = main.GetProperty("temp").GetDouble(), // Temperatura em graus Celsius.
            Description = weather.GetProperty("description").GetString(), // Descrição do clima (ex.: "céu limpo").
            Icon = weather.GetProperty("icon").GetString(), // Ícone representando o clima.
            WindSpeed = wind.GetProperty("speed").GetDouble(), // Velocidade do vento em m/s.
            Humidity = main.GetProperty("humidity").GetInt32() // Umidade relativa do ar em porcentagem.
        };
    }
}
