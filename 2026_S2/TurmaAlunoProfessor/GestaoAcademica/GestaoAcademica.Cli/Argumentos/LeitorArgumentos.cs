using System.Globalization;

namespace GestaoAcademica.Apresentacao.Argumentos;

public class LeitorArgumentos
{
    private readonly Dictionary<string, string> _valores = new();

    public LeitorArgumentos(
        IReadOnlyList<string> argumentos,
        params string[] permitidas)
    {
        for (int i = 0; i < argumentos.Count; i += 2)
        {
            string opcao = argumentos[i];

            if (!opcao.StartsWith("--"))
            {
                throw new UsoInvalidoException(
                    $"Argumento inesperado: '{opcao}'."
                );
            }

            if (!permitidas.Contains(opcao))
            {
                throw new UsoInvalidoException(
                    $"Opção desconhecida: {opcao}."
                );
            }

            if (i + 1 >= argumentos.Count)
            {
                throw new UsoInvalidoException(
                    $"A opção {opcao} exige um valor."
                );
            }

            if (argumentos[i + 1].StartsWith("--"))
            {
                throw new UsoInvalidoException(
                    $"A opção {opcao} exige um valor, mas recebeu " +
                    $"a opção '{argumentos[i + 1]}'."
                );
            }

            if (!_valores.TryAdd(opcao, argumentos[i + 1]))
            {
                throw new UsoInvalidoException(
                    $"Opção repetida: {opcao}."
                );
            }
        }
    }

    public string Texto(string opcao)
    {
        if (!_valores.TryGetValue(opcao, out string? valor))
        {
            throw new UsoInvalidoException(
                $"Opção obrigatória ausente: {opcao}."
            );
        }

        return valor;
    }

    public Guid Identificador(string opcao)
    {
        string texto = Texto(opcao);

        if (!Guid.TryParse(texto, out Guid id))
        {
            throw new UsoInvalidoException(
                $"Valor inválido para {opcao}: '{texto}' não é um Guid."
            );
        }

        return id;
    }

    public DateOnly Data(string opcao)
    {
        string texto = Texto(opcao);

        if (!DateOnly.TryParseExact(
            texto,
            "yyyy-MM-dd",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out DateOnly data))
        {
            throw new UsoInvalidoException(
                $"Data inválida em {opcao}: use o formato yyyy-MM-dd."
            );
        }

        return data;
    }
}
