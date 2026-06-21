using Core.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Core.utils.InformeLp;

public class InformeLpDocument : IDocument
{
    private readonly InformeDTO _informe;

    public InformeLpDocument(InformeDTO informe)
    {
        _informe = informe;
    }

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(1, Unit.Centimetre);
            page.PageColor(Colors.White);
            page.DefaultTextStyle(x => x.FontFamily("Arial").FontSize(9));

            // page.Header() no se usa porque el encabezado va solo en la pág 1
            page.Content().Element(ComposeContent);
            page.Footer().Element(ComposeFooter);
        });
    }

    private void ComposeHeader(IContainer container)
    {
        container.BorderTop(1).BorderLeft(1).BorderRight(1).BorderColor(Colors.Black).Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(2); // Logo
                columns.RelativeColumn(7); // Datos Archivos
            });

            // Logo cell
            table.Cell().BorderRight(1).BorderColor(Colors.Black).Padding(5).AlignCenter().AlignMiddle().Column(c =>
            {
                c.Item().AlignCenter().Text(txt => 
                {
                    txt.Span("Ing").FontSize(24).FontColor(Colors.Blue.Darken2).Bold();
                    txt.Span("ened").FontSize(24).FontColor(Colors.Orange.Darken2).Bold();
                });
                c.Item().AlignCenter().Text("Ingeniería Energía Desarrollo").FontSize(6).FontColor(Colors.Grey.Darken2);
                
                c.Item().PaddingTop(10).AlignCenter().Text(txt =>
                {
                    txt.Span("Cantidad de hojas: ").FontSize(8);
                    txt.TotalPages().FontSize(8);
                });
            });

            // Datos Archivos cell
            table.Cell().Component(new DatosArchivosComponent(_informe.DatosArchivos));
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.PaddingVertical(10).Column(col =>
        {
            // Encabezado solo en la primera página
            col.Item().PaddingBottom(15).Element(ComposeHeader);

            // 1. Datos generales
            col.Item().PaddingBottom(15).Component(new DatosGeneralesComponent(_informe.DatosGenerales));

            // 2. Procedimientos normas y Material (se muestran juntos)
            col.Item().Component(new ProcedimientoNormasComponent(
                _informe.ProcedimientoNormas, 
                _informe.MaterialSuperficial?.Material ?? "-", 
                _informe.DatosArchivos?.Rev ?? "-"));
                
            col.Item().PaddingBottom(15).Component(new MaterialSuperficialComponent(_informe.MaterialSuperficial));

            // 3. Parámetros LP
            col.Item().PaddingBottom(15).Component(new ParametrosLpComponent(_informe.ParametrosLP));

            // Espaciado antes de elementos
            col.Item().PaddingBottom(5).Text("RESULTADOS DE INSPECCIÓN").FontSize(10).Bold();

            // 4. Elementos inspeccionados
            col.Item().PaddingBottom(15).Component(new ElementosInspeccionadosComponent(_informe.Elementos));

            // 5. Resultado Global
            col.Item().PaddingBottom(15).Component(new ResultadoGlobalComponent(_informe.ResultadoGlobal));

            // 6. Responsables
            col.Item().Component(new ResponsablesComponent(_informe.Responsables));

            // Salto de pagina
            col.Item().PageBreak();

            // 7. Consumibles
            col.Item().PaddingBottom(10).Text("INSUMOS").FontSize(10).Bold();
            col.Item().Component(new ConsumiblesComponent(_informe.Consumibles));

            // 8. Registro fotográfico
            col.Item().PaddingTop(20).Component(new RegistroFotograficoComponent(_informe.RegistroFotografico));
        });
    }

    private void ComposeFooter(IContainer container)
    {
        // El footer de la referencia es nulo/vacío aparte de lo ya implementado
        // container.BorderTop(1).BorderColor(Colors.Black).PaddingTop(5).AlignCenter().Text(x => ...);
        // Lo dejamos vacío porque las páginas ya están en el encabezado.
        // Pero para no romper, mantenemos un padding vacío.
        container.PaddingTop(5);
    }
}
