using System;

class Program
{
    static void Main()
    {
        // Nota de uma prova (ponto flutuante com precisão razoável)
        float notaProva = 8.75f;

        // Quantidade de alunos (número inteiro)
        int quantidadeAlunos = 30;

        // Nome de um curso (texto)
        string nomeCurso = "Ciência da Computação";

        // Status de pagamento (booleano: pago ou não pago)
        bool pagamentoEfetuado = true;

        // Dígito verificador (único caractere)
        char digitoVerificador = '7';

        // Exibindo no console
        Console.WriteLine($"Nota da prova: {notaProva}");
        Console.WriteLine($"Quantidade de alunos: {quantidadeAlunos}");
        Console.WriteLine($"Nome do curso: {nomeCurso}");
        Console.WriteLine($"Pagamento efetuado: {pagamentoEfetuado}");
        Console.WriteLine($"Dígito verificador: {digitoVerificador}");
    }
}
