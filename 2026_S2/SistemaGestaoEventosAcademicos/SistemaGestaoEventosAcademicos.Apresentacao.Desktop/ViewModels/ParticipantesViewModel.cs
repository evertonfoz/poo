using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SistemaGestaoEventosAcademicos.Apresentacao.Desktop.ViewModels;

public record ParticipanteItemDto(
    int Id,
    string Nome,
    string Cpf,
    string Email,
    string Telefone,
    int TotalInscricoes
);

public partial class ParticipantesViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _termoBusca = string.Empty;

    public ObservableCollection<ParticipanteItemDto> Participantes { get; } = new()
    {
        new ParticipanteItemDto(1, "Ana Beatriz Silva", "123.456.789-00", "ana.silva@universidade.edu.br", "(41) 99887-1122", 3),
        new ParticipanteItemDto(2, "Carlos Eduardo Oliveira", "987.654.321-99", "carlos.oliveira@academico.br", "(41) 98765-4321", 1),
        new ParticipanteItemDto(3, "Juliana Mendes Santos", "456.789.123-55", "juliana.mendes@pesquisa.org", "(41) 99123-9988", 2),
        new ParticipanteItemDto(4, "Lucas Gabriel Ferreira", "321.654.987-11", "lucas.ferreira@estudante.com", "(41) 98456-7788", 1)
    };
}
