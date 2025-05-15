using Radiance;

namespace PuzzleArena.Controllers;

public class HumanController : Controller
{
    bool Apressed = false;
    bool DPressed = false;
    bool SpacePressed = false;
    public HumanController()
    {
        Window.OnKeyDown += (key, mod) =>
        {
            Apressed |= key == Input.A;
            DPressed |= key == Input.D;
            SpacePressed |= key == Input.Space;
        };
        
        Window.OnKeyUp += (key, mod) =>
        {
            Apressed &= key != Input.A;
            DPressed &= key != Input.D;
            SpacePressed &= key != Input.Space;
        };
    }

    public override MoveType Move()
        => (Apressed, DPressed, SpacePressed) switch
        {
            (_, _, true) => MoveType.Return,
            (true, false, _) => MoveType.Left,
            (false, true, _) => MoveType.Right,
            _ => MoveType.None
        };
}