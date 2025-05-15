using Radiance;

namespace PuzzleArena;

public class HumanController : Controller
{
    bool Apressed = false;
    bool DPressed = false;
    public HumanController()
    {
        Window.OnKeyDown += (key, mod) =>
        {
            Apressed |= key == Input.A;
            DPressed |= key == Input.D;
        };
        
        Window.OnKeyUp += (key, mod) =>
        {
            Apressed &= key != Input.A;
            DPressed &= key != Input.D;
        };
    }

    public override MoveType Move()
        => (Apressed, DPressed) switch
        {
            (true, false) => MoveType.Left,
            (false, true) => MoveType.Right,
            _ => MoveType.None
        };
}