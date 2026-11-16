namespace Aula009Heranca.Modelo;

public class NotificacaoSMS : Notificacao
{
    public NotificacaoSMS(string mensagem) : base(mensagem)
    {
    }

    public override void Enviar()
    {
        Console.WriteLine($"Enviando SMS: {Mensagem}");
    }
}