namespace  EventoComInscricao.Modelo;

public class Participante {
    public string Nome {get; private set;}
    public string Email {get; private set;}
    public Participante(string nome, string email)
    {
        AtualizarNome(nome);
        RegistrarEmail(email);
    }

    public void AtualizarNome(string nome)
    {
        Nome = string.IsNullOrWhiteSpace(nome) ? 
            throw new ArgumentNullException( $"O argumento { nameof(nome)} não pode ser em branco ou vazio") : nome;
    }
    private void RegistrarEmail(string email)
    {
        Email = string.IsNullOrWhiteSpace(email) ? 
            throw new ArgumentNullException( $"O argumento { nameof(email)} não pode ser em branco ou vazio") : email;
    }

    public override bool Equals(object? obj)
    {
        return obj is Participante participante &&
               Email == participante.Email;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Email);
    }
}