namespace Converter;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

public class Converter 
{
  public static void ConvertToPdf(string header, string text, string path)
  {
    PdfWriter writer = new PdfWriter(path);
    PdfDocument pdf = new PdfDocument(writer);
    Document doc = new Document(pdf);
    Paragraph pdf_header = new Paragraph(header).SetTextAlignment(TextAlignment.CENTER).SetFontSize(24);
    doc.Add(pdf_header);
    Paragraph p = new Paragraph(text).SetTextAlignment(TextAlignment.CENTER);
    doc.Add(p);
    doc.Close();
  }
  public static void ConvertToPdf(string text, string path)
  {
    PdfWriter writer = new PdfWriter(path);
    PdfDocument pdf = new PdfDocument(writer);
    Document doc = new Document(pdf);
    Paragraph p = new Paragraph(text);
    doc.Add(p);
    doc.Close();
  }
}
