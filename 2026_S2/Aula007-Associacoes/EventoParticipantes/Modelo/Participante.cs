namespace EventoParticipantes.Modelo;

public class Participante(string nome, string email)
{
    public string Nome { get; private set; } = nome;
    public string Email { get; private set; } = email;

    public override bool Equals(object? participante)
    {
        if (participante is not Participante p)
        {
            return false;
        }
        
        return this.Email == p.Email;
    }

    public override int GetHashCode()
    {
        return this.Email.GetHashCode();
    }
}