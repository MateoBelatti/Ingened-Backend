using System.IO;
using Core.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Core.utils.InformeLp;

public class ConsumiblesComponent : IComponent
{
    private readonly List<ConsumibleDto> _consumibles;

    public ConsumiblesComponent(List<ConsumibleDto> consumibles)
    {
        _consumibles = consumibles ?? new();
    }

    public void Compose(IContainer container)
    {
        container.Column(col =>
        {
            col.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Cell().Background(Colors.White).Border(1).BorderColor(Colors.Black).Padding(2).PaddingLeft(4).Text("LOTE LÍQUIDOS").FontSize(7).Bold();
                table.Cell().Background(Colors.White).Border(1).BorderColor(Colors.Black).Padding(2).PaddingLeft(4).Text("MARCA").FontSize(7).Bold();
                table.Cell().Background(Colors.White).Border(1).BorderColor(Colors.Black).Padding(2).PaddingLeft(4).Text("FECHA DE VENCIMIENTO").FontSize(7).Bold();

                foreach (var con in _consumibles)
                {
                    var loteTxt = string.IsNullOrWhiteSpace(con.Lote) ? "-" : con.Lote;
                    var prodTxt = string.IsNullOrWhiteSpace(con.Producto) ? "" : $"{con.Producto} - ";
                    ValueCell(table, $"{prodTxt}{loteTxt}");
                    ValueCell(table, con.Marca);
                    ValueCell(table, con.Vencimiento);
                }
            });

            // Procesar imágenes de consumibles
            var todasLasImagenes = _consumibles.Where(c => c.Imagenes != null).SelectMany(c => c.Imagenes!).ToList();
            if (todasLasImagenes.Any())
            {
                col.Item().PaddingTop(10).Grid(grid =>
                {
                    grid.Columns(3); // 3 imágenes por fila
                    grid.Spacing(5);

                    foreach (var img in todasLasImagenes)
                    {
                        using var ms = new MemoryStream();
                        img.CopyTo(ms);
                        grid.Item().Image(ms.ToArray()).FitArea();
                    }
                });
            }
        });
    }
    
    private static void ValueCell(TableDescriptor table, string text)
    {
        table.Cell().Background(Colors.White).Border(1).BorderColor(Colors.Black).Padding(2).PaddingLeft(4).Text(string.IsNullOrWhiteSpace(text) ? "-" : text).FontSize(9);
    }
}
