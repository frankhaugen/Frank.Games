namespace Frank.Games.SpaceLaneTycoon;

public class SpectralClassification
{
    public SpectralType Type { get; set; }
    public int? Subclass { get; set; }

    public SpectralClassification(string? spect)
    {
        if (string.IsNullOrEmpty(spect))
        {
            Type = SpectralType.Unknown;
            Subclass = -1;
        }
        else
        {
            Type = ParseSpectralType(spect[0]);

            if (spect.Length > 1 && int.TryParse(spect.AsSpan(1, 1), out int subclass))
                Subclass = subclass;
        }
    }

    private SpectralType ParseSpectralType(char spect)
    {
        return spect switch
        {
            'O' => SpectralType.O,
            'B' => SpectralType.B,
            'A' => SpectralType.A,
            'F' => SpectralType.F,
            'G' => SpectralType.G,
            'K' => SpectralType.K,
            'M' => SpectralType.M,
            'L' => SpectralType.L,
            'T' => SpectralType.T,
            'Y' => SpectralType.Y,
            _ => SpectralType.Unknown,
        };
    }

    public override string ToString()
    {
        if (Type == SpectralType.Unknown || Subclass == -1)
            return "Unknown";
        
        return $"{Type}{Subclass}";
    }
}