using GestaoAcademica.Negocio.Validacao;

namespace GestaoAcademica.Negocio.Modelo;

public class Atividade
{
    public Guid Id { get; }
    public Turma Turma { get; }
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public DateOnly DataEntrega { get; private set; }
    public bool Ativa { get; private set; } = true;

    internal Atividade(
        Turma turma,
        Guid id,
        string titulo,
        string descricao,
        DateOnly dataEntrega)
    {
        Turma = turma ?? throw new ArgumentNullException(nameof(turma));
        Id = Validador.Id(id);
        Titulo = Validador.TextoObrigatorio(titulo, "Título");
        Descricao = Validador.TextoObrigatorio(descricao, "Descrição");
        DataEntrega = Validador.Data(dataEntrega, "Data de entrega");
    }

    public void Atualizar(
        string titulo,
        string descricao,
        DateOnly dataEntrega)
    {
        if (!Ativa)
        {
            throw new RegraDeNegocioException(
                "A atividade foi removida e não pode ser alterada."
            );
        }

        string tituloValido =
            Validador.TextoObrigatorio(titulo, "Título");
        string descricaoValida =
            Validador.TextoObrigatorio(descricao, "Descrição");
        DateOnly dataValida =
            Validador.Data(dataEntrega, "Data de entrega");

        Titulo = tituloValido;
        Descricao = descricaoValida;
        DataEntrega = dataValida;
    }

    internal void Descartar() => Ativa = false;

    public override bool Equals(object? obj) =>
        obj is Atividade outra && Id == outra.Id;

    public override int GetHashCode() => Id.GetHashCode();

    public override string ToString() =>
        $"{Titulo} (entrega em {DataEntrega:dd/MM/yyyy})";
}
