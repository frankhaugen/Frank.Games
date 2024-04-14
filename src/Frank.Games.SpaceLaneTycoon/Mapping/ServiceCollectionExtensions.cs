using Frank.Mapping;

using Microsoft.Extensions.DependencyInjection;

namespace Frank.Games.SpaceLaneTycoon.Mapping;

public static class ServiceCollectionExtensions
{
    public static void AddMapping(this IServiceCollection services)
    {
        services.AddMappingDefinition<FileInfo, HashSet<Star>, FileInfoToStarsMappingDefinition>();
        services.AddMappingDefinition<HashSet<Star>, HashSet<RenderableStar>, StarsToRenderableStarsMappingDefinition>();
        services.AddMappingDefinition<FileInfo, HashSet<RenderableStar>, FileInfoToRenderableStarsMappingDefinition>();
    }
}