namespace EventoParticipantes.Negocio.Contratos;

public interface IRepositorioSistema
{
    EstadoSistema Carregar();
    void Salvar(EstadoSistema estado);
}
