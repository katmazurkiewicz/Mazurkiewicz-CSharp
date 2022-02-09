using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace MemoryGame
{
    class HiScore
    {
      List<string[]> scoretable = new List<string[]> {};
      public HiScore(){ }
      public void HiScoreCheck(int numW, string[] currentScore)
        {
          HiScoreLoad(numW);
          CompareScore(currentScore);
          WriteScore(numW);
        }
      public void HiScoreDisplay(int numW)
      {
        HiScoreLoad(numW);
        HiScorePrint();
      }
      void HiScoreLoad(int numWords)
      {
        string score_path = Directory.GetCurrentDirectory();
        if (numWords == 4)
        {
          score_path += @"\HiScoreEasy.txt";
        }          
        if (numWords == 8)
        {
          score_path += @"\HiScoreHard.txt";
        }
          foreach (string entry in System.IO.File.ReadLines(score_path))
          {
          string[] data = entry.Split('|');
          scoretable.Add(data);
          }
      }
      void CompareScore(string[] newentry)
      {
        for (int i=0; i<10; i++)
        {
          if(Convert.ToInt32(newentry[3]) < Convert.ToInt32(scoretable[i][3]) || (newentry[3] == scoretable[i][3] && ((Convert.ToInt32(newentry[2]) < Convert.ToInt32(scoretable[i][2]) ) ) ) )
          { 
            Console.Clear();
            Console.SetCursorPosition(16,20);
            Console.WriteLine("Congratulations, you've established a high score.");
            Console.SetCursorPosition(18,Console.CursorTop);
            Console.WriteLine("Please enter your name - max. 24 characters:"); //very much an arbitrary value
            Console.SetCursorPosition(40,Console.CursorTop);
            newentry[0] = Console.ReadLine();
            Console.Clear();
            while(newentry[0].Length>24)
            {
              Console.SetCursorPosition(20,20);
              Console.WriteLine("Name too long. Please enter shorter name");
              Console.SetCursorPosition(40,Console.CursorTop);
              newentry[0] = Console.ReadLine();
              Console.Clear();
            }
            scoretable.Insert(i, newentry);
            break;
          }
        }
      }
      void WriteScore(int numWords)
      {
        string path = Directory.GetCurrentDirectory();
        if (numWords == 4)
        {
          path += @"\HiScoreEasy.txt";
        }          
        if (numWords == 8)
        {
          path += @"\HiScoreHard.txt";
        }
        File.WriteAllText(path, scoretable[0][0] + "|" + scoretable[0][1] + "|" + scoretable[0][2] + "|" + scoretable[0][3] + Environment.NewLine);
        for(int i=0; i<9; i++)
        {
          File.AppendAllText(path, scoretable[i+1][0] + "|" + scoretable[i+1][1] + "|" + scoretable[i+1][2] + "|" + scoretable[i+1][3] + Environment.NewLine);
        }
      }
      void HiScorePrint()
      {
        string pad = new string(' ', 21);
        string pleft = new string(' ', 12);
        Console.SetCursorPosition(11,10);
        Console.WriteLine(" NAME" + pad + "| DATE       | TIME(S) | TRIES");
        Console.SetCursorPosition(11,Console.CursorTop);
        Console.WriteLine("__________________________|____________|_________|_______");
        string gap = "                         |            |         |       ";
        for(int i=0; i<10; i++)
        {
          if(Convert.ToInt32(scoretable[i][3])>15) // this would become obsolete with time, but for now there's no need to write out all the placeholders
          {
            break;
          }
          Console.WriteLine(pleft + gap);
          Console.WriteLine(pleft + scoretable[i][0].PadRight(25) + "| " + scoretable[i][1] + " | " + scoretable[i][2].PadRight(8) + "| " + scoretable[i][3]);
        }
        Console.WriteLine();
      }
    }
}
