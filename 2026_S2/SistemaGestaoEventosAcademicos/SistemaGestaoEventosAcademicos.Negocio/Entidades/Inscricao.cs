using SistemaGestaoEventosAcademicos.Negocio.Enums;
using SistemaGestaoEventosAcademicos.Negocio.Excecoes;

namespace SistemaGestaoEventosAcademicos.Negocio.Entidades;

/// <summary>
/// Representa o vínculo formal de um participante com um evento e suas atividades (RN04, RN05, RN06, RN07, RN14, RN16, RN17, RN19, RN20, RN21, RN22, RN23, RN25).
/// </summary>
public class Inscricao
{
    private readonly List<Atividade> _atividadesInscritas = new();

    public int InscricaoId { get; private set; }
    public Participante Participante { get; private set; }
    public Evento Evento { get; private set; }
    public DateTime DataInscricao { get; private set; }
    public StatusInscricao Status { get; private set; }
    public bool PresencaConfirmada { get; private set; }
    public DateTime? DataHoraCheckin { get; private set; }
    public IReadOnlyCollection<Atividade> AtividadesInscritas => _atividadesInscritas.AsReadOnly();

    public Inscricao(int inscricaoId, Participante participante, Evento evento)
    {
        ArgumentNullException.ThrowIfNull(participante);
        ArgumentNullException.ThrowIfNull(evento);

        if (!evento.PermiteInscricoes())
        {
            throw new RegraNegocioException("Inscrições indisponíveis. O evento não se encontra com inscrições abertas.");
        }

        InscricaoId = inscricaoId;
        Participante = participante;
        Evento = evento;
        DataInscricao = DateTime.Now;
        Status = StatusInscricao.Ativa;
        PresencaConfirmada = false;
        DataHoraCheckin = null;
    }

    public void InscreverAtividade(Atividade atividade, int totalInscritosNaAtividade)
    {
        ArgumentNullException.ThrowIfNull(atividade);

        if (!EstaAtiva())
        {
            throw new RegraNegocioException("Não é possível inscrever em atividades para uma inscrição cancelada.");
        }

        if (atividade.EventoId != Evento.EventoId)
        {
            throw new RegraNegocioException("Para participar desta atividade, é obrigatório estar inscrito previamente no evento correspondente.");
        }

        if (_atividadesInscritas.Any(a => a.AtividadeId == atividade.AtividadeId))
        {
            throw new RegraNegocioException("O participante já está inscrito nesta atividade.");
        }

        if (totalInscritosNaAtividade >= atividade.Vagas)
        {
            throw new RegraNegocioException("As vagas para esta atividade estão esgotadas.");
        }

        var atividadeConflitante = _atividadesInscritas.FirstOrDefault(a => a.ConflitaHorarioCom(atividade));
        if (atividadeConflitante != null)
        {
            throw new RegraNegocioException($"Conflito de horário detectado: você já está inscrito na atividade '{atividadeConflitante.Titulo}' que ocorre no mesmo horário.");
        }

        _atividadesInscritas.Add(atividade);
    }

    public void Cancelar()
    {
        ValidarPrazoCancelamento();

        Status = StatusInscricao.Cancelada;
        _atividadesInscritas.Clear();
    }

    public bool ConfirmarPresenca()
    {
        if (!EstaAtiva())
        {
            throw new RegraNegocioException("Não é possível registrar presença para uma inscrição cancelada.");
        }

        if (Evento.Status != StatusEvento.EmAndamento)
        {
            throw new RegraNegocioException("O registro de presença só pode ser efetuado enquanto o evento estiver em andamento.");
        }

        if (PresencaConfirmada)
        {
            return false;
        }

        PresencaConfirmada = true;
        DataHoraCheckin = DateTime.Now;
        return true;
    }

    public void ValidarPrazoCancelamento()
    {
        if (Evento.Status != StatusEvento.InscricoesAbertas || DateTime.Now >= Evento.DataInicio)
        {
            throw new RegraNegocioException("Não é permitido cancelar a inscrição após o início ou encerramento do evento.");
        }
    }

    public Certificado EmitirCertificado()
    {
        if (Evento.Status != StatusEvento.Concluido || !PresencaConfirmada || !EstaAtiva())
        {
            throw new RegraNegocioException("Certificado indisponível. O evento ainda não foi concluído ou a presença não foi registrada.");
        }

        return new Certificado(
            InscricaoId,
            Participante.Nome,
            Participante.Cpf,
            Evento.Nome,
            Evento.CargaHoraria);
    }

    public bool EstaAtiva()
    {
        return Status == StatusInscricao.Ativa;
    }

    public bool PossuiConflitoHorario(Atividade novaAtividade)
    {
        return _atividadesInscritas.Any(a => a.ConflitaHorarioCom(novaAtividade));
    }
}
