using SistemaGestaoEventosAcademicos.Negocio.Enums;
using SistemaGestaoEventosAcademicos.Negocio.Excecoes;

namespace SistemaGestaoEventosAcademicos.Negocio.Entidades;

/// <summary>
/// Representa uma sessão específica (palestra, minicurso, workshop, etc.) vinculada a um evento (RN02, RN15, RN18, RN24, RN25).
/// </summary>
public class Atividade
{
    public int AtividadeId { get; private set; }
    public int EventoId { get; private set; }
    public string Titulo { get; private set; } = string.Empty;
    public string Descricao { get; private set; } = string.Empty;
    public TipoAtividade Tipo { get; private set; }
    public DateTime DataHoraInicio { get; private set; }
    public DateTime DataHoraFim { get; private set; }
    public string Local { get; private set; } = string.Empty;
    public int Vagas { get; private set; }
    public string Instrutor { get; private set; } = string.Empty;

    public Atividade(
        int atividadeId,
        int eventoId,
        string titulo,
        string descricao,
        TipoAtividade tipo,
        DateTime dataHoraInicio,
        DateTime dataHoraFim,
        string local,
        int vagas,
        string instrutor)
    {
        ValidarObrigatoriedade(titulo, local, instrutor);

        if (dataHoraFim <= dataHoraInicio)
        {
            throw new RegraNegocioException("A data/hora de término da atividade deve ser estritamente posterior à data/hora de início.");
        }

        if (vagas <= 0)
        {
            throw new RegraNegocioException("A quantidade de vagas da atividade deve ser maior que zero.");
        }

        AtividadeId = atividadeId;
        EventoId = eventoId;
        Titulo = titulo.Trim();
        Descricao = descricao?.Trim() ?? string.Empty;
        Tipo = tipo;
        DataHoraInicio = dataHoraInicio;
        DataHoraFim = dataHoraFim;
        Local = local.Trim();
        Vagas = vagas;
        Instrutor = instrutor.Trim();
    }

    public void AtualizarDados(
        string titulo,
        string descricao,
        TipoAtividade tipo,
        DateTime dataHoraInicio,
        DateTime dataHoraFim,
        string local,
        int vagas,
        string instrutor,
        int totalInscritosAtuais)
    {
        ValidarObrigatoriedade(titulo, local, instrutor);

        if (dataHoraFim <= dataHoraInicio)
        {
            throw new RegraNegocioException("A data/hora de término da atividade deve ser estritamente posterior à data/hora de início.");
        }

        if (vagas <= 0)
        {
            throw new RegraNegocioException("A quantidade de vagas da atividade deve ser maior que zero.");
        }

        if (vagas < totalInscritosAtuais)
        {
            throw new RegraNegocioException($"A quantidade de vagas ({vagas}) não pode ser inferior ao total de inscritos atuais ({totalInscritosAtuais}).");
        }

        Titulo = titulo.Trim();
        Descricao = descricao?.Trim() ?? string.Empty;
        Tipo = tipo;
        DataHoraInicio = dataHoraInicio;
        DataHoraFim = dataHoraFim;
        Local = local.Trim();
        Vagas = vagas;
        Instrutor = instrutor.Trim();
    }

    public int CalcularDuracaoEmHoras()
    {
        var horas = (DataHoraFim - DataHoraInicio).TotalHours;
        return (int)Math.Max(1, Math.Ceiling(horas));
    }

    public bool ConflitaHorarioCom(Atividade outra)
    {
        return (DataHoraInicio < outra.DataHoraFim) && (DataHoraFim > outra.DataHoraInicio);
    }

    public bool PossuiSobreposicaoLocal(Atividade outra)
    {
        return string.Equals(Local.Trim(), outra.Local.Trim(), StringComparison.OrdinalIgnoreCase)
               && ConflitaHorarioCom(outra);
    }

    private static void ValidarObrigatoriedade(string titulo, string local, string instrutor)
    {
        if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(local) || string.IsNullOrWhiteSpace(instrutor))
        {
            throw new RegraNegocioException("Título, local e instrutor da atividade são obrigatórios.");
        }
    }
}
