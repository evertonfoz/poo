using System.Text.RegularExpressions;

namespace Aula009_HerancaVeiculo.Modelo;

public class Veiculo
{
    public string Placa { get; private set; } = string.Empty;
    public string Marca { get; private set; } = string.Empty;
    public string Modelo { get; private set; } = string.Empty;
    public int AnoFabricacao { get; private set; }
    public double Quilometragem { get; private set; }

    public Veiculo(
        string placa,
        string marca,
        string modelo,
        int anoFabricacao,
        double quilometragemInicial)
    {
        DefinirPlaca(placa);
        DefinirMarca(marca);
        DefinirModelo(modelo);
        DefinirAnoFabricacao(anoFabricacao);
        DefinirQuilometragemInicial(quilometragemInicial);
    }

    // Métodos de inicialização e validação (dados cadastrais imutáveis de um veículo)
    private void DefinirPlaca(string placa) // Regular Expression - RegEx
    {
        if (string.IsNullOrWhiteSpace(placa))
            throw new ArgumentException("A placa do veículo é obrigatória e não pode ser vazia.", nameof(placa));

        string placaNormalizada = placa.Trim().ToUpperInvariant();

        // Padrão Mercosul (Brasil): 3 letras, 1 número, 1 letra e 2 números (ex: ABC1D23 ou ABC-1D23)
        const string padraoMercosul = @"^[A-Z]{3}-?[0-9][A-Z][0-9]{2}$";

        if (!Regex.IsMatch(placaNormalizada, padraoMercosul))
            throw new ArgumentException("A placa informada é inválida. O padrão Mercosul (Brasil) requer 3 letras, 1 número, 1 letra e 2 números (ex: ABC1D23).", nameof(placa));

        Placa = placaNormalizada;
    }

    private void DefinirMarca(string marca)
    {
        if (string.IsNullOrWhiteSpace(marca))
            throw new ArgumentException("A marca do veículo é obrigatória e não pode ser vazia.", nameof(marca));

        Marca = marca.Trim();
    }

    private void DefinirModelo(string modelo)
    {
        if (string.IsNullOrWhiteSpace(modelo))
            throw new ArgumentException("O modelo do veículo é obrigatório e não pode ser vazio.", nameof(modelo));

        Modelo = modelo.Trim();
    }

    private void DefinirAnoFabricacao(int ano)
    {
        // Justificativa: 1886 marca o primeiro automóvel moderno patenteado por Karl Benz.
        // O limite superior é o ano seguinte ao atual (DateTime.Now.Year + 1) para contemplar modelos lançados antecipadamente.
        int anoMinimo = 1886;
        int anoMaximo = DateTime.Now.Year + 1;

        if (ano < anoMinimo || ano > anoMaximo)
            throw new ArgumentOutOfRangeException(nameof(ano), $"O ano de fabricação deve estar entre {anoMinimo} e {anoMaximo}.");

        AnoFabricacao = ano;
    }

    private void DefinirQuilometragemInicial(double quilometragemInicial)
    {
        if (quilometragemInicial < 0)
            throw new ArgumentOutOfRangeException(nameof(quilometragemInicial), "A quilometragem inicial não pode ser negativa.");

        Quilometragem = quilometragemInicial;
    }

    // Método semântico de negócio: registra e acumula quilômetros rodados
    public void RegistrarQuilometragem(double quilometros)
    {
        if (quilometros <= 0)
            throw new ArgumentOutOfRangeException(nameof(quilometros), "A quilometragem a ser registrada deve ser estritamente maior que zero.");

        Quilometragem += quilometros;
    }

    public void ExibirDados()
    {
        Console.WriteLine($"Placa: {Placa} | Veículo: {Marca} {Modelo} ({AnoFabricacao}) | Km Atual: {Quilometragem:N1} km");
    }
}

