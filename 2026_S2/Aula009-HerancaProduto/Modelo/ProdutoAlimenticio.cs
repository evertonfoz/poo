namespace Aula009_HerancaProduto.Modelo;

public class ProdutoAlimenticio : Produto
{
    public DateTime DataValidade { get; private set; }
    public bool PrecisaRefrigeracao { get; private set; }

    public ProdutoAlimenticio(
        string codigo,
        string descricao,
        decimal precoBase,
        DateTime dataValidade,
        bool precisaRefrigeracao)
        : base(codigo, descricao, precoBase)
    {
        AtualizarDataValidade(dataValidade);
        PrecisaRefrigeracao = precisaRefrigeracao;
    }

    // Métodos semânticos de negócio
    public void AtualizarDataValidade(DateTime novaValidade)
    {
        if (novaValidade == default)
            throw new ArgumentException("A data de validade deve ser informada.", nameof(novaValidade));

        DataValidade = novaValidade;
    }

    public void AlterarExigenciaRefrigeracao(bool precisaRefrigeracao)
    {
        PrecisaRefrigeracao = precisaRefrigeracao;
    }

    public bool EstaVencido()
    {
        return DateTime.Today > DataValidade.Date;
    }
}
