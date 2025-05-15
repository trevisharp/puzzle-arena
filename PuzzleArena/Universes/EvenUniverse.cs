using System;

namespace PuzzleArena.Universes;

public class EvenUniverse : Universe
{
    public override int Id => 3;

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
        
        var left = level[currX - 2, currY];
        if (left is not null && !left.IsWall && move == MoveType.Left)
            return (currX - 2, currY);
        
        var right = level[currX + 2, currY];
        if (right is not null && !right.IsWall && move == MoveType.Right)
            return (currX + 2, currY);
        
        return (currX, currY);
    }
}