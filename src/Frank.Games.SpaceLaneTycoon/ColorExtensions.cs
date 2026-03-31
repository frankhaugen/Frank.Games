using Raylib_cs;

public static class ColorExtensions
{
    public static Color ToRaylibColor(this System.Drawing.Color color)
    {
        return new Color(color.R, color.G, color.B, color.A);
    }
}