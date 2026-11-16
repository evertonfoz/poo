namespace Atividade02_PersonagensJogo.Modelo;

/// <summary>
/// Desafio adicional: Especialização de Guerreiro imbuída de poder sagrado.
/// Herda de Guerreiro e acrescenta BonusSagrado, compondo os comportamentos de Personagem e Guerreiro.
/// </summary>
public class Paladino : Guerreiro
{
    public int BonusSagrado { get; private set; }

    public Paladino(string nome, int pontosVida, int forcaAtaque, int bonusEspada, int bonusSagrado)
        : base(nome, pontosVida, forcaAtaque, bonusEspada)
    {
        DefinirBonusSagrado(bonusSagrado);
    }

    private void DefinirBonusSagrado(int bonusSagrado)
    {
        if (bonusSagrado < 0)
            throw new ArgumentOutOfRangeException(nameof(bonusSagrado), "O bônus sagrado não pode ser negativo.");

        BonusSagrado = bonusSagrado;
    }

    public override void ExibirStatus()
    {
        Console.WriteLine($"[{GetType().Name}] {Nome} -> Vida: {PontosVida} | Força: {ForcaAtaque} | Bônus Espada: +{BonusEspada} | Bônus Sagrado: +{BonusSagrado}");
    }

    /// <summary>
    /// Sobrescreve Atacar() utilizando base.Atacar() para obter o dano de Guerreiro (ForcaAtaque + BonusEspada)
    /// e adiciona o BonusSagrado.
    /// </summary>
    public override int Atacar()
    {
        int danoGuerreiro = base.Atacar();
        int danoTotal = danoGuerreiro + BonusSagrado;
        Console.WriteLine($"  [Golpe Sagrado!] {Nome} invocou a luz divina! Dano base + espada: {danoGuerreiro} | + Sagrado: {BonusSagrado} = Total: {danoTotal}");
        return danoTotal;
    }
}
