public record Banco
{
    public string Numero { get; init; }
    public string Nome { get; init; }

    public Banco(String numero, String nome)
    {
        Numero = string.IsNullOrEmpty(numero) ? 
                throw new ArgumentException("Número do banco não pode ser vazio.") : 
                numero;
        Nome = string.IsNullOrEmpty(nome) ? throw new ArgumentException("Nome do banco não pode ser vazio.") : nome;
    }
}

// public class Banco
// {
//     public string Numero { get; private set; }
//     public string Nome { get; private set; }
//     public String Contato { get;  private set; }

//     public Banco(String numero, String nome)
//     {
//         // if (string.IsNullOrEmpty(numero) || string.IsNullOrEmpty(nome))
//         // {
//         //     throw new ArgumentException("Número e nome do banco não podem ser vazios.");
//         // }

//         Numero = string.IsNullOrEmpty(numero) ? 
//                 throw new ArgumentException("Número do banco não pode ser vazio.") : 
//                 numero;
//         Nome = string.IsNullOrEmpty(nome) ? throw new ArgumentException("Nome do banco não pode ser vazio.") : nome;
//     }

// // Regular Expression -> Expressão Regular
//     public void AtualizarContato(string contato)
//     {
//         if (string.IsNullOrEmpty(contato))
//         {
//             throw new ArgumentException("Contato não pode ser vazio.");
//         }
//         if (!contato.StartsWith("(") || !contato.Contains(")"))
//         {
//             throw new ArgumentException("Contato deve estar no formato (XX) XXXX-XXXX.");
//         }
//         Contato = contato;
//     }
// }