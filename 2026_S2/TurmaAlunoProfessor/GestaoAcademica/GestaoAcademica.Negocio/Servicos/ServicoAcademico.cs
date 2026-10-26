using GestaoAcademica.Negocio.Contratos;
using GestaoAcademica.Negocio.Modelo;
using GestaoAcademica.Negocio.Validacao;

namespace GestaoAcademica.Negocio.Servicos;

public class ServicoAcademico
{
    private readonly IRepositorioAcademico _repositorio;
    private readonly List<Professor> _professores;
    private readonly List<Aluno> _alunos;
    private readonly List<Turma> _turmas;

    public ServicoAcademico(IRepositorioAcademico repositorio)
    {
        _repositorio = repositorio
            ?? throw new ArgumentNullException(nameof(repositorio));

        DadosAcademicos dados = _repositorio.Carregar();
        _professores = dados.Professores.ToList();
        _alunos = dados.Alunos.ToList();
        _turmas = dados.Turmas.ToList();
    }

    private void Persistir()
    {
        _repositorio.Salvar(
            new DadosAcademicos(_professores, _alunos, _turmas)
        );
    }

    private static bool MesmoTexto(string a, string b) =>
        string.Equals(a, b, StringComparison.OrdinalIgnoreCase);

    // ---------- Professores ----------
    public IReadOnlyList<Professor> ListarProfessores() =>
        _professores.AsReadOnly();

    public Professor ObterProfessor(Guid id)
    {
        Professor? professor =
            _professores.FirstOrDefault(p => p.Id == id);

        if (professor is null)
            throw new RegraDeNegocioException("Professor não encontrado.");

        return professor;
    }

    public Professor CadastrarProfessor(string nome, string email)
    {
        var professor = new Professor(nome, email);
        ExigirEmailProfessorLivre(professor.Email, professor.Id);

        _professores.Add(professor);
        Persistir();
        return professor;
    }

    public void AlterarProfessor(Guid id, string nome, string email)
    {
        Professor professor = ObterProfessor(id);
        string emailValido = Validador.Email(email);
        ExigirEmailProfessorLivre(emailValido, id);

        professor.Atualizar(nome, emailValido);
        Persistir();
    }

    public void RemoverProfessor(Guid id)
    {
        Professor professor = ObterProfessor(id);

        List<string> turmas = _turmas
            .Where(t => t.Professor.Equals(professor))
            .Select(t => t.Nome)
            .ToList();

        if (turmas.Count > 0)
        {
            string lista = string.Join(", ", turmas);
            throw new RegraDeNegocioException(
                $"O professor é responsável pelas turmas: {lista}. " +
                "Altere o responsável antes de remover."
            );
        }

        _professores.Remove(professor);
        Persistir();
    }

    private void ExigirEmailProfessorLivre(string email, Guid idAtual)
    {
        bool emUso = _professores.Any(
            p => p.Id != idAtual && MesmoTexto(p.Email, email)
        );

        if (emUso)
        {
            throw new RegraDeNegocioException(
                "Já existe um professor com este e-mail."
            );
        }
    }

    // ---------- Alunos ----------
    public IReadOnlyList<Aluno> ListarAlunos() =>
        _alunos.AsReadOnly();

    public Aluno ObterAluno(Guid id)
    {
        Aluno? aluno =
            _alunos.FirstOrDefault(a => a.Id == id);

        if (aluno is null)
            throw new RegraDeNegocioException("Aluno não encontrado.");

        return aluno;
    }

    public Aluno CadastrarAluno(string nome, string email)
    {
        var aluno = new Aluno(nome, email);
        ExigirEmailAlunoLivre(aluno.Email, aluno.Id);

        _alunos.Add(aluno);
        Persistir();
        return aluno;
    }

    public void AlterarAluno(Guid id, string nome, string email)
    {
        Aluno aluno = ObterAluno(id);
        string emailValido = Validador.Email(email);
        ExigirEmailAlunoLivre(emailValido, id);

        aluno.Atualizar(nome, emailValido);
        Persistir();
    }

    public void RemoverAluno(Guid id)
    {
        Aluno aluno = ObterAluno(id);

        List<string> turmas = _turmas
            .Where(t => t.Alunos.Contains(aluno))
            .Select(t => t.Nome)
            .ToList();

        if (turmas.Count > 0)
        {
            string lista = string.Join(", ", turmas);
            throw new RegraDeNegocioException(
                $"O aluno está matriculado nas turmas: {lista}. " +
                "Cancele as matrículas antes de remover."
            );
        }

        _alunos.Remove(aluno);
        Persistir();
    }

    private void ExigirEmailAlunoLivre(string email, Guid idAtual)
    {
        bool emUso = _alunos.Any(
            a => a.Id != idAtual && MesmoTexto(a.Email, email)
        );

        if (emUso)
        {
            throw new RegraDeNegocioException(
                "Já existe um aluno com este e-mail."
            );
        }
    }

    // ---------- Turmas ----------
    public IReadOnlyList<Turma> ListarTurmas() => _turmas.AsReadOnly();

    public Turma ObterTurma(Guid id)
    {
        Turma? turma = _turmas.FirstOrDefault(t => t.Id == id);

        if (turma is null)
            throw new RegraDeNegocioException("Turma não encontrada.");

        return turma;
    }

    public Turma CadastrarTurma(string nome, Guid professorId)
    {
        Professor professor = ObterProfessor(professorId);
        var turma = new Turma(nome, professor);
        ExigirNomeTurmaLivre(turma.Nome, turma.Id);

        _turmas.Add(turma);
        Persistir();
        return turma;
    }

    public void AlterarTurma(Guid id, string nome, Guid professorId)
    {
        Turma turma = ObterTurma(id);
        Professor professor = ObterProfessor(professorId);
        string nomeValido =
            Validador.TextoObrigatorio(nome, "Nome da turma");
        ExigirNomeTurmaLivre(nomeValido, id);

        turma.Atualizar(nomeValido, professor);
        Persistir();
    }

    // Composição: a exclusão da turma elimina suas atividades.
    // Associação e agregação: professor e alunos permanecem.
    public void RemoverTurma(Guid id)
    {
        Turma turma = ObterTurma(id);
        turma.Encerrar();
        _turmas.Remove(turma);
        Persistir();
    }

    private void ExigirNomeTurmaLivre(string nome, Guid idAtual)
    {
        bool emUso = _turmas.Any(
            t => t.Id != idAtual && MesmoTexto(t.Nome, nome)
        );

        if (emUso)
        {
            throw new RegraDeNegocioException(
                "Já existe uma turma com este nome."
            );
        }
    }

    // ---------- Matrículas ----------
    public void Matricular(Guid turmaId, Guid alunoId)
    {
        Turma turma = ObterTurma(turmaId);
        Aluno aluno = ObterAluno(alunoId);

        turma.Matricular(aluno);
        Persistir();
    }

    public void CancelarMatricula(Guid turmaId, Guid alunoId)
    {
        Turma turma = ObterTurma(turmaId);
        Aluno aluno = ObterAluno(alunoId);

        turma.CancelarMatricula(aluno);
        Persistir();
    }

    // ---------- Atividades (sempre acessadas pela turma) ----------
    public IReadOnlyList<Atividade> ListarAtividades(Guid turmaId) =>
        ObterTurma(turmaId).Atividades;

    public Atividade ObterAtividade(Guid turmaId, Guid atividadeId) =>
        ObterTurma(turmaId).ObterAtividade(atividadeId);

    public Atividade CadastrarAtividade(
        Guid turmaId,
        string titulo,
        string descricao,
        DateOnly dataEntrega)
    {
        Turma turma = ObterTurma(turmaId);
        Atividade atividade = turma.CadastrarAtividade(
            titulo,
            descricao,
            dataEntrega
        );

        Persistir();
        return atividade;
    }

    public void AlterarAtividade(
        Guid turmaId,
        Guid atividadeId,
        string titulo,
        string descricao,
        DateOnly dataEntrega)
    {
        Turma turma = ObterTurma(turmaId);
        turma.AlterarAtividade(
            atividadeId,
            titulo,
            descricao,
            dataEntrega
        );

        Persistir();
    }

    public void RemoverAtividade(Guid turmaId, Guid atividadeId)
    {
        ObterTurma(turmaId).RemoverAtividade(atividadeId);
        Persistir();
    }
}
