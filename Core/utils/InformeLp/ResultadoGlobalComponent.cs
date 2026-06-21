using Core.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Core.utils.InformeLp;

public class ResultadoGlobalComponent : IComponent
{
    private readonly ResultadoGlobalDataDto _dto;

    public ResultadoGlobalComponent(ResultadoGlobalDataDto dto)
    {
        _dto = dto;
    }

    public void Compose(IContainer container)
    {
        container.Border(1).BorderColor(Colors.Black).Padding(5).Row(row =>
        {
            var res = _dto?.ResultadoGlobal?.ToUpper() ?? "";
            
            row.RelativeItem().Row(r => {
                r.AutoItem().Text(res == "ACEPTADO" || res == "ACEPTABLE" ? "☒" : "☐").FontSize(14);
                r.RelativeItem().PaddingLeft(5).AlignMiddle().Text("ACEPTABLE").FontSize(8);
            });
            row.RelativeItem().Row(r => {
                r.AutoItem().Text(res.Contains("INDICACIONES") ? "☒" : "☐").FontSize(14);
                r.RelativeItem().PaddingLeft(5).AlignMiddle().Text("ACEPTABLE CON INDICACIONES").FontSize(8);
            });
            row.RelativeItem().Row(r => {
                r.AutoItem().Text(res == "RECHAZADO" || res == "NO ACEPTABLE" ? "☒" : "☐").FontSize(14);
                r.RelativeItem().PaddingLeft(5).AlignMiddle().Text("NO ACEPTABLE").FontSize(8);
            });
        });
    }
}
