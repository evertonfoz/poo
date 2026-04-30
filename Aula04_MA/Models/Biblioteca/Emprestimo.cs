public class Emprestimo
{
    public Livro Livro { get; init; }
    public Usuario Usuario { get; init; }
    public DateTime DataEmprestimo { get; init; }
    public DateTime? DataLimiteParaDevolucao { get; private set; }

    public Emprestimo(Livro livro, Usuario usuario, DateTime dataEmprestimo, DateTime? dataLimiteParaDevolucao = null)
    {
        Livro = livro;
        Usuario = usuario;
        DataEmprestimo = dataEmprestimo;
        DataLimiteParaDevolucao = dataLimiteParaDevolucao;
    }

    public void RegistrarDevolucao()
    {
        DataLimiteParaDevolucao = DateTime.Now;
    }
}