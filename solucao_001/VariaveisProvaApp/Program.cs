using System;

class Program
{
    static void Main()
    {
        // Nota de uma prova (decimal)
        decimal notaProva = 8.7m;

        // Quantidade de alunos em uma sala (int)
        int quantidadeAlunos = 30;

        // Nome de um curso (string)
        string nomeCurso = "Ciência da Computação";

        // Status de pagamento (bool)
        bool pagamentoRealizado = true;

        // Dígito verificador de um documento (char)
        char digitoVerificador = 'X';

        Console.WriteLine($"Nota da prova: {notaProva}");
        Console.WriteLine($"Quantidade de alunos: {quantidadeAlunos}");
        Console.WriteLine($"Nome do curso: {nomeCurso}");
        Console.WriteLine($"Pagamento realizado: {pagamentoRealizado}");
        Console.WriteLine($"Dígito verificador: {digitoVerificador}");
    }
}
