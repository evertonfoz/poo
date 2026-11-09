namespace Aula009Heranca.Modelo;
public class Notificacao
{
    public string Mensagem { get; set; }

    public Notificacao(string mensagem)
    {
        Mensagem = string.IsNullOrWhiteSpace(mensagem) ? 
                throw new ArgumentException("A mensagem não pode ser nula ou vazia.") 
                : mensagem;
    }

    public void Exibir()
    {
        Console.WriteLine($"{Mensagem}");
    }
}