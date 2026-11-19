using SistemaGestaoEventosAcademicos.Negocio.Enums;
using SistemaGestaoEventosAcademicos.Negocio.Excecoes;

namespace SistemaGestaoEventosAcademicos.Negocio.Entidades;

/// <summary>
/// Representa o evento acadêmico principal (RN01, RN02, RN08, RN12, RN13, RN14, RN15, RN18, RN24).
/// </summary>
public class Evento
{
    private readonly List<Atividade> _atividades = new();

    public int EventoId { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Descricao { get; private set; } = string.Empty;
    public DateTime DataInicio { get; private set; }
    public DateTime DataFim { get; private set; }
    public string Local { get; private set; } = string.Empty;
    public int CapacidadeMaxima { get; private set; }
    public int CargaHoraria { get; private set; }
    public StatusEvento Status { get; private set; }
    public IReadOnlyCollection<Atividade> Atividades => _atividades.AsReadOnly();

    public Evento(
        int eventoId,
        string nome,
        string descricao,
        DateTime dataInicio,
        DateTime dataFim,
        string local,
        int capacidadeMaxima,
        int cargaHoraria)
    {
        ValidarObrigatoriedade(nome, local);

        if (dataFim <= dataInicio)
        {
            throw new RegraNegocioException("A data de término do evento deve ser posterior à data de início.");
        }

        if (dataInicio.Date < DateTime.Now.Date)
        {
            throw new RegraNegocioException("A data de término do evento deve ser posterior à data de início e o evento não pode iniciar no passado.");
        }

        if (capacidadeMaxima <= 0 || cargaHoraria <= 0)
        {
            throw new RegraNegocioException("A capacidade máxima e a carga horária devem ser maiores que zero.");
        }

        EventoId = eventoId;
        Nome = nome.Trim();
        Descricao = descricao?.Trim() ?? string.Empty;
        DataInicio = dataInicio;
        DataFim = dataFim;
        Local = local.Trim();
        CapacidadeMaxima = capacidadeMaxima;
        CargaHoraria = cargaHoraria;
        Status = StatusEvento.Previsto;
    }

    public void AlterarStatus(StatusEvento novoStatus)
    {
        GarantirModificavel();

        if (Status == novoStatus)
        {
            throw new RegraNegocioException($"O evento já se encontra no status {novoStatus}.");
        }

        bool transicaoValida = (Status, novoStatus) switch
        {
            (StatusEvento.Previsto, StatusEvento.InscricoesAbertas) => true,
            (StatusEvento.Previsto, StatusEvento.Cancelado) => true,
            (StatusEvento.InscricoesAbertas, StatusEvento.EmAndamento) => true,
            (StatusEvento.InscricoesAbertas, StatusEvento.Cancelado) => true,
            (StatusEvento.EmAndamento, StatusEvento.Concluido) => true,
            _ => false
        };

        if (!transicaoValida)
        {
            throw new RegraNegocioException($"Transição de status inválida de {Status} para {novoStatus}.");
        }

        Status = novoStatus;
    }

    public void AdicionarAtividade(Atividade atividade)
    {
        ArgumentNullException.ThrowIfNull(atividade);

        GarantirModificavel();

        if (atividade.DataHoraInicio < DataInicio || atividade.DataHoraFim > DataFim)
        {
            throw new RegraNegocioException("O período da atividade deve estar integralmente compreendido entre o início e o término do evento.");
        }

        if (atividade.Vagas > CapacidadeMaxima)
        {
            throw new RegraNegocioException("A quantidade de vagas da atividade deve ser maior que zero e não pode exceder a capacidade máxima do evento.");
        }

        ValidarConflitoLocalAtividades(atividade);

        _atividades.Add(atividade);
    }

    public void RemoverAtividade(int atividadeId, int totalInscritosNaAtividade)
    {
        GarantirModificavel();

        if (totalInscritosNaAtividade > 0)
        {
            throw new RegraNegocioException("Não é possível remover uma atividade que possui participantes inscritos. Cancele as inscrições antes de prosseguir.");
        }

        var atividade = ObterAtividadePorId(atividadeId);
        if (atividade == null)
        {
            throw new RegraNegocioException("Atividade não encontrada no evento.");
        }

        _atividades.Remove(atividade);
    }

    public void AtualizarDados(
        string nome,
        string descricao,
        DateTime dataInicio,
        DateTime dataFim,
        string local,
        int capacidadeMaxima,
        int cargaHoraria,
        int totalInscritosAtuais)
    {
        GarantirModificavel();
        ValidarObrigatoriedade(nome, local);

        if (dataFim <= dataInicio)
        {
            throw new RegraNegocioException("A data de término do evento deve ser posterior à data de início.");
        }

        if (capacidadeMaxima <= 0 || cargaHoraria <= 0)
        {
            throw new RegraNegocioException("A capacidade máxima e a carga horária devem ser maiores que zero.");
        }

        if (capacidadeMaxima < totalInscritosAtuais)
        {
            throw new RegraNegocioException("A capacidade máxima e a carga horária devem ser maiores que zero. A capacidade não pode ser menor que o total de inscritos atuais.");
        }

        if (_atividades.Any(a => a.DataHoraInicio < dataInicio || a.DataHoraFim > dataFim))
        {
            throw new RegraNegocioException("O novo período do evento conflita com o cronograma de atividades já cadastradas.");
        }

        Nome = nome.Trim();
        Descricao = descricao?.Trim() ?? string.Empty;
        DataInicio = dataInicio;
        DataFim = dataFim;
        Local = local.Trim();
        CapacidadeMaxima = capacidadeMaxima;
        CargaHoraria = cargaHoraria;
    }

    public void ValidarConflitoLocalAtividades(Atividade novaAtividade)
    {
        var conflito = _atividades.FirstOrDefault(a => a.AtividadeId != novaAtividade.AtividadeId && novaAtividade.PossuiSobreposicaoLocal(a));
        if (conflito != null)
        {
            throw new RegraNegocioException("O local informado já está reservado para outra atividade no mesmo intervalo de horário.");
        }
    }

    public bool PermiteInscricoes()
    {
        return Status == StatusEvento.InscricoesAbertas;
    }

    public bool EstaEncerrado()
    {
        return Status == StatusEvento.Concluido || Status == StatusEvento.Cancelado;
    }

    public void GarantirModificavel()
    {
        if (EstaEncerrado())
        {
            throw new RegraNegocioException($"O evento está {Status}. Operações de alteração não são permitidas.");
        }
    }

    public Atividade? ObterAtividadePorId(int atividadeId)
    {
        return _atividades.FirstOrDefault(a => a.AtividadeId == atividadeId);
    }

    private static void ValidarObrigatoriedade(string nome, string local)
    {
        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(local))
        {
            throw new RegraNegocioException("Nome e local do evento são de preenchimento obrigatório.");
        }
    }
}
