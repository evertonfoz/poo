using Aula009Heranca.Modelo;

// Notificacao notificacao = new("Olá, esta é uma notificação!");
// notificacao.Exibir();
// Console.WriteLine(notificacao.Mensagem);

// Notificacao notificacao = new Notificacao();

NotificacaoSMS notificacaoSMS = new("Olá, esta é uma notificação via SMS!");

notificacaoSMS.Enviar();