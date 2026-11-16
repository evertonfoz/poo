namespace Atividade03_ProcessamentoDocumentos.Modelo;

/// <summary>
/// Especialização de Documento para textos, artigos ou atas.
/// Contabiliza palavras e herda diretamente a exibição padrão de resumo.
/// </summary>
public class DocumentoTexto : Documento
{
    public string Conteudo { get; private set; } = string.Empty;
    public int QuantidadePalavras { get; private set; }

    public DocumentoTexto(string titulo, string autor, DateTime dataCriacao, string conteudo)
        : base(titulo, autor, dataCriacao)
    {
        DefinirConteudo(conteudo);
    }

    private void DefinirConteudo(string conteudo)
    {
        Conteudo = conteudo ?? string.Empty;
        QuantidadePalavras = ContarPalavras(Conteudo);
    }

    /// <summary>
    /// Conta sequências não vazias separadas por caracteres de espaçamento em branco.
    /// </summary>
    private static int ContarPalavras(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            return 0;

        char[] separadores = [' ', '\t', '\r', '\n'];
        return texto.Split(separadores, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    /// <summary>
    /// Implementa Processar() exibindo a quantidade de palavras do documento.
    /// </summary>
    public override void Processar()
    {
        Console.WriteLine($"[Processando Documento de Texto: \"{Titulo}\"]");
        Console.WriteLine($"  Total de palavras identificadas: {QuantidadePalavras} palavras.");
    }

    // Nota: ExibirResumo() NÃO foi sobrescrito propositalmente para reaproveitar a implementação herdada da classe base.
}
