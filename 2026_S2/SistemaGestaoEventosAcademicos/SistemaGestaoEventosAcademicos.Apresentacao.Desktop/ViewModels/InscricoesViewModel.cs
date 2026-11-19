using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SistemaGestaoEventosAcademicos.Apresentacao.Desktop.ViewModels;

public record InscricaoItemDto(
    int Id,
    string Participante,
    string Evento,
    string DataInscricao,
    string Status,
    int QuantidadeAtividades
);

public partial class InscricoesViewModel : ViewModelBase
{
    public ObservableCollection<InscricaoItemDto> Inscricoes { get; } = new()
    {
        new InscricaoItemDto(101, "Ana Beatriz Silva", "Semana de Ciência e Tecnologia 2026", "19/11/2026 09:30", "Ativa", 2),
        new InscricaoItemDto(102, "Carlos Eduardo Oliveira", "Semana de Ciência e Tecnologia 2026", "19/11/2026 10:15", "Ativa", 1),
        new InscricaoItemDto(103, "Juliana Mendes Santos", "Workshop de Inteligência Artificial Aplicada", "18/11/2026 14:00", "Ativa", 3),
        new InscricaoItemDto(104, "Lucas Gabriel Ferreira", "Semana de Ciência e Tecnologia 2026", "17/11/2026 16:45", "Cancelada", 0)
    };
}
