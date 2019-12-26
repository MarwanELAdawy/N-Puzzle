using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
        again:
            
            Puzzle puzzle = new Puzzle();
            int[,] res = puzzle.InitialState();
            int size = puzzle.size;
            int ch;
            int choice;
            int n = 1;
            int[,] goal = new int[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    goal[i, j] = n;
                    n++;
                }
            goal[size - 1, size - 1] = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("#################################################################");
                puzzle.print();
                Console.WriteLine("#################################################################");
            //InitialGraph(res, size);
            EnterAgain:
                Console.WriteLine();
                Console.WriteLine("Enter 1 to Show number of puzzle's movements.H");
                Console.WriteLine("Enter 2 to Show number of puzzle's movements.M");
                Console.WriteLine("Enter 3 to Choose another puzzle.");
                Console.Write("Enter your choice: ");
                try
                {
                    choice = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                }
                catch
                {
                    Console.WriteLine("Wrong Choice!!!");
                    goto EnterAgain;
                }
                switch (choice)
                {
                    case 1:

                        new A_Star(res, goal, size,true);
                        break;
                    case 2:
                        new A_Star(res, goal, size,false);
                        break;
                    case 3:
                        Console.Clear();
                        goto again;
                    default:
                        break;
                }

            YNAgain:
                Console.Write("Do You Want To Add Another Operation (Y/N):");
                try
                {
                    ch = char.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine();
                    Console.WriteLine("Wrong Choice!!!");
                    goto YNAgain;
                }
            }
            while (ch == 'y' || ch == 'Y') ;
        
        }

        //coordinate of block
        static int[] SearchForRoot(int[,] Matrix, int size)
        {
            int[] MatrixCoordinate = new int[1];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (Matrix[i, j] == 0)
                    {
                        MatrixCoordinate[0] = i;
                        MatrixCoordinate[1] = j;
                        break;
                    }

            return MatrixCoordinate;
        }

        static void InitialGraph(int[,] arr, int size)
        {
            Graph g = new Graph(size);
            int row = 0;
            int col = 0;

            for (row = 0; row < size; row++)
                for (col = 0; col < size; col++)
                    if (col == 0 && row == 0)
                    {
                        g.addedge(row, col, arr[row, col + 1]);
                        g.addedge(row, col, arr[row + 1, col]);
                    }
            
                    else if (col == size - 1 && row == 0)
                    {
                        g.addedge(row, col, arr[row, col - 1]);
                        g.addedge(row, col, arr[row + 1, col]);
                    }

                    else if (row == size - 1 && col == 0)
                    {
                        g.addedge(row, col, arr[row, col + 1]);
                        g.addedge(row, col, arr[row - 1, col]);
                    }

                    else if (col == size - 1 && row == size - 1)
                    {
                        g.addedge(row, col, arr[row, col - 1]);
                        g.addedge(row, col, arr[row - 1, col]);
                    }

                    else
                        if ((row - 1) != -1 && (row + 1) != size && (col + 1) != size && (col - 1) != -1)
                        {
                            g.addedge(row, col, arr[row - 1, col]);// upper
                            g.addedge(row, col, arr[row, col + 1]);//next
                            g.addedge(row, col, arr[row, col - 1]);//previous
                            g.addedge(row, col, arr[row + 1, col]);
                        }
                        else
                            if ((row - 1) == -1)
                            {
                                g.addedge(row, col, arr[row, col + 1]);//next
                                g.addedge(row, col, arr[row, col - 1]);//previous
                                g.addedge(row, col, arr[row + 1, col]);
                            }
                            else if ((row + 1) == size)
                            {
                                g.addedge(row, col, arr[row - 1, col]);// upper
                                g.addedge(row, col, arr[row, col + 1]);//next
                                g.addedge(row, col, arr[row, col - 1]);//previous
                            }
                            else if ((col - 1) == -1)
                            {
                                g.addedge(row, col, arr[row - 1, col]);// upper
                                g.addedge(row, col, arr[row, col + 1]);//next
                                g.addedge(row, col, arr[row + 1, col]);
                            }
                            else if ((col + 1) == size)
                            {
                                g.addedge(row, col, arr[row - 1, col]);// upper
                                g.addedge(row, col, arr[row, col - 1]);//previous
                                g.addedge(row, col, arr[row + 1, col]);
                            }
            g.DisplayGraph();
        }
    }
}