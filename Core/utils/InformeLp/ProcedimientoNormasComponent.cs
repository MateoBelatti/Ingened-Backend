using Core.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Core.utils.InformeLp;

public class ProcedimientoNormasComponent : IComponent
{
    private readonly ProcedimientoNormasDto _dto;
    private readonly string _material;
    private readonly string _revision;

    public ProcedimientoNormasComponent(ProcedimientoNormasDto dto, string material, string revision)
    {
        _dto = dto;
        _material = material;
        _revision = revision;
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

            HeaderCell(table, "PROCEDIMIENTO GENERAL:");
            HeaderCell(table, "ENSAYO N°:");
            HeaderCell(table, "NORMA");

            ValueCell(table, _dto.ProcGeneral);
            ValueCell(table, _dto.EnsayoTipo);
            ValueCell(table, _dto.Norma);

            HeaderCell(table, "PROCEDIMIENTO ESPECÍFICO:");
            HeaderCell(table, "REVISIÓN N°");
            HeaderCell(table, "MATERIAL");

            ValueCell(table, _dto.ProcEspecifico);
            ValueCell(table, _revision);
            ValueCell(table, _material);
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
