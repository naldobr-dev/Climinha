using System.Text.Json;

namespace Climinha.Classes;

/// <summary>
/// Responsável por fornecer a chave da API do OpenWeather a partir de um arquivo de configuração.
/// </summary>
public class ApiKeyProvider
{
    /// <summary>
    /// Lê o arquivo 'secrets.json' do pacote do aplicativo e retorna a chave da API do OpenWeather.
    /// </summary>
    /// <returns>
    /// Uma string contendo a chave da API, ou null caso não seja encontrada.
    /// </returns>
    public static async Task<string?> GetOpenWeatherApiKeyAsync()
    {
        // Abre o arquivo 'secrets.json' incluído no pacote do aplicativo.
        using var stream = await FileSystem.OpenAppPackageFileAsync("secrets.json");
        using var reader = new StreamReader(stream);

        // Lê todo o conteúdo do arquivo como uma string JSON.
        var json = await reader.ReadToEndAsync();

        // Faz o parsing do JSON para acessar suas propriedades.
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        // Retorna o valor da chave 'ApiKey' dentro do objeto 'OpenWeather'.
        return root.GetProperty("OpenWeather").GetProperty("ApiKey").GetString();
    }
}
