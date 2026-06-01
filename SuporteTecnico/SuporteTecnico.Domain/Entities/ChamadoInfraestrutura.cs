using SuporteTecnico.Domain.Enums;
using SuporteTecnico.Domain.Validations;

namespace SuporteTecnico.Domain.Entities;

// Um chamado de infraestrutura e um chamado de suporte.
public class ChamadoInfraestrutura : ChamadoSuporte
{
    public string LocalProblema { get; }
    public string EquipamentoAfetado { get; }
    public bool EquipamentoCritico { get; }

    public ChamadoInfraestrutura(
        string protocolo,
        string nomeSolicitante,
        string descricaoProblema,
        PrioridadeChamadoEnum prioridade,
        DateTime dataAbertura,
        string localProblema,
        string equipamentoAfetado,
        bool equipamentoCritico)
        : base(protocolo, nomeSolicitante, descricaoProblema, prioridade, dataAbertura)
    {
        LocalProblema = ValidadorDominio.ValidarTextoObrigatorio(
            localProblema,
            "O local do problema e obrigatorio.");

        EquipamentoAfetado = ValidadorDominio.ValidarTextoObrigatorio(
            equipamentoAfetado,
            "O equipamento afetado e obrigatorio.");

        EquipamentoCritico = equipamentoCritico;
    }

    public override string GerarResumo()
    {
        return
            $"{base.GerarResumo()}\n" +
            $"Local do problema: {LocalProblema}\n" +
            $"Equipamento afetado: {EquipamentoAfetado}\n" +
            $"Equipamento critico: {(EquipamentoCritico ? "Sim" : "Nao")}";
    }

    public override decimal CalcularPrazoAtendimentoHoras()
    {
        var prazoBase = base.CalcularPrazoAtendimentoHoras();
        return EquipamentoCritico ? prazoBase / 2 : prazoBase;
    }
}
