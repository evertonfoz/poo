using EventoParticipantes.Negocio.Contratos;
using EventoParticipantes.Negocio.Servicos;
using EventoParticipantes.Persistencia.Memoria;

IRepositorioSistema repositorio = new RepositorioMemoria();
var sistema = new Gerenciador(repositorio);

// 1) Cadastro: tudo pelo Gerenciador, só com Guids
var idShiderlene = sistema.CriarOrganizador("Shiderlene Souza",
  "shiderlene@eventos.com");
var idBruno = sistema.CriarParticipante("Bruno Lima",
  "bruno@eventos.com");
var idCarla = sistema.CriarParticipante("Carla Dias",
  "carla@eventos.com");
var idPalestra = sistema.CriarEvento("Palestra de POO",
  idShiderlene, capacidade: 1);

// 2) Inscrições: duplicidade e lotação
sistema.Inscrever(idPalestra, idBruno);
Tentar(() => sistema.Inscrever(idPalestra, idBruno));
Tentar(() => sistema.Inscrever(idPalestra, idCarla));

// 3) Exclusões protegidas
Tentar(() => sistema.RemoverParticipante(idBruno));
Tentar(() => sistema.RemoverOrganizador(idShiderlene));
Tentar(() => sistema.RemoverEvento(idPalestra));

// 4) Alteração: a regra de capacidade continua no Evento
sistema.AlterarEvento(idPalestra, nome: "POO na prática",
  capacidade: 2);
sistema.Inscrever(idPalestra, idCarla);
Tentar(() => sistema.AlterarEvento(idPalestra, capacidade: 1));

var palestra = sistema.ObterEvento(idPalestra);
Console.WriteLine($"{palestra.Nome}, de {palestra.Organizador.Nome}: "
  + $"{palestra.Participantes.Count}/{palestra.Capacidade}");
foreach (var p in palestra.Participantes)
  Console.WriteLine($"- {p.Nome}");

// 5) Cancelamento: depois dele, as remoções são permitidas
sistema.CancelarInscricao(idPalestra, idBruno);
sistema.CancelarInscricao(idPalestra, idCarla);
sistema.RemoverEvento(idPalestra);
sistema.RemoverOrganizador(idShiderlene);

var salvo = repositorio.Carregar();
Console.WriteLine($"No repositório: {salvo.Eventos.Count} evento(s), "
  + $"{salvo.Organizadores.Count} organizador(es)");

void Tentar(Action acao) {
  try {
    acao();
  }
  catch (InvalidOperationException ex) {
    Console.WriteLine($"Recusado: {ex.Message}");
  }
}
