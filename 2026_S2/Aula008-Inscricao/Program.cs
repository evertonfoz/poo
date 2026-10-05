using EventoComInscricao.Modelo;

var participante1 = new Participante("Everton", "evertoncoimbra@gmail.com");
var participante2 = new Participante("Gabriel", "gabriel@gmail.com");

var evento = new Evento("Feira de Tecnologia");
evento.InscreverParticipante(participante1);
// evento.InscreverParticipante(participante2);

foreach (var inscricao in evento.Inscricoes)
{
    Console.WriteLine(inscricao);
}

evento.ConfirmarInscricao(participante1);
evento.ConfirmarInscricao(participante1);
// evento.ConfirmarInscricao(participante2);