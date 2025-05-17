using System.Text.Json.Serialization;

namespace Climinha.Classes;

/// <summary>
/// Classe que representa os dados meteorológicos de uma cidade.
/// </summary>
public class WeatherData
{
    public string City { get; set; } // Nome da cidade
    public double Temperature { get; set; } // Temperatura em graus Celsius
    public string Description { get; set; } // Descrição do clima (ex.: "céu limpo")
    public string Icon { get; set; } // Ícone representando o clima
    public double WindSpeed { get; set; } // Velocidade do vento em m/s
    public int Humidity { get; set; } // Umidade relativa do ar em porcentagem

    [JsonIgnore]
    public int TemperatureInt => (int)Temperature; // Propriedade para obter a temperatura como inteiro
}
