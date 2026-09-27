using EventoParticipantes.Modelo;

var listaDeParticipantes = new List<Participante>();

var participante1 = new Participante("Everton", "everton@example.com");
var participante2 = new Participante("Maria", "maria@example.com");

var organizador = new Organizador("João", "joao@example.com");

listaDeParticipantes.Add(participante1);
listaDeParticipantes.Add(participante2);


// evento.AdicionarParticipante(new Participante("Carlos", "carlos@example.com"));

var evento = new Evento(organizador, "Evento de Tecnologia", 1);
var participante = new Participante("Shi", "shi@example.com");

try
{

    evento.AdicionarParticipante(new Participante("Carlos", "carlos@example.com"));
    evento.AdicionarParticipante(participante);

} catch (InvalidOperationException ex)
{
    Console.WriteLine($"Erro: {ex.Message}");
}


//evento.RemoverParticipante(participante);

if (evento.RemoverParticipante("carlos@example.com"))
{
    Console.WriteLine($"Participante com email carlos@example.com removido com sucesso.");
}

Console.WriteLine("Participantes do evento:");
foreach (var p in evento.Participantes)
{
    Console.WriteLine($"Nome: {participante.Nome}, Email: {participante.Email}");
}

// evento.AdicionarParticipante(null);

//evento.Participantes = listaDeParticipantes;