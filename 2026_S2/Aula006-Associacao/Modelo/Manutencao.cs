public class Manutencao
{
    public string? Descricao { get; private set; }
    public DateTime? DataDaManutencao { get; private set; }
    public Tecnico? TecnicoResponsavel { get; private set; }

    public Manutencao(string descricao, Tecnico tecnicoResponsavel)
    {
        AtualizarDescricao(descricao);
        AtualizarTecnicoResponsavel(tecnicoResponsavel);
    }

    public void AtualizarDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
        {
            throw new ArgumentException("A descrição não pode ser nula ou vazia.", nameof(descricao));
        }
        Descricao = descricao;
    }

    public void AtualizarTecnicoResponsavel(Tecnico tecnicoResponsavel)
    {
        if (tecnicoResponsavel == null)
        {
            throw new ArgumentNullException(nameof(tecnicoResponsavel), "O técnico responsável não pode ser nulo.");
        }
        TecnicoResponsavel = tecnicoResponsavel;
    }

    public void RegistrarDataDaManutencao(DateTime data)
    {
        if (DataDaManutencao != null)
        {
            throw new InvalidOperationException("A data da manutenção já foi registrada e não pode ser alterada.");
        }
        
        if (data > DateTime.Now)
        {
            throw new ArgumentException("A data da manutenção não pode ser futura.", nameof(data));
        }
        DataDaManutencao = data;
    }
}