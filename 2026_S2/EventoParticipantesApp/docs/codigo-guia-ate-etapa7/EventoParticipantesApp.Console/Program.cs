using EventoParticipantes.Negocio.Contratos;
using EventoParticipantes.Negocio.Modelo;
using EventoParticipantes.Negocio.Servicos;
using EventoParticipantes.Persistencia.Memoria;

// 1) Dados "já salvos": Bruno está inscrito na palestra
var shiderlene = new Organizador("Shiderlene Souza",
  "shiderlene@eventos.com");
var bruno = new Participante("Bruno Lima", "bruno@eventos.com");
var palestra = new Evento("Palestra de POO", shiderlene,
  capacidade: 10);
palestra.Inscrever(bruno);
IRepositorioSistema repositorio = new RepositorioMemoria();
repositorio.Salvar(new EstadoSistema(
  [shiderlene], [bruno], [palestra]));

var sistema = new Gerenciador(repositorio);

// 2) Mesmo e-mail da organizadora: permitido (outro cadastro)
sistema.CriarParticipante("Shiderlene Souza",
  "shiderlene@eventos.com");

var idCarla = sistema.CriarParticipante("Carla Dias",
  "carla@eventos.com");
sistema.AlterarParticipante(idCarla, nome: "Carla D. Lima");

try {
  sistema.CriarParticipante("Outra Carla", "CARLA@eventos.com");
}
catch (InvalidOperationException ex) {
  Console.WriteLine($"Recusado: {ex.Message}");
}

try {
  sistema.RemoverParticipante(bruno.ParticipanteId);
}
catch (InvalidOperationException ex) {
  Console.WriteLine($"Recusado: {ex.Message}");
}

foreach (var p in sistema.ListarParticipantes())
  Console.WriteLine($"- {p.Nome} <{p.Email}>");

sistema.RemoverParticipante(idCarla);
var salvo = repositorio.Carregar();
Console.WriteLine($"No repositório: {salvo.Participantes.Count}"
  + " participante(s)");
