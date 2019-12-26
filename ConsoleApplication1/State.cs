using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    struct Movements
    {
        public bool left;
        public bool right;
        public bool up;
        public bool down;
    }

    class State
    {
        public int[,] integers;
        public int cost;
        public  State parent;
        public Movements mov;
        public int CostInDepth;
        public int numOfPossibleMov = 0;
        public int numOfMov = 0;
        public StringBuilder uniqueKey = new StringBuilder();
        public string unique = null;
        int size;
        public int getCost()
        {
            return cost;
        }
        public State(State P, int[,] integers,  int PastCost, ref bool isTheGoal, int size)
        {
            this.size = size;
            //CostInDepth equals 0 becaues it is the initial state 
            CostInDepth = 0;
            //int size = ;
            this.integers = new int[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    this.integers[i, j] = integers[i, j];
                   // uniqueKey.Append(integers[i, j]);
                }
            parent = P;            
        }
        public State(State P, int[,] integers, int oldX, int oldY, int newX, int newY, int PastCost,  ref bool isInColsed, ref HashSet<string> ClosedStates, int size)
        {          
            CostInDepth = PastCost + 1;
            //int size = integers.GetLength(0);
            this.size = size;
            this.integers = new int[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    this.integers[i, j] = integers[i, j];
                    uniqueKey.Append(integers[i, j]);
                }
            int tmp = this.integers[newX, newY];
            this.integers[newX, newY] = this.integers[oldX, oldY];
            this.integers[oldX, oldY] = tmp;
            //swap the uniqueKey chars after swaping the integers in array
            uniqueKey.Replace(integers[oldX, oldY].ToString(), "~").Replace(integers[newX, newY].ToString(), integers[oldX, oldY].ToString()).Replace("~", integers[newX, newY].ToString());
            unique = uniqueKey.ToString();

            if (ClosedStates.Contains(unique))
                isInColsed = true;
          
            parent = P;
        }

        public bool IsSolvable(int size, int[,] start)
        {
            int NumOfInv = getInvCount(start, size);
            //int row = -1;
            
            int row=_findIndex(start);
            int pos = size - row;
            if (size % 2 == 1) //Odd Number
            {
                if (NumOfInv % 2 == 0)
                    return true;
            }
            else //Even Number
            {
                if ((pos % 2 == 0 && NumOfInv % 2 == 1) || (pos % 2 == 1 && NumOfInv % 2 == 0))
                    return true;
            }
            return false;
        }

        public int getInvCount(int[,] start, int size)
        {
            int[] start1 = new int[size * size];
            int index = 0;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    start1[index] = start[i, j];
                    index++;
                }

            int inv_count = 0;
            for (int i = 0; i < size * size - 1; i++)
            {
                if (start1[i] == 0)
                    continue;
                for (int j = i + 1; j <= size * size - 1; j++)
                {
                    if (start1[j] == 0)
                        continue;
                    if (start1[i] > start1[j])
                        inv_count++;
                }
            }

            return inv_count;
        }

        int _findIndex(int[,]s)
        {
            
            for(int i=0;i<size;i++)
                for(int j = 0; j < size; j++)
                {
                    if (s[i, j] == 0)
                        return i;
                }
            return 0;
        }


    }
}