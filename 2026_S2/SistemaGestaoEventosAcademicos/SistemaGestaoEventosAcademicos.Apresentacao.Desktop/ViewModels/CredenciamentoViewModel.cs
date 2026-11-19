using CommunityToolkit.Mvvm.ComponentModel;

namespace SistemaGestaoEventosAcademicos.Apresentacao.Desktop.ViewModels;

public partial class CredenciamentoViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _cpfOuInscricao = string.Empty;

    [ObservableProperty]
    private string _mensagemStatus = "Informe o CPF ou número de inscrição para registrar a presença no evento em andamento.";

    [ObservableProperty]
    private bool _isSucesso;
}
