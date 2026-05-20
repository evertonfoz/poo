namespace TrabalhoPOO.Models.EntidadesBase;

public class Curso
{
    public string Nome { get; }

    public Curso(string nome)
    {
        Nome = ValidadorDominio.ValidarCampoObrigatorio(nome, nameof(nome));
    }

    public override string ToString()
    {
        return Nome;
    }
}