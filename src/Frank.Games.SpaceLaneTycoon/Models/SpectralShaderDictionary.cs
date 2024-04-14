namespace Frank.Games.SpaceLaneTycoon;

public static class SpectralShaderDictionary
{
    private static readonly Dictionary<SpectralType, ShaderFlavor> _shaders;

    static SpectralShaderDictionary()
    {
        _shaders = new Dictionary<SpectralType, ShaderFlavor>()
        {
            { SpectralType.O, new ShaderFlavor(1.0f, (0.5f, 0.5f, 1.0f)) },
            { SpectralType.B, new ShaderFlavor(0.9f, (0.7f, 0.7f, 1.0f)) },
            { SpectralType.A, new ShaderFlavor(0.8f, (0.9f, 0.9f, 1.0f)) },
            { SpectralType.F, new ShaderFlavor(0.7f, (1.0f, 1.0f, 1.0f)) },
            { SpectralType.G, new ShaderFlavor(0.6f, (1.0f, 1.0f, 0.9f)) },
            { SpectralType.K, new ShaderFlavor(0.5f, (1.0f, 0.9f, 0.7f)) },
            { SpectralType.M, new ShaderFlavor(0.5f, (1.0f, 0.4f, 0.4f)) },
            { SpectralType.Unknown, new ShaderFlavor(0.3f, (1.0f, 1.0f, 1.0f)) }, // Default for unknown types
        };
    }

    public static ShaderFlavor GetShaderFlavor(SpectralType type)
    {
        if (_shaders.TryGetValue(type, out ShaderFlavor flavor))
        {
            return flavor;
        }
        return _shaders[SpectralType.Unknown]; // Return a default value if the type is not found
    }
}