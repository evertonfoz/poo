namespace EventoParticipantes.Modelo;

public class Evento(Organizador organizador, string nome, int quantidadeMaximaDeParticipantes)
{
    public string Nome { get; private set; } = nome;
    public Organizador Organizador { get; private set; } = organizador;
    public int QuantidadeMaximaDeParticipantes { get; private set; } = quantidadeMaximaDeParticipantes;

    private readonly List<Participante> _participantes = [];
    public IReadOnlyCollection<Participante> Participantes => _participantes.AsReadOnly();

    public void AdicionarParticipante(Participante participante)
    {
        // ArgumentNullException.ThrowIfNull(participante, nameof(participante));

        if (_participantes.Count >= QuantidadeMaximaDeParticipantes)
        {
            throw new InvalidOperationException($"O evento {Nome} atingiu o limite máximo de participantes.");
        }

        if (_participantes.Contains(participante)) // LINQ => Language Integrated Query
        {
            throw new InvalidOperationException($"O participante com o email {participante.Email} já está registrado no evento.");
        }

        // foreach (var p in _participantes)
        // {
        //     if (p.Email == participante.Email)
        //     {
        //         throw new InvalidOperationException($"O participante com o email {participante.Email} já está registrado no evento.");
        //     }
        // }

        // for (int i = 0; i < _participantes.Count; i++)
        // {
        //     if (_participantes[i].Email == participante.Email)
        //     {
        //         throw new InvalidOperationException($"O participante com o email {participante.Email} já está registrado no evento.");
        //     }
        // }

        _participantes.Add(participante);
    }
    public bool RemoverParticipante(Participante participante)
    {
        if (!_participantes.Contains(participante))
        {
            throw new InvalidOperationException($"O participante com o email {participante.Email} não está registrado no evento.");
        }

        _participantes.Remove(participante);
        return true;
    }

    public bool RemoverParticipante(string email)
    {
        var participante = _participantes.FirstOrDefault(p => p.Email == email);
        if (participante == null)
        {
            throw new InvalidOperationException($"O participante com o email {email} não está registrado no evento.");
        }

        _participantes.Remove(participante);
        return true;
    }
}