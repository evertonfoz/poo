using CommunityToolkit.Mvvm.ComponentModel;

namespace SistemaGestaoEventosAcademicos.Apresentacao.Desktop.ViewModels;

public partial class RelatoriosViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _descricao = "Gere relatórios analíticos de taxa de ocupação, lista de presença por atividade e emita certificados acadêmicos validados.";
}
