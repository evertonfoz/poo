using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SistemaGestaoEventosAcademicos.Apresentacao.Desktop.Models;

namespace SistemaGestaoEventosAcademicos.Apresentacao.Desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isPaneOpen = true;

    [ObservableProperty]
    private MenuItemModel? _selectedMenuItem;

    [ObservableProperty]
    private ViewModelBase? _currentPage;

    [ObservableProperty]
    private string _paginaTitulo = "Painel Principal";

    [ObservableProperty]
    private string _paginaDescricao = "Visão geral e indicadores do sistema acadêmico";

    public ObservableCollection<MenuItemModel> MenuItems { get; } = new();

    public MainWindowViewModel()
    {
        // Registro dos itens do menu lateral com ícones e metadados
        MenuItems.Add(new MenuItemModel("Painel Principal", "Métricas e visão geral", "📊", typeof(DashboardViewModel)));
        MenuItems.Add(new MenuItemModel("Eventos", "Gestão de eventos e cronograma", "📅", typeof(EventosViewModel)));
        MenuItems.Add(new MenuItemModel("Participantes", "Cadastro e consulta de participantes", "👥", typeof(ParticipantesViewModel)));
        MenuItems.Add(new MenuItemModel("Inscrições", "Inscrições em eventos e atividades", "📝", typeof(InscricoesViewModel)));
        MenuItems.Add(new MenuItemModel("Credenciamento", "Check-in de presença em tempo real", "🎟️", typeof(CredenciamentoViewModel)));
        MenuItems.Add(new MenuItemModel("Relatórios e Certificados", "Ocupação e emissão acadêmica", "🎓", typeof(RelatoriosViewModel)));

        // Define a tela inicial padrão (Dashboard)
        SelectedMenuItem = MenuItems[0];
    }

    [RelayCommand]
    private void TogglePane()
    {
        IsPaneOpen = !IsPaneOpen;
    }

    partial void OnSelectedMenuItemChanged(MenuItemModel? value)
    {
        if (value is null)
            return;

        PaginaTitulo = value.Titulo;
        PaginaDescricao = value.Subtitulo;

        CurrentPage = Activator.CreateInstance(value.ViewModelType) as ViewModelBase;
    }
}
