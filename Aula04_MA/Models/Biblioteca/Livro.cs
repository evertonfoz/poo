public record Livro
{
    public string Titulo { get; init; }
    public string Autor { get; init; }

    public Livro(string titulo, string autor)
    {
        Titulo = titulo;
        Autor = autor;
    }
}