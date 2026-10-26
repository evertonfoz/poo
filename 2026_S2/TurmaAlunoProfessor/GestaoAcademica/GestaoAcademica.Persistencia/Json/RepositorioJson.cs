using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using GestaoAcademica.Negocio.Contratos;
using GestaoAcademica.Negocio.Modelo;
using GestaoAcademica.Negocio.Validacao;
using GestaoAcademica.Persistencia.Json.Dtos;

namespace GestaoAcademica.Persistencia.Json;

public class RepositorioJson : IRepositorioAcademico
{
    private const int VersaoAtual = 1;

    private static readonly JsonSerializerOptions Opcoes = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        RespectNullableAnnotations = true,
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow
    };

    public string CaminhoArquivo { get; }

    public RepositorioJson(string caminhoArquivo)
    {
        if (string.IsNullOrWhiteSpace(caminhoArquivo))
        {
            throw new ArgumentException(
                "O caminho do arquivo é obrigatório.",
                nameof(caminhoArquivo)
            );
        }

        CaminhoArquivo = Path.GetFullPath(caminhoArquivo);
    }

    public DadosAcademicos Carregar()
    {
        if (!File.Exists(CaminhoArquivo))
        {
            return new DadosAcademicos(
                new List<Professor>(),
                new List<Aluno>(),
                new List<Turma>()
            );
        }

        string json;
        try
        {
            json = File.ReadAllText(CaminhoArquivo);
        }
        catch (Exception ex)
            when (ex is IOException or UnauthorizedAccessException)
        {
            throw new PersistenciaException(
                "Não foi possível ler o arquivo " +
                $"'{CaminhoArquivo}': {ex.Message}",
                ex
            );
        }

        BancoDadosDto? banco;
        try
        {
            banco = JsonSerializer.Deserialize<BancoDadosDto>(json, Opcoes);
        }
        catch (JsonException ex)
        {
            throw new PersistenciaException(
                $"O arquivo '{CaminhoArquivo}' é inválido ou não " +
                $"segue o formato esperado ({ex.Message}). " +
                "O arquivo não foi modificado.",
                ex
            );
        }

        if (banco is null)
        {
            throw new PersistenciaException(
                $"O arquivo '{CaminhoArquivo}' não contém dados. " +
                "O arquivo não foi modificado."
            );
        }

        if (banco.Versao != VersaoAtual)
        {
            throw new PersistenciaException(
                $"Versão de arquivo não suportada: {banco.Versao}. " +
                $"Versão esperada: {VersaoAtual}."
            );
        }

        try
        {
            ValidarEstrutura(banco);
            return Reconstruir(banco);
        }
        catch (RegraDeNegocioException ex)
        {
            throw new PersistenciaException(
                $"Dados inconsistentes em '{CaminhoArquivo}': " +
                ex.Message,
                ex
            );
        }
    }

    // Rejeita null explícito em coleções obrigatórias do JSON.
    private static void ValidarEstrutura(BancoDadosDto banco)
    {
        if (banco.Professores is null
            || banco.Alunos is null
            || banco.Turmas is null)
        {
            throw new RegraDeNegocioException(
                "As listas Professores, Alunos e Turmas são obrigatórias."
            );
        }

        bool itemNulo =
            banco.Professores.Any(p => p is null)
            || banco.Alunos.Any(a => a is null)
            || banco.Turmas.Any(t => t is null);

        if (itemNulo)
        {
            throw new RegraDeNegocioException(
                "As listas não podem conter itens nulos."
            );
        }

        foreach (TurmaDto turma in banco.Turmas)
        {
            if (turma.AlunosIds is null || turma.Atividades is null)
            {
                throw new RegraDeNegocioException(
                    $"A turma '{turma.Nome}' deve informar AlunosIds " +
                    "e Atividades."
                );
            }

            if (turma.Atividades.Any(a => a is null))
            {
                throw new RegraDeNegocioException(
                    $"A turma '{turma.Nome}' contém atividade nula."
                );
            }
        }
    }

    private static DadosAcademicos Reconstruir(BancoDadosDto banco)
    {
        var professores = new Dictionary<Guid, Professor>();
        var emailsProfessores = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase
        );
        foreach (PessoaDto dto in banco.Professores)
        {
            var professor = new Professor(dto.Id, dto.Nome, dto.Email);
            if (!professores.TryAdd(professor.Id, professor))
            {
                throw new RegraDeNegocioException(
                    $"Professor duplicado no arquivo (Id {dto.Id})."
                );
            }
            if (!emailsProfessores.Add(professor.Email))
            {
                throw new RegraDeNegocioException(
                    "E-mail de professor duplicado no arquivo: " +
                    $"{professor.Email}."
                );
            }
        }

        var alunos = new Dictionary<Guid, Aluno>();
        var emailsAlunos = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase
        );
        foreach (PessoaDto dto in banco.Alunos)
        {
            var aluno = new Aluno(dto.Id, dto.Nome, dto.Email);
            if (!alunos.TryAdd(aluno.Id, aluno))
            {
                throw new RegraDeNegocioException(
                    $"Aluno duplicado no arquivo (Id {dto.Id})."
                );
            }

            if (!emailsAlunos.Add(aluno.Email))
            {
                throw new RegraDeNegocioException(
                    "E-mail de aluno duplicado no arquivo: " +
                    $"{aluno.Email}."
                );
            }
        }

        var turmas = new List<Turma>();
        var nomesTurmas = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase
        );
        var idsAtividades = new HashSet<Guid>();
        foreach (TurmaDto dto in banco.Turmas)
        {
            if (!professores.TryGetValue(
                dto.ProfessorId, out Professor? professor))
            {
                throw new RegraDeNegocioException(
                    $"A turma '{dto.Nome}' referencia um professor " +
                    $"inexistente (Id {dto.ProfessorId})."
                );
            }

            if (turmas.Any(t => t.Id == dto.Id))
            {
                throw new RegraDeNegocioException(
                    $"Turma duplicada no arquivo (Id {dto.Id})."
                );
            }

            var turma = new Turma(dto.Id, dto.Nome, professor);

            if (!nomesTurmas.Add(turma.Nome))
            {
                throw new RegraDeNegocioException(
                    "Nome de turma duplicado no arquivo: " +
                    $"{turma.Nome}."
                );
            }

            foreach (Guid alunoId in dto.AlunosIds)
            {
                if (!alunos.TryGetValue(alunoId, out Aluno? aluno))
                {
                    throw new RegraDeNegocioException(
                        $"A turma '{dto.Nome}' referencia um aluno " +
                        $"inexistente (Id {alunoId})."
                    );
                }

                turma.Matricular(aluno);
            }

            foreach (AtividadeDto atividade in dto.Atividades)
            {
                if (!idsAtividades.Add(atividade.Id))
                {
                    throw new RegraDeNegocioException(
                        "Atividade compartilhada entre turmas no " +
                        $"arquivo (Id {atividade.Id})."
                    );
                }

                turma.CadastrarAtividade(
                    atividade.Id,
                    atividade.Titulo,
                    atividade.Descricao,
                    atividade.DataEntrega
                );
            }

            turmas.Add(turma);
        }

        return new DadosAcademicos(
            banco.Professores.Select(p => professores[p.Id]),
            banco.Alunos.Select(a => alunos[a.Id]),
            turmas
        );
    }

    public void Salvar(DadosAcademicos dados)
    {
        var banco = new BancoDadosDto
        {
            Versao = VersaoAtual,
            Professores = dados.Professores
                .Select(p => PessoaParaDto(p.Id, p.Nome, p.Email))
                .ToList(),
            Alunos = dados.Alunos
                .Select(a => PessoaParaDto(a.Id, a.Nome, a.Email))
                .ToList(),
            Turmas = dados.Turmas
                .Select(TurmaParaDto)
                .ToList()
        };

        string temporario = CaminhoArquivo + ".tmp";
        try
        {
            string? pasta = Path.GetDirectoryName(CaminhoArquivo);
            if (!string.IsNullOrEmpty(pasta))
                Directory.CreateDirectory(pasta);

            string json = JsonSerializer.Serialize(banco, Opcoes);
            File.WriteAllText(temporario, json);
            File.Move(temporario, CaminhoArquivo, overwrite: true);
        }
        catch (Exception ex)
            when (ex is IOException
                or UnauthorizedAccessException
                or NotSupportedException)
        {
            RemoverSilenciosamente(temporario);
            throw new PersistenciaException(
                $"Não foi possível gravar '{CaminhoArquivo}': " +
                ex.Message,
                ex
            );
        }
    }

    private static PessoaDto PessoaParaDto(
        Guid id,
        string nome,
        string email)
    {
        return new PessoaDto { Id = id, Nome = nome, Email = email };
    }

    private static TurmaDto TurmaParaDto(Turma turma)
    {
        return new TurmaDto
        {
            Id = turma.Id,
            Nome = turma.Nome,
            ProfessorId = turma.Professor.Id,
            AlunosIds = turma.Alunos.Select(a => a.Id).ToList(),
            Atividades = turma.Atividades
                .Select(a => new AtividadeDto
                {
                    Id = a.Id,
                    Titulo = a.Titulo,
                    Descricao = a.Descricao,
                    DataEntrega = a.DataEntrega
                })
                .ToList()
        };
    }

    private static void RemoverSilenciosamente(string caminho)
    {
        try
        {
            File.Delete(caminho);
        }
        catch (Exception)
        {
            // Limpeza best-effort: a falha original é a relevante.
        }
    }
}
