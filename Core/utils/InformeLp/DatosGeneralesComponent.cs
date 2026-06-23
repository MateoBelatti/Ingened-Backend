using Core.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Core.utils.InformeLp;

public class DatosGeneralesComponent : IComponent
{
    private readonly DatosGeneralesDto _datosGenerales;

    public DatosGeneralesComponent(DatosGeneralesDto datosArchivosDto)
    {
        _datosGenerales = datosArchivosDto;
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

            // Fila 1 - Títulos
            HeaderCell(table, "PROYECTO");
            HeaderCell(table, "COMPONENTE");
            HeaderCell(table, "SUBCONJUNTO");

            // Fila 2 - Valores
            ValueCell(table, _datosGenerales.Proyecto);
            ValueCell(table, _datosGenerales.Componente);
            ValueCell(table, _datosGenerales.Subconjunto);

            // Fila 3 - Títulos
            HeaderCell(table, "OBRA CONTRATO/OC:");
            HeaderCell(table, "PLANO");
            HeaderCell(table, "POSICIÓN N°");

            // Fila 4 - Valores
            ValueCell(table, _datosGenerales.Obra);
            ValueCell(table, _datosGenerales.Plano);
            ValueCell(table, _datosGenerales.Posicion);

            // Fila 5 - Título Lugar
            table.Cell().ColumnSpan(3)
                .Background(Colors.Grey.Lighten3)
                .Border(1).BorderColor(Colors.Black)
                .Padding(2).PaddingLeft(4)
                .Text("LUGAR").FontSize(7).Bold();

            // Fila 6 - Valor Lugar
            table.Cell().ColumnSpan(3)
                .Background(Colors.White)
                .Border(1).BorderColor(Colors.Black)
                .Padding(2).PaddingLeft(4)
                .Text(string.IsNullOrWhiteSpace(_datosGenerales.Lugar) ? "-" : _datosGenerales.Lugar).FontSize(9);
        });
    }

    private static void HeaderCell(TableDescriptor table, string text)
    {
        table.Cell()
            .Background(Colors.Grey.Lighten3)
            .Border(1).BorderColor(Colors.Black)
            .Padding(2).PaddingLeft(4)
            .Text(text).FontSize(7).Bold();
    }

    private static void ValueCell(TableDescriptor table, string text)
    {
        table.Cell()
            .Background(Colors.White)
            .Border(1).BorderColor(Colors.Black)
            .Padding(2).PaddingLeft(4)
            .Text(string.IsNullOrWhiteSpace(text) ? "-" : text).FontSize(9);
    }
}
