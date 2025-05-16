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

    public static Level FromString(params string[] map)
    {
        int width = map[0].Length;
        List<TileType> tileTypes = [];

        foreach (var line in map)
        {
            foreach (var tile in line)
            {
                tileTypes.Add(tile switch
                {
                    'P' => TileType.Player,
                    'W' => TileType.Wall,
                    'G' => TileType.Goal,
                    '1' => TileType.NormalPortal,
                    '2' => TileType.GravityPortal,
                    '3' => TileType.EvenPortal,
                    '4' => TileType.SpacePortal,
                    _ => TileType.Path,
                });
            }
        }


        return FromTiles(width, [ ..tileTypes]);
    }

    public static Level Get(int level) => level switch
    {
        1 => FromString(
            "P  G"
        ),

        2 => FromString(
            "P WW",
            "W WW",
            "W  G"
        ),

        3 => FromString(
            "W  G",
            "W WW",
            "2PWW"
        ),

        4 => FromString(
            "GWW",
            " P ",
            "WW ",
            "W2 "
        ),

        5 => FromString(
            "3P WG"
        ),

        6 => FromString(
            "3P  WG"
        ),

        7 => FromString(
            "WWW W",
            "3P 2G"
        ),

        8 => FromString(
            "2P 1WW",
            "WW  WW",
            "WW  WW",
            "WW33 G"
        ),

        9 => FromString(
            "WWW    ",
            "4 P   G",
            "WWW    "
        ),

        10 => FromString(
            "WWWW    1",
            "124P   3G",
            "WWWW   WW"
        ),

        11 => FromString(
            "WWWW4WW   ",
            "2W    P  G",
            "WWWWWWW  3"
        ),

        12 => FromString(
            "WW1W  3",
            "G  P   ",
            "WW2W   ",
            "WWWW   ",
            "4W W   "
        ),

        _ => throw new Exception($"Unknown level {level}.")
    };
}