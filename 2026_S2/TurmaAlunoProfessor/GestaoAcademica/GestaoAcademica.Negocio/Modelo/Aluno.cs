using GestaoAcademica.Negocio.Validacao;

namespace GestaoAcademica.Negocio.Modelo;

public class Aluno
{
    public Guid Id { get; }
    public string Nome { get; private set; }
    public string Email { get; private set; }

    public Aluno(string nome, string email)
        : this(Guid.NewGuid(), nome, email)
    {
    }

    public Aluno(Guid id, string nome, string email)
    {
        Id = Validador.Id(id);
        Nome = Validador.TextoObrigatorio(nome, "Nome");
        Email = Validador.Email(email);
    }

    public void Atualizar(string nome, string email)
    {
        string nomeValido = Validador.TextoObrigatorio(nome, "Nome");
        string emailValido = Validador.Email(email);

        Nome = nomeValido;
        Email = emailValido;
    }

    public override bool Equals(object? obj) =>
        obj is Aluno outro && Id == outro.Id;

    public override int GetHashCode() => Id.GetHashCode();

    public override string ToString() => $"{Nome} <{Email}>";
}
