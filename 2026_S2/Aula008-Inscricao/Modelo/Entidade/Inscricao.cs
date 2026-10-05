namespace  EventoComInscricao.Modelo;

public class Inscricao
{
    public Participante Participante {get; private set;}
    public StatusInscricao Status {get; private set;}

    public Inscricao(Participante participante)
    {
        Participante = participante;
        Status = StatusInscricao.Pendente;
    }

    public void ConfirmarInscricao()
    {        
        if (Status == StatusInscricao.Confirmada)
            throw new InvalidOperationException($"A inscrição do participante { Participante.Nome} já está confirmada");

        if (Status == StatusInscricao.Cancelada) 
            throw new InvalidOperationException($"A inscrição do participante {Participante.Nome} já está cancelada");

        Status = StatusInscricao.Confirmada;
    }

    public override string ToString()
    {
        return $"{Participante.Nome} -> {Status}";
    }
}