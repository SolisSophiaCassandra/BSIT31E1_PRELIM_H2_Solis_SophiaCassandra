using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ConsoleApp1;

public class XmlFileReader : BaseFileReader
{
    public override string SupportedFormat => "XML";

    protected override void ParseContent(string filePath)
    {
        Console.WriteLine(" -> Opening XML stream...");

        string raw = File.ReadAllText(filePath);
        string rootName = "Unknown";
        int totalNodes = 0;

        try
        {
            XDocument doc = XDocument.Parse(raw);
            rootName = doc.Root?.Name.LocalName ?? "None";
            totalNodes = doc.Descendants().Count();

            Console.WriteLine($" -> Detected XML Root Element: <{rootName}> with {totalNodes} total descendant nodes.");
        }
        catch (System.Xml.XmlException)
        {
            Console.WriteLine(" -> Warning: Invalid XML format detected during parsing.");
        }

        string displayContent = raw.Length > 100
            ? raw.Substring(0, 100) + "..."
            : raw;

        Console.WriteLine("\n--- First 100 Characters ---");
        Console.WriteLine(displayContent);
        Console.WriteLine("----------------------------\n");
    }
}