using Frank.Games.SpaceLaneTycoon.Mapping;
using Frank.Mapping;

namespace Frank.Games.SpaceLaneTycoon;

public class StarCache
{
    private readonly HashSet<RenderableStar> _stars;
    
    public StarCache(IMappingDefinition<FileInfo, HashSet<RenderableStar>> fileInfoToStarsMappingDefinition, Assets assets)
    {
        _stars = fileInfoToStarsMappingDefinition.Map(assets.StarsFile).OrderBy(x => x.Id).Take(1000).ToHashSet();
        Console.WriteLine($"Loaded {_stars.Count} stars");
    }
    
    public IEnumerable<RenderableStar> Stars => _stars;
}