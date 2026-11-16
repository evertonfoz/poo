namespace Atividade02_PersonagensJogo.Modelo;

/// <summary>
/// Classe concreta base que representa um personagem genérico no jogo.
/// Fornece atributos essenciais e comportamento padrão de ataque virtual.
/// </summary>
public class Personagem
{
    public string Nome { get; private set; } = string.Empty;
    public int PontosVida { get; private set; }
    public int ForcaAtaque { get; private set; }

    public Personagem(string nome, int pontosVida, int forcaAtaque)
    {
        DefinirNome(nome);
        DefinirPontosVidaIniciais(pontosVida);
        DefinirForcaAtaque(forcaAtaque);
    }

    private void DefinirNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do personagem é obrigatório e não pode ser vazio.", nameof(nome));

        Nome = nome.Trim();
    }

    private void DefinirPontosVidaIniciais(int pontosVida)
    {
        if (pontosVida <= 0)
            throw new ArgumentOutOfRangeException(nameof(pontosVida), "Os pontos de vida iniciais devem ser positivos (maiores que zero).");

        PontosVida = pontosVida;
    }

    private void DefinirForcaAtaque(int forcaAtaque)
    {
        if (forcaAtaque <= 0)
            throw new ArgumentOutOfRangeException(nameof(forcaAtaque), "A força de ataque deve ser positiva (maior que zero).");

        ForcaAtaque = forcaAtaque;
    }

    /// <summary>
    /// Exibe o status atual do personagem.
    /// </summary>
    public virtual void ExibirStatus()
    {
        Console.WriteLine($"[{GetType().Name}] {Nome} -> Vida: {PontosVida} | Força: {ForcaAtaque}");
    }

    /// <summary>
    /// Executa um ataque padrão.
    /// Declarado como virtual para permitir especializações (sobrescrita) pelas subclasses.
    /// </summary>
    /// <returns>Valor do dano desferido.</returns>
    public virtual int Atacar()
    {
        return ForcaAtaque;
    }

    /// <summary>
    /// Aplica dano ao personagem, reduzindo seus pontos de vida sem permitir valores negativos.
    /// </summary>
    /// <param name="dano">Valor do dano recebido.</param>
    public void ReceberDano(int dano)
    {
        if (dano < 0)
            throw new ArgumentOutOfRangeException(nameof(dano), "O dano recebido não pode ser negativo.");

        PontosVida = Math.Max(0, PontosVida - dano);
        Console.WriteLine($"-> {Nome} recebeu {dano} de dano! Pontos de Vida restantes: {PontosVida}");
    }
}
