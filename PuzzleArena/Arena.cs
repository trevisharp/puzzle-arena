using Radiance;
using Radiance.Primitives;
using static Radiance.Utils;

namespace PuzzleArena;

public class Arena(Level level, Controller controller)
{
    int currentUniverse = 1;
    int playerXPos = level.StartX;
    int playerYPos = level.StartY;
    int nextPlayerXPos = level.StartX;
    int nextPlayerYPos = level.StartY;
    float animationTime = 0f;

    public void Start()
    {
        var universes = Universe.All;
        var tileRender = render((val px, val py, val size, vec4 clr) => 
        {
            zoom(size);
            move(px, py);
            color = clr;
            fill();

            move(0, 0, 1);
            color = black;
            draw(4f);
        })(Polygons.Square);
        
        var playerRender = render((val px, val py, val size) => 
        {
            zoom(size * (0.8 + sin(3 * t) / 10));
            move(px, py, 3);
            color = mix(
                vec(0f, 0.8f, 0f, 1f),
                vec(0.4f, 1f, 0.4f, 1f),
                sin(t)
            );
            fill();

            move(0, 0, 1);
            color = black;
            draw(4f);
        })(Polygons.Square);

        float sizeFactor = 0;
        float offsetX = 0;
        float offsetY = 0;

        Window.OnLoad += () =>
        {
            float xSizeFactor = Window.Width / (float)level.Width;
            float ySizeFactor = Window.Height / (float)level.Height;

            if (xSizeFactor < ySizeFactor)
            {
                sizeFactor = xSizeFactor;
                offsetX = 0;
                offsetY = (Window.Height - level.Height * sizeFactor) / 2;
            }
            else
            {
                sizeFactor = ySizeFactor;
                offsetY = 0;
                offsetX = (Window.Width - level.Width * sizeFactor) / 2;
            }
        };

        Window.OnFrame += () =>
        {
            animationTime += 5 * Window.DeltaTime;
            if (animationTime < 1f)
                return;
            
            playerXPos = nextPlayerXPos;
            playerYPos = nextPlayerYPos;
            animationTime = 0;
            
            var currentTile = level[playerXPos, playerYPos];
            if (currentTile is not null && currentTile.IsPortal)
                currentUniverse = currentTile.PortalUniverse;

            var move = controller.Move(
                level,
                playerXPos,
                playerYPos,
                currentUniverse 
            );
            var universe = universes[currentUniverse];
            (nextPlayerXPos, nextPlayerYPos) = universe
                .GetPlayerTarget(playerXPos, playerYPos, move, level
            );
        };

        Window.OnRender += () =>
        {
            foreach (var tile in level.Tiles)
            {
                Vec4 color = (tile.IsGoal, tile.IsWall, tile.IsPortal, tile.PortalUniverse) switch
                {
                    (true, _, _, _) => green,
                    (_, true, _, _) => vec(0.4f, 0.2f, 0, 1),
                    (_, _, true, 1) => blue,
                    (_, _, true, 2) => vec(0f, 0f, 0.3f, 1f),
                    (_, _, true, 3) => vec(1f, 1f, 0.4f, 1f),
                    (_, _, true, 4) => vec(0.1f, 0.1f, 0.1f, 1f),
                    _ when currentUniverse == 1 => blue,
                    _ when currentUniverse == 2 => vec(0f, 0f, 0.3f, 1f),
                    _ when currentUniverse == 3 => vec(1f, 1f, 0.4f, 1f),
                    _ when currentUniverse == 4 => vec(0.1f, 0.1f, 0.1f, 1f),
                    _ => white
                };

                if (tile.X == playerXPos && tile.Y == playerYPos)
                {
                    var x = playerXPos * (1f - animationTime) + nextPlayerXPos * animationTime;
                    var y = playerYPos * (1f - animationTime) + nextPlayerYPos * animationTime;
                    playerRender(
                        offsetX + x * sizeFactor + sizeFactor / 2,
                        Window.Height - (offsetY + y * sizeFactor + sizeFactor / 2),
                        sizeFactor
                    );
                }

                tileRender(
                    offsetX + tile.X * sizeFactor + sizeFactor / 2,
                    Window.Height - (offsetY + tile.Y * sizeFactor + sizeFactor / 2),
                    sizeFactor,
                    color
                );
            }
        };

        Window.CloseOn(Input.Escape);
        Window.Open();
    }
}