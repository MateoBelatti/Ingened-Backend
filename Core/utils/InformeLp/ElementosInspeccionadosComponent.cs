using Core.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Core.utils.InformeLp;

public class ElementosInspeccionadosComponent : IComponent
{
    private readonly List<ElementoInspeccionadoDto> _elementos;

    public ElementosInspeccionadosComponent(List<ElementoInspeccionadoDto> elementos)
    {
        _elementos = elementos ?? new();
    }

    public void Compose(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(2); // LINEA/ISO/ELEM
                columns.RelativeColumn(1); // SPOOL
                columns.RelativeColumn(1); // CUÑO
                columns.RelativeColumn(1); // ESPESOR
                columns.RelativeColumn(1.2f); // DIAMETRO
                columns.RelativeColumn(1.5f); // CRITERIO
                columns.RelativeColumn(1.2f); // RESULTADO
            });

            // Headers
            HeaderCell(table, "LÍNEA\nISOMÉTRICO\nELEMENTO");
            HeaderCell(table, "SPOOL");
            HeaderCell(table, "CUÑO");
            HeaderCell(table, "ESPESOR\n[sch/mm]");
            HeaderCell(table, "Ø DIÁMETRO [\"]");
            HeaderCell(table, "CRITERIO DE\nACEPTACIÓN");
            HeaderCell(table, "RESULTADO");

            // Values
            foreach (var el in _elementos)
            {
                var col1 = $"{el.Linea}\n{el.Isometrico}\n{el.Elemento}".Trim();
                
                table.Cell().Background(Colors.Yellow.Lighten2).Border(1).BorderColor(Colors.Black).Padding(2).AlignCenter().AlignMiddle().Text(string.IsNullOrWhiteSpace(col1) ? "-" : col1).FontSize(8).Bold();
                ValueCell(table, el.Spool);
                ValueCell(table, el.Cuno);
                ValueCell(table, el.EspSch);
                ValueCell(table, el.Diam);
                ValueCell(table, el.Criterio).FontColor(Colors.Orange.Darken2).Bold();
                
                var resUpper = (el.Resultado ?? "").ToUpper();
                table.Cell().Background(resUpper.Contains("APROBADO") ? Colors.Yellow.Lighten2 : Colors.White)
                    .Border(1).BorderColor(Colors.Black).Padding(2).AlignCenter().AlignMiddle()
                    .Text(string.IsNullOrWhiteSpace(el.Resultado) ? "-" : el.Resultado).FontSize(8);
            }
        });
    }

    private static void HeaderCell(TableDescriptor table, string text)
    {
        table.Cell().Background(Colors.White).Border(1).BorderColor(Colors.Black).Padding(2).AlignCenter().AlignMiddle().Text(text).FontSize(7).Bold();
    }

    private static TextSpanDescriptor ValueCell(TableDescriptor table, string text)
    {
        return table.Cell().Background(Colors.White).Border(1).BorderColor(Colors.Black).Padding(2).AlignCenter().AlignMiddle()
            .Text(string.IsNullOrWhiteSpace(text) ? "-" : text).FontSize(8);
    }
}
