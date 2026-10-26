namespace EventoParticipantes.Negocio.Modelo;

public sealed class Organizador : Pessoa
{
    public Guid OrganizadorId { get; }

    public Organizador(string nome, string email, Guid? organizadorId = null)
        : base(nome, email)
    {
        OrganizadorId = organizadorId ?? Guid.NewGuid();
    }
}
