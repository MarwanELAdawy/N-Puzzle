using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication1
{
    class Puzzle
    {
        public int RowArr = 0, ColArr = 0;
        public int RowIndex = 0;
        public int size = 0;
        public int[,] res = null;
        public int choice;
        public string[] puzzles;
        public string Filename;

        public Puzzle()
        {
        here:
            Console.WriteLine("#################################################################");
            puzzles = File.ReadAllLines("puzzles.txt");
            for (int i = 0; i < puzzles.Length; i++)
                Console.WriteLine((i + 1) + "_ " + puzzles[i]);

            Console.WriteLine("#################################################################");
            Console.Write("Enter your puzzle: ");
            
            try
            {
                
                choice = int.Parse(Console.ReadLine());

              
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine("Wrong Choice!!!");
                Thread.Sleep(1000);
                Console.Clear();
                goto here;
            }
            try
            {
                Filename = puzzles[choice - 1] + ".txt";
            }
            catch(Exception e)
            {
                Console.WriteLine();
                Console.WriteLine("Wrong Choice!!!");
                Thread.Sleep(1000);
                Console.Clear();
                goto here;
                
            }
        }

        public int[,] InitialState()
        {
            try
            {
                this.Filename = File.ReadAllText(this.Filename);
                foreach (var row in this.Filename.Split('\n'))
                {
                    if (row != "\r")
                    {
                        if (RowIndex == 0)
                        {
                            size = int.Parse(row.Trim());
                            res = new int[size, size];
                        }
                        else
                        {
                            ColArr = 0;
                            foreach (var col in row.Trim().Split(' '))
                            {
                                res[RowArr, ColArr] = int.Parse(col.Trim());
                                ColArr++;
                            }
                            RowArr++;
                        }
                        RowIndex++;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Wrong Choice!!!");
            }
            return res;
        }

        public void print()
        {
            Console.WriteLine("Size: " + size);
            Console.WriteLine("Initial State:");
            for(int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(res[i,j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}