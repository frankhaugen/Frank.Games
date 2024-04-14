using System.Globalization;

using CsvHelper;
using CsvHelper.Configuration;

using Frank.Mapping;

namespace Frank.Games.SpaceLaneTycoon.Mapping;

public class FileInfoToStarsMappingDefinition : IMappingDefinition<FileInfo, HashSet<Star>>
{
    private readonly CsvConfiguration _csvConfiguration = new(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
        Delimiter = ",",
        IgnoreBlankLines = true,
        TrimOptions = TrimOptions.Trim
    };
    
    /// <inheritdoc />
    public HashSet<Star> Map(FileInfo from)
    {
        if (from is null) throw new ArgumentNullException(nameof(from));
        if (!from.Exists) throw new ArgumentException("File does not exist.", nameof(from));
        if (!from.Name.EndsWith(".csv", StringComparison.OrdinalIgnoreCase)) throw new ArgumentException("File is not a CSV file.", nameof(from));
        if (from.Length == 0) throw new ArgumentException("File is empty.", nameof(from));
        if (from.Length > 1_000_000_000) throw new ArgumentException("File is too large.", nameof(from));
        if (from.Length < 1000) throw new ArgumentException("File is too small.", nameof(from));
        
        using var fileStream = from.OpenRead();
        using var reader = new StreamReader(fileStream);
        using var csv = new CsvReader(reader, _csvConfiguration);
        csv.Context.RegisterClassMap<StarClassMap>();
        var result = csv.GetRecords<Star>().ToHashSet();
        Console.WriteLine($"Loaded {result.Count} stars from {from.Name}");
        return result;
    }
}