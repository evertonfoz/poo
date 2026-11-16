namespace Atividade02_PersonagensJogo.Modelo;

/// <summary>
/// Especialização de Personagem que luta corpo a corpo com bônus de espada.
/// </summary>
public class Guerreiro : Personagem
{
    public int BonusEspada { get; private set; }

    public Guerreiro(string nome, int pontosVida, int forcaAtaque, int bonusEspada)
        : base(nome, pontosVida, forcaAtaque)
    {
        DefinirBonusEspada(bonusEspada);
    }

    private void DefinirBonusEspada(int bonusEspada)
    {
        if (bonusEspada < 0)
            throw new ArgumentOutOfRangeException(nameof(bonusEspada), "O bônus de espada não pode ser negativo.");

        BonusEspada = bonusEspada;
    }

    public override void ExibirStatus()
    {
        Console.WriteLine($"[{GetType().Name}] {Nome} -> Vida: {PontosVida} | Força: {ForcaAtaque} | Bônus Espada: +{BonusEspada}");
    }

    /// <summary>
    /// Sobrescreve o ataque padrão somando o bônus da espada à força de ataque.
    /// Mantém-se virtual (não sealed) para permitir que subclasses (como Paladino) possam especializá-lo.
    /// </summary>
    public override int Atacar()
    {
        return base.Atacar() + BonusEspada;
    }
}
