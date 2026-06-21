using System.IO;
using Core.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Core.utils.InformeLp;

public class RegistroFotograficoComponent : IComponent
{
    private readonly RegistroFotograficoDataDto? _dto;

    public RegistroFotograficoComponent(RegistroFotograficoDataDto? dto)
    {
        _dto = dto;
    }

    public void Compose(IContainer container)
    {
        container.Column(col =>
        {
            col.Item().PaddingBottom(10).Text("REGISTRO FOTOGRÁFICO").FontSize(10).Bold();
            
            if (_dto?.Fotos != null && _dto.Fotos.Any())
            {
                col.Item().Grid(grid =>
                {
                    grid.Columns(2); // 2 imágenes por fila en el registro fotográfico para que se vean más grandes
                    grid.Spacing(10);

                    foreach (var foto in _dto.Fotos)
                    {
                        using var ms = new MemoryStream();
                        foto.CopyTo(ms);
                        grid.Item().Image(ms.ToArray()).FitArea();
                    }
                });
            }
            else
            {
                col.Item().Border(1).BorderColor(Colors.Grey.Lighten2).Height(100).AlignCenter().AlignMiddle()
                    .Text("No se adjuntaron fotos en el registro.").FontSize(8).FontColor(Colors.Grey.Medium);
            }
        });
    }
}
