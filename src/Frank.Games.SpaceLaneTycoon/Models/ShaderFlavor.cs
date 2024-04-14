namespace Frank.Games.SpaceLaneTycoon;

public struct ShaderFlavor
{
    public float PointSize { get; set; }
    public (float R, float G, float B) Color { get; set; }

    public ShaderFlavor(float pointSize, (float R, float G, float B) color)
    {
        PointSize = pointSize;
        Color = color;
    }
    
    public string ToShaderString()
    {
        return $"vec4({Color.R}, {Color.G}, {Color.B}, {PointSize})";
    }
}