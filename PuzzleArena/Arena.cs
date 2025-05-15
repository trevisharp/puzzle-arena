using Radiance;
using static Radiance.Utils;

namespace PuzzleArena;

public class Arena(Level level, Controller controller)
{
    int currentUniverse = 1;
    float playerXPos = level.StartX;
    float playerYPos = level.StartY;

    public void Start()
    {
        Window.OnFrame += () =>
        {
            
        };

        Window.OnRender += () =>
        {

        };

        Window.CloseOn(Input.Escape);
        Window.Open();
    }
}