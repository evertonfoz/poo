using CommunityToolkit.Mvvm.ComponentModel;

namespace SistemaGestaoEventosAcademicos.Apresentacao.Desktop.ViewModels;

public partial class DashboardViewModel : ViewModelBase
{
    [ObservableProperty]
    private int _totalEventos = 4;

    [ObservableProperty]
    private int _eventosInscricoesAbertas = 2;

    [ObservableProperty]
    private int _totalParticipantes = 128;

    [ObservableProperty]
    private int _totalInscricoesAtivas = 165;

    [ObservableProperty]
    private double _taxaOcupacaoGeral = 74.5;
}
