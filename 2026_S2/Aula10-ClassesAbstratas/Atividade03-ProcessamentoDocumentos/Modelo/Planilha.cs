namespace Atividade03_ProcessamentoDocumentos.Modelo;

/// <summary>
/// Especialização de Documento que modela matrizes de dados em linhas e colunas.
/// Utiliza o modificador 'new' para OCULTAR (hiding/shadowing) o método ExibirResumo da classe base.
/// </summary>
public class Planilha : Documento
{
    public int QuantidadeLinhas { get; private set; }
    public int QuantidadeColunas { get; private set; }

    public Planilha(
        string titulo,
        string autor,
        DateTime dataCriacao,
        int quantidadeLinhas,
        int quantidadeColunas)
        : base(titulo, autor, dataCriacao)
    {
        DefinirDimensoes(quantidadeLinhas, quantidadeColunas);
    }

    private void DefinirDimensoes(int linhas, int colunas)
    {
        if (linhas <= 0)
            throw new ArgumentOutOfRangeException(nameof(linhas), "A quantidade de linhas deve ser positiva (maior que zero).");

        if (colunas <= 0)
            throw new ArgumentOutOfRangeException(nameof(colunas), "A quantidade de colunas deve ser positiva (maior que zero).");

        QuantidadeLinhas = linhas;
        QuantidadeColunas = colunas;
    }

    /// <summary>
    /// Implementa Processar() exibindo o produto linhas x colunas (células totais).
    /// </summary>
    public override void Processar()
    {
        long totalCelulas = (long)QuantidadeLinhas * QuantidadeColunas;
        Console.WriteLine($"[Processando Planilha: \"{Titulo}\"]");
        Console.WriteLine($"  Dimensão: {QuantidadeLinhas:N0} linhas × {QuantidadeColunas:N0} colunas -> Total de Células: {totalCelulas:N0}");
    }

    /// <summary>
    /// OCULTAÇÃO (Method Hiding) usando a palavra-chave 'new'.
    /// Não sobrescreve a tabela virtual da base.
    /// Chamadas através de referência do tipo Planilha invocam este método.
    /// Chamadas através de referência do tipo Documento invocam o método da classe base!
    /// </summary>
    public new void ExibirResumo()
    {
        Console.WriteLine($"Resumo da planilha: {QuantidadeLinhas} linhas e {QuantidadeColunas} colunas.");
    }
}
