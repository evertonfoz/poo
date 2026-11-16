namespace Atividade03_ProcessamentoDocumentos.Modelo;

/// <summary>
/// Classe abstrata que representa um documento corporativo genérico.
/// Define os metadados fundamentais (Título, Autor e Data de Criação) e estabelece
/// o contrato de processamento através de método abstrato.
/// </summary>
public abstract class Documento
{
    public string Titulo { get; private set; } = string.Empty;
    public string Autor { get; private set; } = string.Empty;
    public DateTime DataCriacao { get; private set; }

    protected Documento(string titulo, string autor, DateTime dataCriacao)
    {
        DefinirTitulo(titulo);
        DefinirAutor(autor);
        DefinirDataCriacao(dataCriacao);
    }

    private void DefinirTitulo(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("O título do documento é obrigatório e não pode ser vazio.", nameof(titulo));

        Titulo = titulo.Trim();
    }

    private void DefinirAutor(string autor)
    {
        if (string.IsNullOrWhiteSpace(autor))
            throw new ArgumentException("O autor do documento é obrigatório e não pode ser vazio.", nameof(autor));

        Autor = autor.Trim();
    }

    private void DefinirDataCriacao(DateTime dataCriacao)
    {
        if (dataCriacao > DateTime.Now)
            throw new ArgumentOutOfRangeException(nameof(dataCriacao), "A data de criação não pode estar no futuro.");

        DataCriacao = dataCriacao;
    }

    /// <summary>
    /// Método abstrato que força cada tipo concreto de documento a implementar sua própria lógica de processamento.
    /// </summary>
    public abstract void Processar();

    /// <summary>
    /// Método virtual concreto que fornece uma apresentação de resumo padrão.
    /// Pode ser aproveitado ou sobrescrito (override) pelas subclasses.
    /// </summary>
    public virtual void ExibirResumo()
    {
        Console.WriteLine($"[Documento] Título: \"{Titulo}\" | Autor: {Autor} | Data de Criação: {DataCriacao:dd/MM/yyyy}");
    }
}
