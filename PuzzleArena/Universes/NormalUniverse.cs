namespace PuzzleArena.Universes;

public class NormalUniverse : Universe
{
    public override int Id => 1;

    public override (int nextX, int nextY) GetPlayerTarget(int currX, int currY, MoveType move, Tile[] tiles)
    {
        throw new System.NotImplementedException();
    }
}