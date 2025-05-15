using PuzzleArena;
using PuzzleArena.Controllers;

var arena = new Arena(
    Level.Get(3), 
    new HumanController()
);
arena.Start();