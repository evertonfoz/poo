namespace EventoParticipantes.Modelo;

public class Organizador(string nome, string email)
{
    public string Nome { get; private set; } = nome;
    public string Email { get; private set; } = email;
}