using EventoParticipantes.Negocio.Contratos;
using EventoParticipantes.Negocio.Modelo;

namespace EventoParticipantes.Negocio.Servico;

// Núcleo do Gerenciador: estado, persistência e integridade.
// As operações de cada entidade ficam nos arquivos Gerenciador.*.cs.
public sealed partial class Gerenciador
{
    private readonly IRepositorioSistema _repositorio;
    private readonly List<Organizador> _organizadores;
    private readonly List<Participante> _participantes;
    private readonly List<Evento> _eventos;

    public Gerenciador(IRepositorioSistema repositorio)
    {
        _repositorio = repositorio
            ?? throw new ArgumentNullException(nameof(repositorio));
        var estado = _repositorio.Carregar();
        _organizadores = estado.Organizadores.ToList();
        _participantes = estado.Participantes.ToList();
        _eventos = estado.Eventos.ToList();
        ValidarIntegridade();
    }

    private void Gravar() => _repositorio.Salvar(new EstadoSistema(_organizadores, _participantes, _eventos));

    private static bool MesmoEmail(string a, string b) => string.Equals(a, b, StringComparison.OrdinalIgnoreCase);

    private void ValidarIntegridade()
    {
        ValidarIdsUnicos(_organizadores.Select(x => x.OrganizadorId), "Organizador");
        ValidarIdsUnicos(_participantes.Select(x => x.ParticipanteId), "Participante");
        ValidarIdsUnicos(_eventos.Select(x => x.EventoId), "Evento");

        ValidarEmailsUnicos(_organizadores.Select(x => x.Email), "organizadores");
        ValidarEmailsUnicos(_participantes.Select(x => x.Email), "participantes");

        foreach (var evento in _eventos)
        {
            if (!_organizadores.Any(x => x.OrganizadorId == evento.OrganizadorId))
                throw new InvalidOperationException($"Evento '{evento.Nome}' referencia organizador inexistente.");

            if (evento.ParticipanteIds.Distinct().Count() != evento.ParticipanteIds.Count)
                throw new InvalidOperationException($"Evento '{evento.Nome}' possui inscrições duplicadas.");

            if (evento.ParticipanteIds.Count > evento.Capacidade)
                throw new InvalidOperationException($"Evento '{evento.Nome}' excede a capacidade.");

            foreach (var participanteId in evento.ParticipanteIds)
                if (!_participantes.Any(x => x.ParticipanteId == participanteId))
                    throw new InvalidOperationException($"Evento '{evento.Nome}' referencia participante inexistente.");
        }
    }

    private static void ValidarIdsUnicos(IEnumerable<Guid> ids, string entidade)
    {
        var lista = ids.ToList();
        if (lista.Distinct().Count() != lista.Count)
            throw new InvalidOperationException($"{entidade}: Guid duplicado.");
    }

    private static void ValidarEmailsUnicos(IEnumerable<string> emails, string grupo)
    {
        var vistos = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var email in emails)
            if (!vistos.Add(email))
                throw new InvalidOperationException($"E-mail duplicado entre {grupo}.");
    }
}
