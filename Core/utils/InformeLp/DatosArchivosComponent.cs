using Core.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Core.utils.InformeLp;

public class DatosArchivosComponent : IComponent
{
    private readonly DatosArchivosDto _dto;

    public DatosArchivosComponent(DatosArchivosDto dto)
    {
        _dto = dto ?? new DatosArchivosDto();
    }

    public void Compose(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            // Fila 1
            HeaderCell(table, "Nr. Inf.");
            HeaderCell(table, "CLIENTE");
            HeaderCell(table, "CÓDIGO");

            ValueCell(table, _dto.NrInf);
            ValueCell(table, _dto.Cliente);
            ValueCell(table, _dto.Codigo);

            // Fila 2
            HeaderCell(table, "OC / CONTRATO");
            HeaderCell(table, "REVISIÓN");
            HeaderCell(table, "FECHA");

            ValueCell(table, _dto.Oc);
            ValueCell(table, _dto.Rev);
            ValueCell(table, _dto.Fecha);
        });
    }

    private static void HeaderCell(TableDescriptor table, string text)
    {
        table.Cell().Background(Colors.Grey.Lighten3).Border(1).BorderColor(Colors.Black).Padding(2).PaddingLeft(4).Text(text).FontSize(7).Bold();
    }

    private static void ValueCell(TableDescriptor table, string text)
    {
        table.Cell().Background(Colors.White).Border(1).BorderColor(Colors.Black).Padding(2).PaddingLeft(4).Text(string.IsNullOrWhiteSpace(text) ? "-" : text).FontSize(9);
    }
}
