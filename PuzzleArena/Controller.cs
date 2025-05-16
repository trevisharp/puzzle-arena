namespace PuzzleArena;

public abstract class Controller
{
    public abstract MoveType Move(
        Level level,
        int playerX,
        int playerY,
        int universe
    );
}