namespace Core.Interfaces;

public interface IInformeDocumentBuilder
{
    byte[] GeneratePdf(object datos);
}