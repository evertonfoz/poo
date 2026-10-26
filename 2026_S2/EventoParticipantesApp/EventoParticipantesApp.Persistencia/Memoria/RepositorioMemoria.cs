using EventoParticipantes.Negocio.Contratos;

namespace EventoParticipantes.Persistencia.Memoria;

public sealed class RepositorioMemoria : IRepositorioSistema
{
    private EstadoSistema _estado = new([], [], []);

    public EstadoSistema Carregar() => _estado;

    public void Salvar(EstadoSistema estado) => _estado = estado;
}
