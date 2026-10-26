using EventoParticipantes.Negocio.Modelo;
using EventoParticipantes.Negocio.Servico;

namespace EventoParticipantes.App.Menus;

internal sealed class MenuEventos : MenuCadastro
{
    public MenuEventos(Gerenciador sistema) : base(sistema) { }

    protected override string Titulo => "Eventos";

    protected override void Cadastrar()
    {
        var id = Sistema.CriarEvento(Entrada.Texto("Nome"),
            Entrada.Id("Id do organizador"), Entrada.Inteiro("Capacidade"));
        Console.WriteLine($"Evento cadastrado. Id: {id}");
    }

    protected override void Listar()
    {
        var eventos = Sistema.ListarEventos();
        if (eventos.Count == 0)
            Console.WriteLine("Nenhum evento cadastrado.");
        foreach (var x in eventos)
            Exibir(x);
    }

    protected override void Buscar()
    {
        var evento = Sistema.ObterEvento(Entrada.Id("Id"));
        Exibir(evento);
        foreach (var participanteId in evento.ParticipanteIds)
        {
            var participante = Sistema.ObterParticipante(participanteId);
            Console.WriteLine($"  - {participante.Nome} ({participante.Email})");
        }
    }

    // O evento só permite alterar a capacidade.
    protected override void Alterar()
    {
        Sistema.AlterarCapacidadeEvento(Entrada.Id("Id"), Entrada.Inteiro("Nova capacidade"));
        Console.WriteLine("Capacidade alterada.");
    }

    protected override void Remover()
    {
        Sistema.RemoverEvento(Entrada.Id("Id"));
        Console.WriteLine("Evento removido.");
    }

    private void Exibir(Evento x)
    {
        var organizador = Sistema.ObterOrganizador(x.OrganizadorId);
        Console.WriteLine($"{x.EventoId} | {x.Nome} | Organizador: {organizador.Nome} | "
                          + $"Inscritos: {x.ParticipanteIds.Count}/{x.Capacidade}");
    }
}
