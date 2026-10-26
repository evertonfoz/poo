namespace EventoParticipantes.Negocio.Modelo;

public sealed class Participante : Pessoa
{
    public Guid ParticipanteId { get; }

    public Participante(string nome, string email, Guid? participanteId = null)
        : base(nome, email)
    {
        ParticipanteId = participanteId ?? Guid.NewGuid();
    }
}
