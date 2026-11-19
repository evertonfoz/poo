namespace SistemaGestaoEventosAcademicos.Negocio.Entidades;

/// <summary>
/// Representa o certificado oficial emitido para comprovação de participação em evento acadêmico (RN07, RN25).
/// </summary>
public class Certificado
{
    public int InscricaoId { get; init; }
    public string NomeParticipante { get; init; }
    public string CpfParticipante { get; init; }
    public string NomeEvento { get; init; }
    public int CargaHoraria { get; init; }
    public DateTime DataEmissao { get; init; }
    public string CodigoAutenticacao { get; init; }

    public Certificado(
        int inscricaoId,
        string nomeParticipante,
        string cpfParticipante,
        string nomeEvento,
        int cargaHoraria,
        DateTime? dataEmissao = null,
        string? codigoAutenticacao = null)
    {
        InscricaoId = inscricaoId;
        NomeParticipante = nomeParticipante;
        CpfParticipante = cpfParticipante;
        NomeEvento = nomeEvento;
        CargaHoraria = cargaHoraria;
        DataEmissao = dataEmissao ?? DateTime.Now;
        CodigoAutenticacao = codigoAutenticacao ?? GerarCodigoAutenticacao(inscricaoId);
    }

    private static string GerarCodigoAutenticacao(int inscricaoId)
    {
        return $"CERT-{inscricaoId:D4}-{Guid.NewGuid().ToString("N")[..8].ToUpperInvariant()}";
    }
}
