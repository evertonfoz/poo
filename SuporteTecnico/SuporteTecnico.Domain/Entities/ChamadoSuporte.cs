using SuporteTecnico.Domain.Enums;
using SuporteTecnico.Domain.Exceptions;
using SuporteTecnico.Domain.Validations;

namespace SuporteTecnico.Domain.Entities;

public class ChamadoSuporte
{
    public string Protocolo { get; }
    public string NomeSolicitante { get; }
    public string DescricaoProblema { get; }
    public PrioridadeChamadoEnum Prioridade { get; }
    public StatusChamadoEnum Status { get; private set; }
    public DateTime DataAbertura { get; }

    public ChamadoSuporte(
        string protocolo,
        string nomeSolicitante,
        string descricaoProblema,
        PrioridadeChamadoEnum prioridade,
        DateTime dataAbertura)
    {
        Protocolo = ValidadorDominio.ValidarTextoObrigatorio(
            protocolo,
            "O protocolo do chamado é obrigatório.");

        NomeSolicitante = ValidadorDominio.ValidarTextoObrigatorio(
            nomeSolicitante,
            "O nome do solicitante é obrigatório.");

        DescricaoProblema = ValidadorDominio.ValidarTextoObrigatorio(
            descricaoProblema,
            "A descrição do problema é obrigatória.");

        if (!Enum.IsDefined(typeof(PrioridadeChamadoEnum), prioridade))
            throw new DomainException("A prioridade informada é inválida.");

        if (dataAbertura > DateTime.Now)
            throw new DomainException("A data de abertura não pode estar no futuro.");

        Prioridade = prioridade;
        DataAbertura = dataAbertura;
        Status = StatusChamadoEnum.Aberto;
    }

#region Métodos Virtuais para Resumo e Cálculo de Prazo
    public virtual string GerarResumo()
    {
        return
            $"Protocolo: {Protocolo}\n" +
            $"Solicitante: {NomeSolicitante}\n" +
            $"Prioridade: {Prioridade}\n" +
            $"Status: {Status}\n" +
            $"Data de abertura: {DataAbertura:dd/MM/yyyy HH:mm}\n" +
            $"Descrição: {DescricaoProblema}";
    }

    public virtual decimal CalcularPrazoAtendimentoHoras()
    {
        return Prioridade switch
        {
            PrioridadeChamadoEnum.Baixa => 72,
            PrioridadeChamadoEnum.Media => 48,
            PrioridadeChamadoEnum.Alta => 24,
            PrioridadeChamadoEnum.Critica => 8,
            _ => throw new DomainException("Prioridade inválida para cálculo de prazo.")
        };
    }

#endregion
    
  #region Métodos para Alteração de Status  
    public void IniciarAtendimento()
    {
        if (Status == StatusChamadoEnum.Encerrado)
            throw new DomainException("Não é possível iniciar atendimento de um chamado encerrado.");

        Status = StatusChamadoEnum.EmAtendimento;
    }

    public void Encerrar()
    {
        if (Status == StatusChamadoEnum.Encerrado)
            throw new DomainException("O chamado já está encerrado.");

        Status = StatusChamadoEnum.Encerrado;
    }
#endregion
}