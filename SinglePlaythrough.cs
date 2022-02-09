using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace MemoryGame
{
    class Playthrough
    {
        List<string> wordbase;
        public Playthrough(List<string> _wordbase){wordbase=_wordbase;}
        public void SinglePlaythrough(List<string> wordbase)
        {   
            Console.Clear();
            Console.SetCursorPosition(23,20);
            Console.WriteLine("Please choose the difficulty level");
            Console.SetCursorPosition(25,21);
            Console.WriteLine ("- press a key: (E)asy or (H)ard");
            Console.SetCursorPosition(40,Console.CursorTop);
            ConsoleKeyInfo pressed = Console.ReadKey();
            string gameMode = pressed.KeyChar.ToString(); // game mode input
            Console.Clear();
            Game game = new Game(wordbase);
            while(gameMode != "e" && gameMode != "E" && gameMode != "h" && gameMode != "H")
            {
                Console.SetCursorPosition(27,20);
                Console.WriteLine("Sorry, not a valid option.");
                Console.SetCursorPosition(17,21);
                Console.WriteLine ("Please press E for easy mode, H for hard mode.");
                Console.SetCursorPosition(40,Console.CursorTop);
                pressed = Console.ReadKey();
                gameMode = pressed.KeyChar.ToString();
                Console.Clear();
            }
            if(gameMode == "e" || gameMode == "E") {game.playGame(4,10);}
            if(gameMode == "h" || gameMode == "H") {game.playGame(8,15);}          
        }
    }
}