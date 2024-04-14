using System.Drawing;
using System.Numerics;

namespace Frank.Games.SpaceLaneTycoon.Mapping;

public class RenderableStar
{
    public int Id { get; set; }
    public Vector3 Postition { get; set; }
    public string? Name { get; set; }
    public SpectralClassification SpectralClassification { get; set; }
    public Color Color { get; set; }
    public float Radius { get; set; }
    public float Temperature { get; set; }
    public float Luminosity { get; set; }
    public float Mass { get; set; }

    public string GetFragmentShader()
    {
        var shaderFlavor = SpectralShaderDictionary.GetShaderFlavor(SpectralClassification.Type);
        var shader = $$"""
              #version 330 core
              out vec4 FragColor;
              void main()
              {
                  FragColor = {{shaderFlavor.ToShaderString()}};
              }
              """;
        // Console.WriteLine(shader);
        return shader;
    }
    
    public string GetVertexShader()
    {
        var shader = $$"""
                 #version 330 core
                 void main()
                 {
                     gl_Position = vec4({{Postition.X}}, {{Postition.Y}}, {{Postition.Z}}, 1.0);
                 }
                 """;
        // Console.WriteLine(shader);
        return shader;
    }
}