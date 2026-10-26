using GestaoAcademica.Negocio.Validacao;

namespace GestaoAcademica.Negocio.Modelo;

public class Turma
{
    private readonly List<Aluno> _alunos = new();
    private readonly List<Atividade> _atividades = new();

    public Guid Id { get; }
    public string Nome { get; private set; }
    public Professor Professor { get; private set; }
    public bool Ativa { get; private set; } = true;

    public IReadOnlyList<Aluno> Alunos => _alunos.AsReadOnly();

    public IReadOnlyList<Atividade> Atividades =>
        _atividades.AsReadOnly();

    public Turma(string nome, Professor professor)
        : this(Guid.NewGuid(), nome, professor)
    {
    }

    public Turma(Guid id, string nome, Professor professor)
    {
        Id = Validador.Id(id);
        Nome = Validador.TextoObrigatorio(nome, "Nome da turma");
        Professor = ExigirProfessor(professor);
    }

    public void Atualizar(string nome, Professor professor)
    {
        ExigirAtiva();

        string nomeValido =
            Validador.TextoObrigatorio(nome, "Nome da turma");
        Professor novoProfessor = ExigirProfessor(professor);

        Nome = nomeValido;
        Professor = novoProfessor;
    }

    // Agregação: o aluno é apenas vinculado à turma.
    public void Matricular(Aluno aluno)
    {
        ExigirAtiva();

        if (aluno is null)
            throw new RegraDeNegocioException("Aluno inválido.");

        if (_alunos.Contains(aluno))
        {
            throw new RegraDeNegocioException(
                $"O aluno {aluno.Nome} já está matriculado nesta turma."
            );
        }

        _alunos.Add(aluno);
    }

    public void CancelarMatricula(Aluno aluno)
    {
        ExigirAtiva();

        if (aluno is null || !_alunos.Remove(aluno))
        {
            throw new RegraDeNegocioException(
                "O aluno não está matriculado nesta turma."
            );
        }
    }

    // Composição: apenas a turma cria, altera e remove suas atividades.
    public Atividade CadastrarAtividade(
        string titulo,
        string descricao,
        DateOnly dataEntrega)
    {
        return CadastrarAtividade(
            Guid.NewGuid(),
            titulo,
            descricao,
            dataEntrega
        );
    }

    public Atividade CadastrarAtividade(
        Guid id,
        string titulo,
        string descricao,
        DateOnly dataEntrega)
    {
        ExigirAtiva();

        if (_atividades.Any(a => a.Id == id))
        {
            throw new RegraDeNegocioException(
                "Já existe uma atividade com este identificador " +
                "na turma."
            );
        }

        var atividade = new Atividade(
            this,
            id,
            titulo,
            descricao,
            dataEntrega
        );
        _atividades.Add(atividade);
        return atividade;
    }

    public Atividade ObterAtividade(Guid id)
    {
        Atividade? atividade =
            _atividades.FirstOrDefault(a => a.Id == id);

        if (atividade is null)
        {
            throw new RegraDeNegocioException(
                "Atividade não encontrada nesta turma."
            );
        }

        return atividade;
    }

    public void AlterarAtividade(
        Guid id,
        string titulo,
        string descricao,
        DateOnly dataEntrega)
    {
        ExigirAtiva();
        ObterAtividade(id).Atualizar(titulo, descricao, dataEntrega);
    }

    public void RemoverAtividade(Guid id)
    {
        ExigirAtiva();

        Atividade atividade = ObterAtividade(id);
        _atividades.Remove(atividade);
        atividade.Descartar();
    }

    // Chamado na exclusão da turma: as atividades deixam de existir;
    // professor e cadastros de alunos permanecem intactos.
    public void Encerrar()
    {
        foreach (Atividade atividade in _atividades)
            atividade.Descartar();

        _atividades.Clear();
        _alunos.Clear();
        Ativa = false;
    }

    private static Professor ExigirProfessor(Professor? professor)
    {
        if (professor is null)
        {
            throw new RegraDeNegocioException(
                "A turma exige um professor responsável."
            );
        }

        return professor;
    }

    private void ExigirAtiva()
    {
        if (!Ativa)
            throw new RegraDeNegocioException("A turma foi encerrada.");
    }

    public override bool Equals(object? obj) =>
        obj is Turma outra && Id == outra.Id;

    public override int GetHashCode() => Id.GetHashCode();

    public override string ToString() =>
        $"{Nome} (Prof. {Professor.Nome})";
}
