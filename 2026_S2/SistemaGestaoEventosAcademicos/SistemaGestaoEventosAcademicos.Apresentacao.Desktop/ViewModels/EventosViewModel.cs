using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SistemaGestaoEventosAcademicos.Apresentacao.Desktop.ViewModels;

public record EventoItemDto(
    int Id,
    string Nome,
    string Periodo,
    string Local,
    string Status,
    string Capacidade
);

public partial class EventosViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _filtro = string.Empty;

    public ObservableCollection<EventoItemDto> Eventos { get; } = new()
    {
        new EventoItemDto(1, "Semana de Ciência e Tecnologia 2026", "24/11/2026 a 28/11/2026", "Auditório Central", "Inscrições Abertas", "120/150 vagas"),
        new EventoItemDto(2, "Workshop de Inteligência Artificial Aplicada", "01/12/2026 a 03/12/2026", "Laboratório 04", "Inscrições Abertas", "40/40 vagas (Esgotado)"),
        new EventoItemDto(3, "I Simpósio de Engenharia de Software", "15/12/2026 a 18/12/2026", "Centro de Convenções", "Previsto", "0/300 vagas"),
        new EventoItemDto(4, "Jornada Acadêmica de Computação", "10/11/2026 a 12/11/2026", "Auditório B", "Concluído", "95/100 vagas")
    };
}
