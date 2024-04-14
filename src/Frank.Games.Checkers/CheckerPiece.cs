namespace Frank.Games.Common;

public class CheckerPiece
{
    public PieceColor Color { get; private set; }
    public PieceType Type { get; set; } // Can be changed to King in-game

    public CheckerPiece(PieceColor color)
    {
        Color = color;
        Type = PieceType.Normal;
    }
}