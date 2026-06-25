using System;
using System.IO;
using System.Text.Json;

namespace ConsoleApp1;

public class JsonFileReader : BaseFileReader
{
    public override string SupportedFormat => "JSON";

    protected override void ParseContent(string filePath)
    {
        Console.WriteLine(" -> Opening JSON stream...");

        string raw = File.ReadAllText(filePath);
        int elementCount = 0;

        try
        {
            using JsonDocument doc = JsonDocument.Parse(raw);

            // Count root properties if it's an object, or items if it's an array
            if (doc.RootElement.ValueKind == JsonValueKind.Object)
            {
                foreach (var _ in doc.RootElement.EnumerateObject()) elementCount++;
            }
            else if (doc.RootElement.ValueKind == JsonValueKind.Array)
            {
                elementCount = doc.RootElement.GetArrayLength();
            }

            // Updated to match your exact output string
            Console.WriteLine($" -> Parsed {elementCount} root properties.");
        }
        catch (JsonException)
        {
            Console.WriteLine(" -> Error: Invalid JSON format.");
        }

        // Clean up formatting (remove line breaks and spaces) so the preview is a tight single line
        string cleanedRaw = raw.Replace("\r", "").Replace("\n", "").Replace(" ", "");

        // Keep the exact substring cutoff with trailing ellipsis on its own line
        string displayContent = cleanedRaw.Length > 100
            ? cleanedRaw.Substring(0, 100) + "\n..."
            : cleanedRaw;

        Console.WriteLine("\n--- First 100 Characters ---");
        Console.WriteLine(displayContent);
        Console.WriteLine("----------------------------\n");
    }
}