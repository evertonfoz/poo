namespace  EventoComInscricao.Modelo;

public class Evento
{
    private readonly List<Inscricao> _inscricoes = [];
    public string Nome {get; private set;}
    public IReadOnlyCollection<Inscricao> Inscricoes => _inscricoes.AsReadOnly<Inscricao>();

    public Evento (string nome)
    {
        RegistrarNome(nome);
    }

    private void RegistrarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentNullException($"O {nameof(nome)} não pode ser vazio ou em branco");
        Nome = nome;
    }
    public void InscreverParticipante(Participante participante)
    {
        Inscricao inscricao = new Inscricao(participante);
        if (_inscricoes.Any(inscricao => inscricao.Participante.Equals(participante)))
            throw new InvalidOperationException($"O(a) participante {participante.Nome} já está inscrito");
        _inscricoes.Add(inscricao);
    }

    public void ConfirmarInscricao(Participante participante)
    {
        var inscricao = BuscarInscricao(participante) ?? 
            throw new ArgumentNullException(
                $"O participante {participante.Nome} não está inscrito");
                
        inscricao.ConfirmarInscricao();
    }

    private Inscricao? BuscarInscricao(Participante participante)
    {
        return _inscricoes.FirstOrDefault(
            inscricao => inscricao.Participante.Equals(participante));
    }
}