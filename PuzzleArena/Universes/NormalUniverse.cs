namespace PuzzleArena.Universes;

public class NormalUniverse : Universe
{
    public override int Id => 1;

    public override (int nextX, int nextY) GetPlayerTarget(int currX, int currY, MoveType move, Level level)
    {
        if (move == MoveType.Return)
            return (level.StartX, level.StartY);
        
        var under = level[currX, currY + 1];
        bool hasGround = under is null || under.IsWall;

        if (hasGround && move == MoveType.None)
            return (currX, currY);
        
        if (!hasGround)
            return (currX, currY + 1);
        
        var left = level[currX - 1, currY];
        if (left is not null && !left.IsWall && move == MoveType.Left)
            return (currX - 1, currY);
        
        var right = level[currX + 1, currY];
        if (right is not null && !right.IsWall && move == MoveType.Right)
            return (currX + 1, currY);
        
        return (currX, currY);
    }
}