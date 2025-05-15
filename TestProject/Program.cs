using PuzzleArena;
using PuzzleArena.Controllers;

var arena = new Arena(
    Level.Get(12), 
    new HumanController()
);
arena.Start();