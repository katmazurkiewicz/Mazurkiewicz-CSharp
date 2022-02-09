using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace MemoryGame
{
    class HiScore
    {
      List<string[]> scoretable;
      public HiScore(List<string[]> _scoretable){_scoretable=scoretable;}
      public void HiScoreCheck(int numW, List<string[]> scoretable, string[] currentScore)
        {
          HiScoreLoad(numW,scoretable);
          CompareScore(scoretable,currentScore);
          WriteScore(numW,scoretable);
        }
      public void HiScoreDisplay(int numW, List<string[]> scoretable)
      {
        HiScoreLoad(numW,scoretable);
        HiScorePrint(scoretable);
      }
      void HiScoreLoad(int numWords, List<string[]> sTable)
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
          sTable.Add(data); //System.NullReferenceException” w task-solution.dll: 'Object reference not set to an instance of an object.'
          }
      }
      void CompareScore(List<string[]> list, string[] newentry)
      {
        for (int i=0; i<10; i++)
        {
          if(Convert.ToInt32(newentry[3]) < Convert.ToInt32(list[i][3]) || (newentry[3] == list[i][3] && ((Convert.ToInt32(newentry[2]) < Convert.ToInt32(list[i][2]) ) ) ) )
          { 
            Console.WriteLine("Congratulations, you've established a high score.");
            Console.WriteLine("Please enter your name - max. 24 characters:");
            newentry[0] = Console.ReadLine();
            while(newentry[0].Length>24)
            {
              Console.WriteLine("Name too long. Please enter shorter name");
              newentry[0] = Console.ReadLine();
              Console.Clear();
            }
            list.Insert(i, newentry);
            break;
          }
        }
      }
      void WriteScore(int numWords, List<string[]> list)
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
        File.WriteAllText(path, list[0][0] + "|" + list[0][1] + "|" + list[0][2] + "|" + list[0][3] + Environment.NewLine);
        for(int i=0; i<9; i++)
        {
          File.AppendAllText(path, list[i+1][0] + "|" + list[i+1][1] + "|" + list[i+1][2] + "|" + list[i+1][3] + Environment.NewLine);
        }
      }
      void HiScorePrint(List<string[]> sTable)
      {
        string pad = new string(' ', 21);
        Console.WriteLine(" NAME" + pad + "| DATE       | TIME(S) | TRIES");
        for(int i=0; i<10; i++)
        {
          if(Convert.ToInt32(sTable[i][3])>15) // this would become obsolete with time, but no need to write out all the placeholders
          {
            break;
          }
          Console.WriteLine(" " + sTable[i][0].PadRight(25) + "| " + sTable[i][1] + " | " + sTable[i][2].PadRight(9) + "| " + sTable[i][3]);
        }
      }
    }
}
