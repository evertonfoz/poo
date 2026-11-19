using System;
using Avalonia;

namespace SistemaGestaoEventosAcademicos.Apresentacao.Desktop;

internal sealed class Program
{
    // Método de inicialização da aplicação desktop Avalonia.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
