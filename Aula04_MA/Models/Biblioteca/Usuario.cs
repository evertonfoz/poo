using System.Text.RegularExpressions;

public record Usuario
{
    private readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    private readonly Regex CpfRegex = new(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$");
    private readonly Regex TelefoneRegex = new(@"^\(\d{2}\) \d{4,5}-\d{4}$");
    private readonly string[] PalavrasIndevidas = new[] { "bobo", "chato", "zoiudo", "bigodudo" };
    
    public string Nome { get; init; }
    public string Email { get; init; }
    public string CPF { get; init; }
    public string Telefone { get; init; }   

    public Usuario(string nome, string email, string cpf, string telefone)
    {
        Nome = string.IsNullOrWhiteSpace(nome) ? throw new ArgumentException("O nome não pode ser vazio.") : nome;
        ValidarNome(Nome);
        
        if (!EmailRegex.IsMatch(email))
        {
            throw new ArgumentException("Email inválido.");
        }
        Email = email; // Regular Expression - RegEx - Expressão Regular
        if (!CpfRegex.IsMatch(cpf))
        {
            throw new ArgumentException("CPF inválido.");
        }
        CPF = cpf;
        if (!TelefoneRegex.IsMatch(telefone))
        {
            throw new ArgumentException("Telefone inválido.");
        }
        Telefone = telefone;
    }

    private void ValidarNome(string nome)
    {
        foreach (var palavra in PalavrasIndevidas)
        {
            if (nome.Contains(palavra, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException($"O nome não pode conter a palavra '{palavra}'.");
            }
        }
    }
}