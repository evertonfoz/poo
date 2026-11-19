using System;

namespace SistemaGestaoEventosAcademicos.Apresentacao.Desktop.Models;

public record MenuItemModel(
    string Titulo,
    string Subtitulo,
    string Icone,
    Type ViewModelType
);
