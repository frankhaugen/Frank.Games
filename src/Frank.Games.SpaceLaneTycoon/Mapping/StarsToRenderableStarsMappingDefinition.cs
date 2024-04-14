using System.Drawing;
using System.Numerics;

using Frank.Mapping;

namespace Frank.Games.SpaceLaneTycoon.Mapping;

public class StarsToRenderableStarsMappingDefinition : IMappingDefinition<HashSet<Star>, HashSet<RenderableStar>>
{
    /// <inheritdoc />
    public HashSet<RenderableStar> Map(HashSet<Star> from)
    {
        return from.Select(star => new RenderableStar()
        {
            Id = star.Id,
            Postition = new Vector3(decimal.ToSingle(star.X), decimal.ToSingle(star.Y), decimal.ToSingle(star.Z)),
            SpectralClassification = new SpectralClassification(star.Spect),
            Color = CalculateColor(star.Spect),
            Name = star.Proper,
        }).ToHashSet();
    }

    private Color CalculateColor(string? starSpect)
    {
        var spectralClassification = new SpectralClassification(starSpect);
        
        return spectralClassification.Type switch
        {
            SpectralType.L => Color.Blue,
            SpectralType.B => Color.Cyan,
            SpectralType.A => Color.White,
            SpectralType.F => Color.Yellow,
            SpectralType.G => Color.Orange,
            SpectralType.K => Color.Red,
            SpectralType.M => Color.DarkRed,
            _ => Color.Gray
        };
    }
}