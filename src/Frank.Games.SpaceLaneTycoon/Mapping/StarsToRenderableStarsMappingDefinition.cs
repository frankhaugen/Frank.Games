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
            Position = new Vector3(decimal.ToSingle(star.X), decimal.ToSingle(star.Y), decimal.ToSingle(star.Z)),
            SpectralClassification = new SpectralClassification(star.Spect),
            Color = CalculateColor(star.Spect),
            Name = star.Proper,
            Radius = CalculateRadiusF(star),
        }).ToHashSet();
    }

    private float CalculateRadiusF(Star star)
    {
        if (star.Ci == null)
        {
            return 1;
        }
        
        // Constants
        const float sigma = 5.67e-8f; // Stefan-Boltzmann constant (W/m^2/K^4)
        const float solarLuminosity = 3.828e26f; // Solar luminosity (W)
        const float solarRadius = 6.96e8f; // Solar radius (m)

        var luminosity = decimal.ToSingle(star.Lum);
        var ci = decimal.ToSingle(star.Ci.GetValueOrDefault(1));
        var colorIndex = decimal.ToSingle(star.Ci.GetValueOrDefault(1));
        
        // Convert color index to temperature using the empirical formula
        var temperature = 4600f * (1f / (0.92f * ci + 1.7f) + 1f / (0.92f * colorIndex + 0.62f));

        // Convert luminosity from solar luminosities to Watts
        var luminosityInWatts = luminosity * solarLuminosity;

        // Calculate the radius using the Stefan-Boltzmann law rearranged for radius
        var radiusInMeters = MathF.Sqrt(luminosityInWatts / (4 * MathF.PI * sigma * MathF.Pow(temperature, 4)));

        // Convert the radius from meters to solar radii
        var radius =  radiusInMeters / solarRadius;
        
        return radius;
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