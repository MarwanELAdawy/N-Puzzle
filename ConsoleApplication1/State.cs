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
        int Hamming_Cost;
        int Manhhaten_Cost;
        //first constructor for the initial state
        public State(State P, int[,] integers,  int PastCost, bool solveWithHamman, ref bool isTheGoal, Dictionary<int, KeyValuePair<int, int>>goalindex)
        {
            //CostInDepth equals 0 becaues it is the initial state 
            CostInDepth = 0;

            this.integers = new int[integers.GetLength(0), integers.GetLength(1)];
            for (int i = 0; i < integers.GetLength(0); i++)
                for (int j = 0; j < integers.GetLength(1); j++)
                {
                    this.integers[i, j] = integers[i, j];
                   // uniqueKey.Append(integers[i, j]);
                }

            //Hamming_Cost = HammingCost(this.integers);
            if (solveWithHamman)
            {
                Hamming_Cost = HammingCost(this.integers);
                cost = CostInDepth + Hamming_Cost;
                if (Hamming_Cost == 0)
                {
                    //Console.WriteLine(CostInDepth);
                    isTheGoal = true;
                }
            }
            else
            {
                Manhhaten_Cost = ManhhatenCost(goalindex);
                cost = CostInDepth + Manhhaten_Cost;

                if (Manhhaten_Cost == 0)
                    isTheGoal = true;
            }
          
            
        }
        public State(State P, int[,] integers, int oldX, int oldY, int newX, int newY, int PastCost, bool solveWithHamman, ref bool isInColsed, ref HashSet<string> ClosedStates, ref bool isTheGoal,Dictionary<int, KeyValuePair<int, int>> goalIndex)
        {          
            CostInDepth = PastCost + 1;
            this.integers = new int[integers.GetLength(0), integers.GetLength(1)];
            for (int i = 0; i < integers.GetLength(0); i++)
                for (int j = 0; j < integers.GetLength(1); j++)
                {
                    this.integers[i, j] = integers[i, j];
                    uniqueKey.Append(integers[i, j]);
                }
            //Hamming_Cost = HammingCost(this.integers);
            //swap the integers in the array depending on the movment
            int tmp = this.integers[newX, newY];
            this.integers[newX, newY] = this.integers[oldX, oldY];
            this.integers[oldX, oldY] = tmp;
            //swap the uniqueKey chars after swaping the integers in array
            uniqueKey.Replace(integers[oldX, oldY].ToString(), "~").Replace(integers[newX, newY].ToString(), integers[oldX, oldY].ToString()).Replace("~", integers[newX, newY].ToString());
            unique = uniqueKey.ToString();

            //Hamming_Cost = HammingCost(this.integers);
            //if i have befor this state , isInColsed will be true so it will not be insert in OpenStats
            if (ClosedStates.Contains(unique))
                isInColsed = true;
            if (solveWithHamman)
            {
                Hamming_Cost = HammingCost(this.integers);
                cost = CostInDepth + Hamming_Cost;
                if (Hamming_Cost == 0)
                {
                    //Console.WriteLine(CostInDepth);
                    isTheGoal = true;
                }
            }
            else
            {
                Manhhaten_Cost = ManhhatenCost(goalIndex);
                cost = CostInDepth + Manhhaten_Cost;

                if (Manhhaten_Cost == 0)
                    isTheGoal = true;
            }
            parent = P;
        }
        public int HammingCost(int[,] integers)
        {
            int cost = 0;
            int number = 1;

            for (int i = 0; i < integers.GetLength(0); i++)
                for (int j = 0; j < integers.GetLength(1); j++)
                {
                    if (number == integers.GetLength(0) * integers.GetLength(0))
                        number = 0;
                    if (integers[i, j] != number)
                        cost++;
                    number++;
                }
            return cost;
        }
        public int ManhhatenCost(Dictionary<int, KeyValuePair<int, int>> goalindex)
        {
            int Hcost=0;
            KeyValuePair<int, int> GI;
            for (int i = 0; i < integers.GetLength(0); i++)
            {
                for (int j = 0; j < integers.GetLength(1); j++)
                {
                    GI= goalindex[integers[i, j]];

                    if (i!=GI.Key||j!=GI.Value)
                    {
                        Hcost += Math.Abs(i - GI.Key) + Math.Abs(j - GI.Value);
                    }
                }
            }
            return Hcost;
        }
        public void findIndex(int[,] arr, ref int row, ref int col)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                    if (arr[i, j] == 0)
                    {
                        row = i;
                        col = j;
                    }
        }
        public void findMov(int RowIndex, int ColIndex, int rowSize, int Colsize)
        {
            if (RowIndex - 1 >= 0)
            {
                mov.up = true;
                numOfMov++;
            }
            if (RowIndex + 1 < rowSize)
            {
                mov.down = true;
                numOfMov++;
            }
            if (ColIndex - 1 >= 0)
            {
                mov.left = true;
                numOfMov++;
            }
            if (ColIndex + 1 < Colsize)
            {
                mov.right = true;
                numOfMov++;
            }
        }
    }
}