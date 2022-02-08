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
            Console.WriteLine("Please choose the difficulty level - press a key: (E)asy or (H)ard");
            string gameMode = Console.ReadLine(); // game mode input
            Game game = new Game(wordbase);
            while(gameMode != "e" && gameMode != "E" && gameMode != "h" && gameMode != "H")
            {
                Console.WriteLine("Sorry, not a valid option. Please press E for easy mode, H for hard mode");
                gameMode=Console.ReadLine();
            }
            if(gameMode == "e" || gameMode == "E") {game.playGame(4,10);}
            if(gameMode == "h" || gameMode == "H") {game.playGame(8,15);}          
        }
    }
}