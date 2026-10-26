using EventoParticipantes.Negocio.Modelo;
using EventoParticipantes.Negocio.Validacao;

namespace EventoParticipantes.Negocio.Servico;

public sealed partial class Gerenciador
{
    public Guid CriarOrganizador(string nome, string email)
    {
        var organizador = new Organizador(nome, email);
        if (_organizadores.Any(x => MesmoEmail(x.Email, organizador.Email)))
            throw new InvalidOperationException("E-mail de organizador já cadastrado.");
        _organizadores.Add(organizador);
        Gravar();
        return organizador.OrganizadorId;
    }

    public IReadOnlyCollection<Organizador> ListarOrganizadores() => _organizadores.AsReadOnly();

    public Organizador ObterOrganizador(Guid id) => BuscarOrganizador(id);

    public void AlterarOrganizador(Guid id, string? nome = null, string? email = null)
    {
        var item = BuscarOrganizador(id);
        if (email is not null)
        {
            var novoEmail = Validador.Email(email);
            if (_organizadores.Any(x => x.OrganizadorId != id
                                     && MesmoEmail(x.Email, novoEmail)))
                throw new InvalidOperationException("E-mail de organizador já cadastrado.");
        }
        item.Alterar(nome, email);
        Gravar();
    }

    public void RemoverOrganizador(Guid id)
    {
        var item = BuscarOrganizador(id);
        if (_eventos.Any(x => x.OrganizadorId == id))
            throw new InvalidOperationException("Organizador possui eventos.");
        _organizadores.Remove(item);
        Gravar();
    }

    private Organizador BuscarOrganizador(Guid id) =>
        _organizadores.FirstOrDefault(x => x.OrganizadorId == id)
            ?? throw new KeyNotFoundException("Organizador não encontrado.");
}
