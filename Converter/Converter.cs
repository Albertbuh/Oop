using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Xml.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PluginBase;

public class Converter : ICommand
{
  public string Name { get => "Converter";}
  public string Description { get => "Convert (Pdf, xml) ";}


  public void Execute(params object[] args)
  {
    int op = (int)args[0];
    switch(op)
    {
      case 1:
        if(args.Length >= 4)
        {
          string header = (string)args[1];
          string TextToConvert = (string)args[2];
          string PdfPath = (string)args[3];
          ConvertToPdf(header, TextToConvert, PdfPath);
        }
        break;
      case 2:
        if(args.Length >= 3)
        {
          string jsonFileName = (string)args[1];
          string filename = (string)args[2];
          JsonToXml(jsonFileName, filename);
        }
        break;
      case 3:
        if(args.Length >= 3)
        {
          string? xmlFileName = args[1] as string;
          string? jsonFileName = args[2] as string;
          if(xmlFileName != null && jsonFileName != null)
            XmlToJson(xmlFileName, jsonFileName);
          else
            throw new InvalidOperationException("Not string send to filename");
        }
        break;
      default:
        Console.WriteLine("Fking converter");
        break;
    }
  }



  public static void ConvertToPdf(string header, string text, string PdfPath)
  {
    PdfWriter writer = new PdfWriter(PdfPath);
    PdfDocument pdf = new PdfDocument(writer);
    Document doc = new Document(pdf);
    var date = DateTime.Today;
    Paragraph pdf_header = new Paragraph(header + $"\n({date.ToString("D")})").SetTextAlignment(TextAlignment.CENTER).SetFontSize(24).SetBold();
    doc.Add(pdf_header);
    Paragraph p = new Paragraph(text).SetTextAlignment(TextAlignment.CENTER);
    doc.Add(p);
    doc.Close();
    Console.WriteLine("Pdf converterd...");
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
      strXml.AppendLine("<elements>");

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
      strXml.AppendLine("</elements>");
      Console.WriteLine(strXml.ToString());
      if(filename.IndexOf(".xml") < 0)
        filename = filename + ".xml";
      File.WriteAllText(filename, strXml.ToString());
    }
#nullable restore

    public static void XmlToJson(string xmlFileName, string jsonFileName)
    {
      var doc = XDocument.Parse(File.ReadAllText(xmlFileName));
      string text = JsonConvert.SerializeXNode(doc, Formatting.Indented);
      text = text.Substring(text.IndexOf("["));
      File.WriteAllText(jsonFileName, text);
    }
}
