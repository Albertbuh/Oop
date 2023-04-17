namespace Converter;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Xml.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Converter
{
  public static void ConvertToPdf(string header, string text, string path)
  {
    PdfWriter writer = new PdfWriter(path);
    PdfDocument pdf = new PdfDocument(writer);
    Document doc = new Document(pdf);
    DateTime date = DateTime.Today;
    Paragraph pdf_header = new Paragraph(header + $"\n({date.ToString("U")})").SetTextAlignment(TextAlignment.CENTER).SetFontSize(24).SetBold();
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

#nullable disable
    private static readonly XDeclaration _defaultdeclaration = new XDeclaration("1.0", "utf-8", null);

    public static void JsonToXml(string jsonFileName, string filename)
    {
      string js_text = File.ReadAllText(jsonFileName);
      JArray ja = JArray.Parse(js_text); 

      StringBuilder strXml = new StringBuilder(_defaultdeclaration.ToString() + "\n");
      XDocument xml = new XDocument();

      foreach(var item in ja)
      {
        StringBuilder strElement = new StringBuilder();
        strElement.Append("{\n\"element\":");
        strElement.AppendLine(item.ToString());
        strElement.Append("}");
        var elem = JsonConvert.DeserializeXmlNode(strElement.ToString());
        xml = XDocument.Parse(elem.OuterXml);
        strXml.AppendLine(xml.ToString());
     }
      Console.WriteLine(strXml.ToString());
      if(filename.IndexOf(".xml") < 0)
        filename = filename + ".xml";
      File.WriteAllText(filename, strXml.ToString());
    }
#nullable restore
}
