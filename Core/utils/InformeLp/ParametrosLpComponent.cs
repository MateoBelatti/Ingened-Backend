using Core.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Core.utils.InformeLp;

public class ParametrosLpComponent : IComponent
{
    private readonly ParametrosLPDto _dto;

    public ParametrosLpComponent(ParametrosLPDto dto)
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

            // Fila 1
            HeaderCell(table, "TIPO DE PENETRANTE");
            HeaderCell(table, "TIPO DE REVELADOR");
            HeaderCell(table, "TIPO DE REMOVEDOR");

            ValueCell(table, _dto.TipoPenetrante);
            ValueCell(table, _dto.TipoRevelador);
            ValueCell(table, _dto.TipoRemovedor);

            // Fila 2
            HeaderCell(table, "EXTENSIÓN DEL ENSAYO");
            HeaderCell(table, "LIMPIEZA INICIAL");
            HeaderCell(table, "TIEMPO DE SECADO");

            ValueCell(table, _dto.ExtensionEnsayo);
            ValueCell(table, _dto.LimpiezaInicial);
            ValueCell(table, _dto.TiempoSecado); 

            // Fila 3
            HeaderCell(table, "APLICACIÓN DEL PENETRANTE");
            HeaderCell(table, "TIEMPO DE PENETRACIÓN");
            HeaderCell(table, "TEMPERATURA DE LA SUPERFICIE");

            ValueCell(table, _dto.AplicacionPenetrante);
            ValueCell(table, _dto.TiempoPenetracion);
            ValueCell(table, _dto.Temperatura);

            // Fila 4
            HeaderCell(table, "REMOCIÓN DEL EXCESO DE PENETRANTE");
            HeaderCell(table, "TIEMPO DE SECADO"); 
            HeaderCell(table, "APLICACIÓN DEL REVELADOR");

            ValueCell(table, _dto.RemocionExceso);
            ValueCell(table, _dto.TiempoSecado);
            ValueCell(table, _dto.AplicacionRevelador);

            // Fila 5
            HeaderCell(table, "TIEMPO DE REVELADO");
            HeaderCell(table, "LIMPIEZA POS EXAMEN");
            HeaderCell(table, "ILUMINACIÓN (Lux)");

            ValueCell(table, _dto.TiempoRevelado);
            ValueCell(table, _dto.LimpiezaPostExamen);
            ValueCell(table, _dto.Iluminacion);
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
