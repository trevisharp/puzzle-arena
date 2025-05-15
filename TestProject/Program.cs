using PuzzleArena;
using PuzzleArena.Controllers;

var arena = new Arena(Level.Get(1), new HumanController());
arena.Start();