namespace Atividade02_PersonagensJogo.Modelo;

/// <summary>
/// Especialização de Personagem que ataca à distância e possui mecânica de disparo crítico.
/// </summary>
public class Arqueiro : Personagem
{
    public int BonusCritico { get; private set; }

    public Arqueiro(string nome, int pontosVida, int forcaAtaque, int bonusCritico)
        : base(nome, pontosVida, forcaAtaque)
    {
        DefinirBonusCritico(bonusCritico);
    }

    private void DefinirBonusCritico(int bonusCritico)
    {
        if (bonusCritico <= 0)
            throw new ArgumentOutOfRangeException(nameof(bonusCritico), "O bônus de dano crítico deve ser positivo (maior que zero).");

        BonusCritico = bonusCritico;
    }

    public override void ExibirStatus()
    {
        Console.WriteLine($"[{GetType().Name}] {Nome} -> Vida: {PontosVida} | Força: {ForcaAtaque} | Bônus Crítico: +{BonusCritico}");
    }

    /// <summary>
    /// Sobrescreve o método de ataque padrão herdado da classe base.
    /// Reaproveita a implementação de base.Atacar() como ataque normal.
    /// </summary>
    public override int Atacar()
    {
        return base.Atacar();
    }

    /// <summary>
    /// Sobrecarga (overload) do método Atacar que recebe o parâmetro de controle de ataque crítico.
    /// Se true, soma o bônus crítico ao ataque normal. Se false, desfere o ataque normal.
    /// </summary>
    /// <param name="ataqueCritico">Indica se o ataque será executado com precisão crítica.</param>
    /// <returns>Dano calculado com ou sem crítico.</returns>
    public int Atacar(bool ataqueCritico)
    {
        if (ataqueCritico)
        {
            int dano = Atacar() + BonusCritico;
            Console.WriteLine($"  [Tiro Certeiro!] Acerto Crítico de {Nome}! Dano com bônus: {dano}");
            return dano;
        }

        Console.WriteLine($"  [Tiro Comum] Disparo normal de {Nome}.");
        return Atacar();
    }
}
