public record DocumentoAdocao
{
    public DateTime DataDeEmissao { get; }

    public DocumentoAdocao()
    {
        DataDeEmissao = DateTime.Now.Date;
    }

    // public DocumentoAdocao(DateTime dataDeEmissao)
    // {
    //     if (dataDeEmissao.Date != DateTime.Now.Date)
    //     {
    //         throw new ArgumentException("A data de emissão deve ser a data atual.");
    //     }
    //     DataDeEmissao = dataDeEmissao;
    // }
}