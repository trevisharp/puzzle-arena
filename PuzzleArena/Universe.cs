using System.Collections.Generic;

namespace PuzzleArena;

using Universes;

public abstract class Universe
{
    public abstract int Id { get; }

    public abstract (int nextX, int nextY) GetPlayerTarget(
        int currX, int currY, MoveType move, Level level
    );

    public static Dictionary<int, Universe> All => new() {
        [1] = new NormalUniverse(),
        [2] = new GravityUniverse()
    };
}