using System;

namespace PuzzleArena;

public class Level
{
    public required int StartX { get; init; }
    public required int StartY { get; init; }
    public required int Width { get; init; }
    public required int Height { get; init; }
    public required Tile[] Tiles { get; init; }

    public static Level FromTiles(int width, params TileType[] types)
    {
        if (types.Length % width != 0)
            throw new Exception("The number of tiles may be multiple of the width.");
        int height = types.Length / width;

        int index = 0;
        var tiles = new Tile[types.Length];

        int startx = 0, starty = 0;

        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                var type = types[index];
                if (type == TileType.Player)
                {
                    startx = i;
                    starty = j;
                }
                tiles[index] = Tile.FromType(type, i, j);
                index++;
            }
        }

        return new Level {
            Height = height,
            Width = width,
            StartX = startx,
            StartY = starty,
            Tiles = tiles
        };
    }

    public static Level Get(int level) => level switch
    {
        1 => FromTiles(6, 
            TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall,
            TileType.Wall, TileType.Player, TileType.Path, TileType.Path, TileType.Goal, TileType.Wall,
            TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall
        ),

        _ => throw new Exception($"Unknown level {level}.")
    };
}