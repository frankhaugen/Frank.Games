using Frank.Mapping;

namespace Frank.Games.SpaceLaneTycoon.Mapping;

public class FileInfoToRenderableStarsMappingDefinition : IMappingDefinition<FileInfo, HashSet<RenderableStar>>
{
    private readonly IMappingDefinition<FileInfo, HashSet<Star>> _fileInfoToStarsMappingDefinition;
    private readonly IMappingDefinition<HashSet<Star>, HashSet<RenderableStar>> _starsToRenderableStarsMappingDefinition;

    public FileInfoToRenderableStarsMappingDefinition(
        IMappingDefinition<FileInfo, HashSet<Star>> fileInfoToStarsMappingDefinition,
        IMappingDefinition<HashSet<Star>, HashSet<RenderableStar>> starsToRenderableStarsMappingDefinition
    )
    {
        _fileInfoToStarsMappingDefinition = fileInfoToStarsMappingDefinition;
        _starsToRenderableStarsMappingDefinition = starsToRenderableStarsMappingDefinition;
    }


    /// <inheritdoc />
    public HashSet<RenderableStar> Map(FileInfo from)
    {
        var stars = _fileInfoToStarsMappingDefinition.Map(from);
        Console.WriteLine($"Converted {stars.Count} stars from {from.Name}");
        var renderableStars = _starsToRenderableStarsMappingDefinition.Map(stars);
        Console.WriteLine($"Converted {stars.Count} stars to {renderableStars.Count} renderable stars");
        return renderableStars;
    }
}