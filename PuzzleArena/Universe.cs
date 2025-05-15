using System.Collections.Generic;

namespace PuzzleArena;

using Universes;

public abstract class Universe
{
    public abstract int Id { get; }

    public abstract (int nextX, int nextY) GetPlayerTarget(
        int currX, int currY, MoveType move, Tile[] tiles
    );

    public static Dictionary<int, Universe> All => new() {
        [1] = new NormalUniverse(),
    };
}