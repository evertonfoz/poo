public class Tecnico
{
    public string? Nome { get; private set; }
    public string? RegistroProfissional { get; private set; }

    public Tecnico(string nome, string registroProfissional)
    {
        AtualizarNome(nome);
        AtualizarRegistroProfissional(registroProfissional);
    }

    public void AtualizarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("O nome não pode ser nulo ou vazio.", nameof(nome));
        }
        Nome = nome;
    }

    public void AtualizarRegistroProfissional(string registroProfissional)
    {
        if (string.IsNullOrWhiteSpace(registroProfissional))
        {
            throw new ArgumentException("O registro profissional não pode ser nulo ou vazio.", nameof(registroProfissional));
        }
        RegistroProfissional = registroProfissional;
    }
}