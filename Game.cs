using System;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace MemoryGame
{
class Game
{
    List<string> wordbase;
    public Game(List<string> _wordbase){wordbase=_wordbase;}
    public void playGame(int numWords, int numTries)
    {   
        Console.Clear();
        int numRows=numWords/2;
        string[,] board = new string[numRows,4], board_display = new string[numRows,4];
        Populate(board, wordbase);
        SetUp(board_display);              
        HiScore hiscore = new HiScore();
        Stopwatch timer = new Stopwatch();
        timer.Start();
        for(int i=0;i<numTries;i++)
        {
            PrintBoard(board_display, numTries-i);
            Uncover(board_display, board, numTries-i);
            if(BComplete(board_display))
            {
                timer.Stop();
                long time = timer.ElapsedMilliseconds/1000;
                PrintBoard(board_display, 15-i);
                Console.Clear();
                Console.SetCursorPosition(17,19);
                Console.WriteLine("Congratulations! You won! It took you {0} tries.",(i+1));
                Console.SetCursorPosition(24,Console.CursorTop);
                Console.WriteLine("The time elapsed is {0} seconds.",time);
                Console.SetCursorPosition(28,Console.CursorTop);
                Console.WriteLine("Press any key to continue");
                Console.SetCursorPosition(40,Console.CursorTop);
                Console.ReadKey();
                Console.Clear();
                string[] score = new string[4];
                score[1] = DateTime.Now.ToString("dd/MM/yyyy");
                score[2] = time.ToString();
                score[3] = (i+1).ToString();
                hiscore.HiScoreCheck(numWords, score);  
                break;
            }
        }
        if(!BComplete(board_display))
        {
            Console.SetCursorPosition(18,20);
            Console.WriteLine("Unfortunately, you didn't make it this time.");
            Thread.Sleep(3000);
        }
        Console.Clear();
        Console.SetCursorPosition(12,20);
        Console.WriteLine("Here is the current high score table for this game mode:");
        Thread.Sleep(3000);
        Console.Clear();
        hiscore.HiScoreDisplay(numWords);
        Console.WriteLine(Environment.NewLine);
        Console.SetCursorPosition(24,Console.CursorTop + 1);
        Console.WriteLine("Press any key to close the table.");
        Console.SetCursorPosition(40,Console.CursorTop + 1);
        Console.ReadKey();
        Console.Clear();
    }
     void Populate(string[,] arr, List<string> wordbase)
        {   
             List<int> FreeSpaces = new List<int>();
            for(int i=0; i<arr.Length; i++)
            {
                FreeSpaces.Add(i);
            }
            Random random = new Random();
            string element;
            for(int i=0; i<arr.Length/2; i++)
            {   
                int w = random.Next(0,wordbase.Count);
                element = wordbase[w]; 
                wordbase.RemoveAt(w); 
                for(int j=0; j<2; j++)
                {         
                    int a = random.Next(0,FreeSpaces.Count);
                    int e = FreeSpaces[a];
                    double ed = Convert.ToDouble(e);
                    int x = Convert.ToInt32(Math.Floor(ed/4));
                    int y = e%4;
                    arr[x,y] = element;
                    FreeSpaces.RemoveAt(a);
                } 
            }
        }
        void SetUp(string[,] arr_display)
            {
                for(int i=0; i<arr_display.Length; i++)
                {
                    double id = Convert.ToDouble (i);
                    int x = Convert.ToInt32(Math.Floor(id/4));
                    int y = i%4;
                    arr_display[x,y] = "X";
                }
            }
        void PrintBoard(string[,] arr_display, int x)
        {    
            //we'll use it before the Uncover function to print current state of board
            //let's try and space the words out neatly - longest word in our base is 13 chars long
            string pad = new String(' ', 13);
            int h_diff = (arr_display.Length-8)/4;
            Console.SetCursorPosition(34,(16-h_diff));
            Console.WriteLine(x + " tries left" + Environment.NewLine);
            Console.WriteLine("         \t1" + pad + "2" + pad + "3" + pad + "4" + Environment.NewLine);
            for(int i=0; i<arr_display.GetLength(0); i++)
            {
                Console.Write("        " + ((char)(i+65))+ "\t");
                for(int j=0; j<arr_display.GetLength(1); j++)
                {
                    Console.Write(arr_display[i,j].PadRight(14));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }
        void Uncover(string[,] arr_display, string[,] arr, int t)
        {   
            string pad = new String(' ', 13);
            int bsize=arr_display.Length;
            Console.SetCursorPosition(13,Console.CursorTop + 2);
            Console.WriteLine("Please input coordinates of the first word to uncover:");
            Console.SetCursorPosition(40,Console.CursorTop + 1);
            string coord1 = Console.ReadLine();
            while(!CoordCheck(coord1,bsize) || arr_display[NumCoord(coord1).Item1,NumCoord(coord1).Item2] != "X")
            {
                Console.SetCursorPosition(22,Console.CursorTop + 1);
                Console.WriteLine("Not a valid coordinate. Please retry.");
                Console.SetCursorPosition(40,Console.CursorTop + 1);
                coord1 = Console.ReadLine();
            }
            Tuple<int, int> numc1 = NumCoord(coord1);           
            Console.Clear();
            int h_diff = (bsize-8)/4;
            Console.SetCursorPosition(34,(16-h_diff));
            Console.WriteLine( t + " tries left" + Environment.NewLine);
            Console.WriteLine("         \t1" + pad + "2" + pad + "3" + pad + "4" + Environment.NewLine);
            for(int i=0; i<arr_display.GetLength(0); i++)
            {
                Console.Write("        " + ((char)(i+65))+ "\t");
                for(int j=0; j<arr_display.GetLength(1); j++)
                {
                    
                    if(i==numc1.Item1 && j==numc1.Item2)
                    {
                        Console.Write(arr[i,j].PadRight(14));
                    }
                    else Console.Write(arr_display[i,j].PadRight(14));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.SetCursorPosition(18,Console.CursorTop + 1);
            Console.WriteLine("Please input coordinates of the second word:");
            Console.SetCursorPosition(40,Console.CursorTop + 1);
            string coord2 = Console.ReadLine();
            while(!CoordCheck(coord2,bsize) || arr_display[NumCoord(coord2).Item1,NumCoord(coord2).Item2]!="X" || coord2==coord1)
            {
                Console.SetCursorPosition(22,Console.CursorTop + 1);
                Console.WriteLine("Not a valid coordinate. Please retry.");
                Console.SetCursorPosition(40,Console.CursorTop + 1);
                coord2 = Console.ReadLine();
            }
            Tuple<int, int> numc2 = NumCoord(coord2);
            Console.Clear();
            Console.SetCursorPosition(34,(16-h_diff));
            Console.WriteLine(t + " tries left" + Environment.NewLine);
            Console.WriteLine("         \t1" + pad + "2" + pad + "3" + pad + "4" + Environment.NewLine);
            for(int i=0; i<arr_display.GetLength(0); i++)
            {
                Console.Write("        " + ((char)(i+65)) + "\t");
                for(int j=0; j<arr_display.GetLength(1); j++)
                {
                    if((i==numc1.Item1 && j==numc1.Item2) || (i==numc2.Item1 && j==numc2.Item2))
                    {
                        Console.Write(arr[i,j].PadRight(14));
                    }
                    else Console.Write(arr_display[i,j].PadRight(14));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.WriteLine();
            if (arr[numc1.Item1, numc1.Item2] == arr[numc2.Item1, numc2.Item2])
                {
                    arr_display[numc1.Item1, numc1.Item2] = arr[numc1.Item1, numc1.Item2];
                    arr_display[numc2.Item1, numc2.Item2] = arr[numc1.Item1, numc1.Item2];
                }
            else {Thread.Sleep(4000);}
            Console.Clear();
        }

        bool CoordCheck(string c, int b) // for given board size
        {   
                if(c.Length!=2) return false;
                char c0=c[0];
                char c1=c[1];
                if (!Char.IsLetter(c0) || !Char.IsDigit(c1)) return false;
                else if (Convert.ToInt32(c0)-65<0 || Convert.ToInt32(c0)-65>(b/4-1)) return false;
                else if (int.Parse(c1.ToString())<0 || int.Parse(c1.ToString())>4) return false;
                else return true;
        }

        Tuple<int, int> NumCoord(string c)
        {
                char x0 = c[0];
                int y0 = int.Parse(c[1].ToString());
                int x = Convert.ToInt32(x0)-65;
                int y = y0-1;
                return new Tuple<int, int> (x,y);
        }

        bool BComplete(string[,] b)
        {   
            int bc = 1;
            for(int i=0; i<b.GetLength(0); i++)
            {
                for(int j=0; j<b.GetLength(1); j++)
                {
                    if(b[i,j]=="X") bc=0;
                }
            }
            if(bc==0) return false;
            else return true;
        }
}  
}
