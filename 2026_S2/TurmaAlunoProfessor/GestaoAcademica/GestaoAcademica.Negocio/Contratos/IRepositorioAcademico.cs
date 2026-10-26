namespace GestaoAcademica.Negocio.Contratos;

public interface IRepositorioAcademico
{
    // Retorna o estado completo. Sem dados gravados, retorna
    // coleções vazias. Dados existentes porém inválidos lançam
    // PersistenciaException (nunca retornam vazio).
    DadosAcademicos Carregar();

    // Grava o estado completo. Lança PersistenciaException
    // se a gravação falhar.
    void Salvar(DadosAcademicos dados);
}
