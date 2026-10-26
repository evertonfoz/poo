using EventoParticipantes.Negocio.Contratos;
using EventoParticipantes.Negocio.Modelo;
using EventoParticipantes.Persistencia.Json.Dtos;

namespace EventoParticipantes.Persistencia.Json;

// Converte entre o modelo de domínio e os DTOs gravados no arquivo.
internal static class MapeadorDados
{
    public static DadosDto ParaDto(EstadoSistema estado) => new()
    {
        Organizadores = estado.Organizadores
            .Select(x => ParaDto(x, x.OrganizadorId)).ToList(),
        Participantes = estado.Participantes
            .Select(x => ParaDto(x, x.ParticipanteId)).ToList(),
        Eventos = estado.Eventos
            .Select(x => new EventoDto
            {
                Id = x.EventoId,
                Nome = x.Nome,
                OrganizadorId = x.OrganizadorId,
                Capacidade = x.Capacidade,
                ParticipanteIds = x.ParticipanteIds.ToList()
            }).ToList()
    };

    public static EstadoSistema ParaEstado(DadosDto dados) => new(
        dados.Organizadores.Select(x => new Organizador(x.Nome, x.Email, x.Id)),
        dados.Participantes.Select(x => new Participante(x.Nome, x.Email, x.Id)),
        dados.Eventos.Select(x => new Evento(x.Nome, x.OrganizadorId, x.Capacidade,
                                             x.Id, x.ParticipanteIds)));

    private static PessoaDto ParaDto(Pessoa pessoa, Guid id) => new()
    {
        Id = id,
        Nome = pessoa.Nome,
        Email = pessoa.Email
    };
}
