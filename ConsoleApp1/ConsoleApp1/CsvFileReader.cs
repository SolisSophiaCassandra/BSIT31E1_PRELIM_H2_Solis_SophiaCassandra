namespace ConsoleApp1;

public class CsvFileReader : BaseFileReader
{
    public override string SupportedFormat => "CSV";

    protected override void ParseContent(string filePath)
    {
        Console.WriteLine(" -> Opening CSV stream...");

        var lines = File.ReadAllLines(filePath);
        int rowCount = lines.Length;

        int colCount = 0;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            colCount = line.Split(',').Length;
            break;
        }

        Console.WriteLine($" -> Detected {rowCount} rows and {colCount} columns.");

        string raw = string.Join(Environment.NewLine, lines);

        string displayContent = raw.Length > 100
            ? raw.Substring(0, 100) + "..."
            : raw;

        Console.WriteLine("\n--- First 100 Characters ---");
        Console.WriteLine(displayContent);
        Console.WriteLine("----------------------------\n");
    }
}