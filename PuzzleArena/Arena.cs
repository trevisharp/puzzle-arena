using Radiance;
using Radiance.Primitives;
using static Radiance.Utils;

namespace PuzzleArena;

public class Arena(Level level, Controller controller)
{
    int currentUniverse = 1;
    float playerXPos = level.StartX;
    float playerYPos = level.StartY;

    public void Start()
    {
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
            zoom(size);
            move(px, py);
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
            
        };

        Window.OnRender += () =>
        {
            foreach (var tile in level.Tiles)
            {
                Vec4 color = (tile.IsGoal, tile.IsWall, tile.IsPortal, tile.PortalUniverse) switch
                {
                    (true, _, _, _) => green,
                    (_, true, _, _) => vec(0.4f, 0.2f, 0, 1),
                    _ => blue
                };

                tileRender(
                    offsetX + tile.X * sizeFactor,
                    offsetY + tile.Y * sizeFactor,
                    sizeFactor,
                    color
                );
            }
        };

        Window.CloseOn(Input.Escape);
        Window.Open();
    }
}