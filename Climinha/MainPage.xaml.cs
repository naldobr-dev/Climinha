using Climinha.Classes;

namespace Climinha;

/// <summary>
/// Página principal do aplicativo que permite ao usuário buscar dados meteorológicos de uma cidade.
/// </summary>
public partial class MainPage : ContentPage
{
    private readonly WeatherService _weatherService; // Instância do serviço de clima para obter dados meteorológicos.

    /// <summary>
    /// Construtor da página principal. Inicializa os componentes e o serviço de clima.
    /// </summary>
    public MainPage()
    {
        InitializeComponent(); // Inicializa os componentes visuais definidos no XAML.
        _weatherService = new WeatherService(); // Cria uma nova instância do serviço de clima.
    }

    /// <summary>
    /// Evento acionado ao clicar no botão de busca. Obtém os dados meteorológicos da cidade informada.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento.</param>
    /// <param name="e">Argumentos do evento.</param>
    private async void OnBuscarClicked(object sender, EventArgs e)
    {
        var cidade = cidadeEntry.Text.Trim(); // Obtém o texto inserido no campo de entrada.

        // Verifica se o campo de entrada está vazio ou contém apenas espaços em branco.
        if (string.IsNullOrWhiteSpace(cidade))
            return;

        // Chama o serviço para buscar os dados meteorológicos da cidade.
        var dados = await _weatherService.GetWeatherAsync(cidade);

        if (dados != null)
        {
            // Caso os dados sejam encontrados, atualiza o contexto de binding e exibe os ícones.
            BindingContext = dados; // Define os dados meteorológicos como contexto de binding.
            iconImage.Source = $"https://openweathermap.org/img/wn/{dados.Icon}@2x.png"; // Define o ícone do clima.

            // Torna os ícones de temperatura, umidade e vento visíveis.
            tempIconImage.IsVisible = true;
            humidityIconImage.IsVisible = true;
            windIconImage.IsVisible = true;
        }
        else
        {
            // Caso a cidade não seja encontrada, exibe um alerta e limpa os dados exibidos.
            await DisplayAlert("Erro", "Cidade não encontrada.", "OK");

            BindingContext = null; // Remove o contexto de binding.
            iconImage.Source = null; // Remove o ícone do clima.

            // Oculta os ícones de temperatura, umidade e vento.
            tempIconImage.IsVisible = false;
            humidityIconImage.IsVisible = false;
            windIconImage.IsVisible = false;
        }
    }
}
