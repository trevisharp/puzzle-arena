using System;

namespace PuzzleArena;

public class Tile
{
    public required int X { get; init; }
    public required int Y { get; init; }

    public required bool IsWall { get; init; }
    public required bool IsGoal { get; init; }

    public required bool IsPortal { get; init; }
    public required int PortalUniverse { get; init; }

    public static Tile FromType(TileType type, int x, int y)
    {
        return type switch
        {
            TileType.Path => new Tile
            {
                X = x,
                Y = y,
                IsWall = false,
                IsPortal = false,
                PortalUniverse = -1,
                IsGoal = false
            },
            TileType.Wall => new Tile
            {
                X = x,
                Y = y,
                IsWall = true,
                IsPortal = false,
                PortalUniverse = -1,
                IsGoal = false
            },
            TileType.Player => new Tile
            {
                X = x,
                Y = y,
                IsWall = false,
                IsPortal = false,
                PortalUniverse = -1,
                IsGoal = false
            },
            TileType.Goal => new Tile
            {
                X = x,
                Y = y,
                IsWall = false,
                IsPortal = false,
                PortalUniverse = -1,
                IsGoal = true
            },
            TileType.NormalPortal => new Tile
            {
                X = x,
                Y = y,
                IsWall = false,
                IsPortal = true,
                PortalUniverse = 1,
                IsGoal = false
            },
            TileType.GravityPortal => new Tile
            {
                X = x,
                Y = y,
                IsWall = false,
                IsPortal = true,
                PortalUniverse = 2,
                IsGoal = false
            },
            TileType.EvenPortal => new Tile
            {
                X = x,
                Y = y,
                IsWall = false,
                IsPortal = true,
                PortalUniverse = 3,
                IsGoal = false
            },
            TileType.SpacePortal => new Tile
            {
                X = x,
                Y = y,
                IsWall = false,
                IsPortal = true,
                PortalUniverse = 4,
                IsGoal = false
            },
            _ => throw new Exception($"Unknow tile type {type}.")
        };
    }
}