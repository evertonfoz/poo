using EventoParticipantes.Negocio.Modelo;
using EventoParticipantes.Negocio.Validacao;

namespace EventoParticipantes.Negocio.Servico;

public sealed partial class Gerenciador
{
    public Guid CriarParticipante(string nome, string email)
    {
        var participante = new Participante(nome, email);
        if (_participantes.Any(x => MesmoEmail(x.Email, participante.Email)))
            throw new InvalidOperationException("E-mail de participante já cadastrado.");
        _participantes.Add(participante);
        Gravar();
        return participante.ParticipanteId;
    }

    public IReadOnlyCollection<Participante> ListarParticipantes() => _participantes.AsReadOnly();

    public Participante ObterParticipante(Guid id) => BuscarParticipante(id);

    public void AlterarParticipante(Guid id, string? nome = null, string? email = null)
    {
        var item = BuscarParticipante(id);
        if (email is not null)
        {
            var novoEmail = Validador.Email(email);
            if (_participantes.Any(x => x.ParticipanteId != id
                                     && MesmoEmail(x.Email, novoEmail)))
                throw new InvalidOperationException("E-mail de participante já cadastrado.");
        }
        item.Alterar(nome, email);
        Gravar();
    }

    public void RemoverParticipante(Guid id)
    {
        var item = BuscarParticipante(id);
        if (_eventos.Any(x => x.ParticipanteIds.Contains(id)))
            throw new InvalidOperationException("Participante inscrito em evento.");
        _participantes.Remove(item);
        Gravar();
    }

    private Participante BuscarParticipante(Guid id) =>
        _participantes.FirstOrDefault(x => x.ParticipanteId == id)
            ?? throw new KeyNotFoundException("Participante não encontrado.");
}
