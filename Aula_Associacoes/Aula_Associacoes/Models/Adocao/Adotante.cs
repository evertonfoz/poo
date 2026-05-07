public class Adotante
{
    public string Nome { get; private set; }
    public string CPF { get; init; }

    public Adotante(string nome, string cpf)
    {
        Nome = string.IsNullOrWhiteSpace(nome) ? throw new ArgumentException("O nome do adotante é obrigatório.", nameof(nome)) : nome;

       if (!DocumentosHelper.CpfValido(cpf))
            throw new ArgumentException("O CPF informado é inválido.", nameof(cpf));
        CPF = cpf;
    }
  }