namespace Aula02_OO.Modelo
{
    public class Usuario
    {
        public string? Nome { get; set; }
        public int Idade { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public bool Ativo { get; set; }
        public char InicialNome
        {
            get
            {
                if (!string.IsNullOrEmpty(Nome))
                {
                    return Nome[0];
                }
                return '\0'; // Retorna um caractere nulo se o nome estiver vazio
            }
        }

        public double CalcularIMC()
        {
            if (Altura > 0)
            {
                return Peso / (Altura * Altura);
            }
            return 0; // Retorna 0 se a altura for inválida
        }
    }
}