using System;
using System.Collections.Generic;

namespace PuzzleArena;

public class Level
{
    public required int StartX { get; init; }
    public required int StartY { get; init; }
    public required int Width { get; init; }
    public required int Height { get; init; }
    public required Tile[] Tiles { get; init; }

    public Tile? this[int x, int y] => 
        x < 0 || x >= Width || y < 0 || y >= Height ?
        null : Tiles[x + y * Width];

    public static Level FromTiles(int width, params TileType[] types)
    {
        if (types.Length % width != 0)
            throw new Exception("The number of tiles may be multiple of the width.");
        
        List<TileType> rounded = [];
        for (int i = 0; i < width + 2; i++)
            rounded.Add(TileType.Wall);
        
        for (int i = 0; i < types.Length; i++)
        {
            if (i % width == 0)
                rounded.Add(TileType.Wall);
            
            rounded.Add(types[i]);
            
            if (i % width == width - 1)
                rounded.Add(TileType.Wall);
        }

        for (int i = 0; i < width + 2; i++)
            rounded.Add(TileType.Wall);
        
        width += 2;
        types = [ ..rounded ];
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
        1 => FromTiles(4, 
            TileType.Player, TileType.Path, TileType.Path, TileType.Goal
        ),

        2 => FromTiles(4,
            TileType.Player, TileType.Path, TileType.Wall, TileType.Wall,
            TileType.Wall, TileType.Path, TileType.Wall, TileType.Wall,
            TileType.Wall, TileType.Path, TileType.Path, TileType.Goal
        ),

        3 => FromTiles(4,
            TileType.Wall, TileType.Path, TileType.Path, TileType.Goal,
            TileType.Wall, TileType.Path, TileType.Wall, TileType.Wall,
            TileType.GravityPortal, TileType.Player, TileType.Wall, TileType.Wall,
            TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall
        ),

        4 => FromTiles(3,
            TileType.Goal, TileType.Wall, TileType.Wall,
            TileType.Path, TileType.Player, TileType.Path,
            TileType.Wall, TileType.Wall, TileType.Path,
            TileType.Wall, TileType.GravityPortal, TileType.Path
        ),

        _ => throw new Exception($"Unknown level {level}.")
    };
}