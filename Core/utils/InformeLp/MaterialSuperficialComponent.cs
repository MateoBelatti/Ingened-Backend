using Core.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Core.utils.InformeLp;

public class MaterialSuperficialComponent : IComponent
{
    private readonly MaterialSuperficialDto _dto;

    public MaterialSuperficialComponent(MaterialSuperficialDto dto)
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
            });

            table.Cell().Background(Colors.Grey.Lighten3).Border(1).BorderColor(Colors.Black).Padding(2).PaddingLeft(4).Text("FORMA DEL MATERIAL").FontSize(7).Bold();
            table.Cell().Background(Colors.White).Border(1).BorderColor(Colors.Black).Padding(2).PaddingLeft(4).Text(string.IsNullOrWhiteSpace(_dto.FormaMaterial) ? "-" : _dto.FormaMaterial).FontSize(9);

            table.Cell().Background(Colors.Grey.Lighten3).Border(1).BorderColor(Colors.Black).Padding(2).PaddingLeft(4).Text("CONDICIONES SUPERFICIALES").FontSize(7).Bold();
            table.Cell().Background(Colors.White).Border(1).BorderColor(Colors.Black).Padding(2).PaddingLeft(4).Text(string.IsNullOrWhiteSpace(_dto.CondSuperficiales) ? "-" : _dto.CondSuperficiales).FontSize(9);
        });
    }
}
