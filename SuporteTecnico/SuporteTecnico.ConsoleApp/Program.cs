using SuporteTecnico.Domain.Entities;

var suporte = new ChamadoSuporte(
    protocolo: "12345",
    nomeSolicitante: "Joao Silva",
    descricaoProblema: "O computador nao liga.",
    prioridade: SuporteTecnico.Domain.Enums.PrioridadeChamadoEnum.Alta,
    dataAbertura: DateTime.Now);

var infraestrutura = new ChamadoInfraestrutura(
    protocolo: "54321",
    nomeSolicitante: "Maria Oliveira",
    descricaoProblema: "O servidor esta com problemas de desempenho.",
    prioridade: SuporteTecnico.Domain.Enums.PrioridadeChamadoEnum.Critica,
    dataAbertura: DateTime.Now,
    localProblema: "Data Center",
    equipamentoAfetado: "Servidor Principal",
    equipamentoCritico: true);

Console.WriteLine("Resumo do chamado de suporte:");
Console.WriteLine(suporte.GerarResumo());
Console.WriteLine($"Prazo de atendimento: {suporte.CalcularPrazoAtendimentoHoras()} horas\n");

Console.WriteLine("Resumo do chamado de infraestrutura:");
Console.WriteLine(infraestrutura.GerarResumo());
Console.WriteLine($"Prazo de atendimento: {infraestrutura.CalcularPrazoAtendimentoHoras()} horas\n");