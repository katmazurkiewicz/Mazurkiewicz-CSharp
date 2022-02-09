// ** TASK **
/* load text file
easy mode vs hard mode - ask
easy: 4 RANDOM word pairs, 10 chances
hard: 8 pairs, 15 chances
display mode, chances left, covered matrix
prompt for coordinates, display word x2
if words match, keep 'em displayed
add a question if user wants to restart
-OPTIONAL add info about chances and guess time at the end (i.e. "You solved the memory game after 8 chances.
It took you 240 seconds").
-OPTIONAL Add a high score - some people take pride in their score. At the end
of a successful game program should ask the user for his/her name and save
that information to a file - name| date | guessing_time | guessing_tries |
-OPTIONAL Expand high score - program should remember 10 best scores (read
from and write to file) and display them at the end, after success/failure.
-OPTIONAL Beautify your game! Add ASCII art, improve word matrix, or anything
to make your program more appealing to the player!
-please add to your Readme.MD just a few sentences on why do
you code - is it just a professional matter or something more? We would like to know
the answer!

*/
using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace MemoryGame
{
  class Program
  {

    static void Main(string[] args)
    {
      Console.SetWindowSize(80, 40);
      // 1)load word base from file, has to still work after repository is downloaded elsewhere
      string path = Directory.GetCurrentDirectory() + @"\Words.txt";
      List<string> WordBase = File.ReadAllLines(path).ToList();
      Console.SetCursorPosition(23,20);
      Console.WriteLine("Welcome to my little memory game!" + Environment.NewLine);  
      //Console.SetCursorPosition(0, Console.CursorTop - 1
      Console.SetCursorPosition(29,Console.CursorTop);
      Console.WriteLine("Press any key to start");
      Console.SetCursorPosition(40,Console.CursorTop);
      Console.ReadKey();    
       while (true)
       {
         Playthrough playthrough = new Playthrough(WordBase);
         playthrough.SinglePlaythrough(WordBase);
         Console.SetCursorPosition(23,20);
         Console.WriteLine("Would you like to play again? (Y/N)");
         Console.SetCursorPosition(40,Console.CursorTop);
         ConsoleKeyInfo pressed = Console.ReadKey();
         string Decision = pressed.KeyChar.ToString();
         while(Decision != "n" && Decision != "N" && Decision != "y" && Decision != "Y")
         {
           Console.Clear();
           Console.SetCursorPosition(20,20);
           Console.WriteLine("Not a valid answer, please press Y or N.");
           Console.SetCursorPosition(40,Console.CursorTop);
           pressed = Console.ReadKey();
          Decision = pressed.KeyChar.ToString();
         }
         Console.Clear();
         if(Decision == "n" || Decision == "N")
         {
           Console.SetCursorPosition(17,20);
           Console.WriteLine("Thank you for playing, see you again sometime!");
           Thread.Sleep(5000);
           break;
         }
       }
    }
  }
}