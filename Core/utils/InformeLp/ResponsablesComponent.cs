using Core.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Core.utils.InformeLp;

public class ResponsablesComponent : IComponent
{
    private readonly ResponsablesDataDto _dto;

    public ResponsablesComponent(ResponsablesDataDto dto)
    {
        _dto = dto;
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

            HeaderCell(table, "REALIZO");
            HeaderCell(table, "REVISO");
            HeaderCell(table, "CLIENTE");

            ValueCell(table, _dto.Realizo);
            ValueCell(table, _dto.Reviso);
            ValueCell(table, "D&D"); // Placeholder as per PDF

            HeaderCell(table, "NIVEL END");
            HeaderCell(table, "NIVEL END");
            HeaderCell(table, "NIVEL END");

            ValueCell(table, "NIVEL II");
            ValueCell(table, "NIVEL II");
            ValueCell(table, "-");

            HeaderCell(table, "FECHA Y LUGAR");
            HeaderCell(table, "FECHA Y LUGAR");
            HeaderCell(table, "FECHA Y LUGAR");

            ValueCell(table, "-");
            ValueCell(table, "-");
            ValueCell(table, "-");
        });
    }
    
    private static void HeaderCell(TableDescriptor table, string text)
    {
        table.Cell().Background(Colors.White).Border(1).BorderColor(Colors.Black).Padding(2).PaddingLeft(4).Text(text).FontSize(7).Bold();
    }

    private static void ValueCell(TableDescriptor table, string text)
    {
        table.Cell().Background(Colors.White).Border(1).BorderColor(Colors.Black).Padding(2).PaddingLeft(4).PaddingBottom(10).Text(string.IsNullOrWhiteSpace(text) ? "-" : text).FontSize(9);
    }
}
