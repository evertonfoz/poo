namespace Atividade02_PersonagensJogo.Modelo;

/// <summary>
/// Especialização de Personagem que conjura magias consumindo mana.
/// </summary>
public class Mago : Personagem
{
    public int ManaAtual { get; private set; }
    public int CustoManaAtaque { get; private set; }
    public int BonusMagico { get; private set; }

    public Mago(
        string nome,
        int pontosVida,
        int forcaAtaque,
        int manaAtual,
        int custoManaAtaque,
        int bonusMagico)
        : base(nome, pontosVida, forcaAtaque)
    {
        DefinirCustoManaAtaque(custoManaAtaque);
        DefinirManaAtual(manaAtual);
        DefinirBonusMagico(bonusMagico);
    }

    private void DefinirManaAtual(int mana)
    {
        if (mana < 0)
            throw new ArgumentOutOfRangeException(nameof(mana), "A mana atual não pode ser negativa.");

        ManaAtual = mana;
    }

    private void DefinirCustoManaAtaque(int custo)
    {
        if (custo <= 0)
            throw new ArgumentOutOfRangeException(nameof(custo), "O custo de mana por ataque deve ser positivo (maior que zero).");

        CustoManaAtaque = custo;
    }

    private void DefinirBonusMagico(int bonus)
    {
        if (bonus < 0)
            throw new ArgumentOutOfRangeException(nameof(bonus), "O bônus mágico não pode ser negativo.");

        BonusMagico = bonus;
    }

    public override void ExibirStatus()
    {
        Console.WriteLine($"[{GetType().Name}] {Nome} -> Vida: {PontosVida} | Força: {ForcaAtaque} | Mana: {ManaAtual} (Custo: {CustoManaAtaque}) | Bônus Mágico: +{BonusMagico}");
    }

    /// <summary>
    /// Sobrescreve o ataque: se tiver mana suficiente, consome e adiciona bônus mágico.
    /// Caso contrário, realiza o ataque físico padrão sem consumir mana.
    /// </summary>
    public override int Atacar()
    {
        if (ManaAtual >= CustoManaAtaque)
        {
            ManaAtual -= CustoManaAtaque;
            int dano = base.Atacar() + BonusMagico;
            Console.WriteLine($"  [Magia Conjurada!] {Nome} gastou {CustoManaAtaque} de mana (restam {ManaAtual}). Dano mágico: {dano}");
            return dano;
        }

        Console.WriteLine($"  [Mana Insuficiente!] {Nome} não tem mana ({ManaAtual}/{CustoManaAtaque}). Realizou ataque físico padrão.");
        return base.Atacar();
    }
}
