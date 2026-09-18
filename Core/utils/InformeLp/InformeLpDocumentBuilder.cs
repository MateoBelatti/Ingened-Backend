using Core.DTOs.InformeLp;
using Core.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Core.utils.InformeLp;

public class InformeLpDocumentBuilder : IDocument, IInformeDocumentBuilder
{
    private InformeLpDto _informe = null!;

    public InformeLpDocumentBuilder() { }

    public InformeLpDocumentBuilder(InformeLpDto informe)
    {
        _informe = informe;
    }

    public void SetData(InformeLpDto informe)
    {
        _informe = informe;
    }

    public byte[] GeneratePdf(object datos)
    {
        _informe = (InformeLpDto)datos;
        return Document.Create(Compose).GeneratePdf();
    }

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(1, Unit.Centimetre);
            page.PageColor(Colors.White);
            page.DefaultTextStyle(x => x.FontFamily("Arial").FontSize(9));

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
                columns.RelativeColumn(2);
                columns.RelativeColumn(7);
            });

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

            table.Cell().Component(new DatosArchivosComponent(_informe.DatosArchivos));
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.PaddingVertical(10).Column(col =>
        {
            col.Item().PaddingBottom(15).Element(ComposeHeader);

            col.Item().PaddingBottom(15).Component(new DatosGeneralesComponent(_informe.DatosGenerales));

            col.Item().Component(new ProcedimientoNormasComponent(
                _informe.ProcedimientoNormas,
                _informe.MaterialSuperficial?.Material ?? "-",
                _informe.DatosArchivos?.Rev ?? "-"));

            col.Item().PaddingBottom(15).Component(new MaterialSuperficialComponent(_informe.MaterialSuperficial));

            col.Item().PaddingBottom(15).Component(new ParametrosLpComponent(_informe.ParametrosLP));

            col.Item().PaddingBottom(5).Text("RESULTADOS DE INSPECCIÓN").FontSize(10).Bold();

            col.Item().PaddingBottom(15).Component(new ElementosInspeccionadosComponent(_informe.Elementos));

            col.Item().PaddingBottom(15).Component(new ResultadoGlobalComponent(_informe.ResultadoGlobal));

            col.Item().Component(new ResponsablesComponent(_informe.Responsables));

            col.Item().PageBreak();

            col.Item().PaddingBottom(10).Text("INSUMOS").FontSize(10).Bold();
            col.Item().Component(new ConsumiblesComponent(_informe.Consumibles));

            col.Item().PaddingTop(20).Component(new RegistroFotograficoComponent(_informe.RegistroFotografico));
        });
    }

    private void ComposeFooter(IContainer container)
    {
        container.PaddingTop(5);
    }
}