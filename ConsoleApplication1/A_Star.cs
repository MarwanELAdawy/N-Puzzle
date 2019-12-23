using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class A_Star
    {
        int[,] currentElements;
        public PriorityQueue<State> OpenStats = null;
        HashSet<string> ClosedStates;
        //HashSet<State> Parents = null;
        Dictionary<int, KeyValuePair<int, int>> goalIndex = new Dictionary<int, KeyValuePair<int, int>>();
        //bool to check if the matrix is the goal
        bool isTheGoal;
        //bool to check if the marix in ClosedStates
        bool isInClosed;
        //The Goal Matrix
        int[,] goal = null;

        public A_Star(int[,] start, int[,]goal)
        {
            this.goal = goal;
            for (int i = 0; i < goal.GetLength(0); i++)
            {
                for (int j = 0; j < goal.GetLength(1); j++)
                {
                    goalIndex.Add(this.goal[i, j],new KeyValuePair<int,int>(i,j));
                }
            }
            //currentElements contains the initial Matrix
            currentElements = start;
            //initialize the OpenStats , ClosedStates & Parents 
            OpenStats = new PriorityQueue<State>(start.Length * 10);
            ClosedStates = new HashSet<string>();
            //Parents = new HashSet<State>();
            //passing the initial matrix to StartNode Function
            StartNode(currentElements);
        }
        public void StartNode(int[,] arr)
        {
            //bool to see if the matix is the goal or not
            isTheGoal = false;
            //make the first state
            State start = new State(null, arr,  0, false, ref isTheGoal,goalIndex);
            //insert the first state in OpenStats
            OpenStats.insert(start);
            //Start A* Algorithem
            if(isTheGoal)
            {
                Console.WriteLine("Number of Movements = " + start.CostInDepth);
                return;
            }
            Go();
        }
        public void Go()
        {

            State current;
            //emptyRowIndex & emptyColIndex are the indecis of The 0
            int emptyRowIndex = -1;
            int emptyColIndex = -1;
            //while true untill the OpenStats' Size equals 0 or till we find the goal 
            while (OpenStats.NumOfNodes != 0)
            {
                //Pick the min cost state from the PiriorityQueue
                current = OpenStats.extractMin();
                //find the index of 0
                current.findIndex(current.integers, ref emptyRowIndex, ref emptyColIndex);
                // find possible movments for the 0
                current.findMov(emptyRowIndex, emptyColIndex, current.integers.GetLength(0), current.integers.GetLength(1));
                //if 0 can move down
                if (current.mov.down)
                {
                    //make isInCLosed & isTheGoal false for now
                    isInClosed = false;
                    isTheGoal = false;
                    // make a State s
                    State s = new State(current, current.integers, emptyRowIndex, emptyColIndex, emptyRowIndex + 1, emptyColIndex,current.CostInDepth, false, ref isInClosed, ref ClosedStates, ref isTheGoal,goalIndex);
                    //see if the state is the goal (it dependes on some operations happend in the state class Constructor )
                    if (isTheGoal)
                    {
                        Console.WriteLine("Number of Movements = " + s.CostInDepth);
                        // return;
                        PrintSolution(s);
                        return;
                    }
                    //see if the state is In the closedState (it dependes on some operations happend in the state class Constructor )
                    //if not put it in the OpenStats
                    if (!isInClosed)
                        OpenStats.insert(s);
                    //if the current state have a new parent(not included in Parent) Put it in Parents
                    
                }
                if (current.mov.up)
                {
                    isInClosed = false;
                    isTheGoal = false;
                    State s = new State(current, current.integers, emptyRowIndex, emptyColIndex, emptyRowIndex - 1, emptyColIndex, current.CostInDepth, false, ref isInClosed, ref ClosedStates, ref isTheGoal,goalIndex);
                    if (isTheGoal)
                    {
                        Console.WriteLine("Number of Movements = " + s.CostInDepth);
                        // return;
                        PrintSolution(s);
                        return;
                    }
                    if (!isInClosed)
                        OpenStats.insert(s);
                   
                }
                if (current.mov.right)
                {
                    isInClosed = false;
                    isTheGoal = false;
                    State s = new State(current, current.integers, emptyRowIndex, emptyColIndex, emptyRowIndex, emptyColIndex + 1, current.CostInDepth, false, ref isInClosed, ref ClosedStates, ref isTheGoal,goalIndex);
                    if (isTheGoal)
                    {
                        Console.WriteLine("Number of Movements = " + s.CostInDepth);
                        //return;
                        PrintSolution(s);
                        return;
                    }
                    if (!isInClosed)
                        OpenStats.insert(s);
                  
                }
                if (current.mov.left)
                {
                    isInClosed = false;
                    isTheGoal = false;
                    State s = new State(current, current.integers, emptyRowIndex, emptyColIndex, emptyRowIndex, emptyColIndex - 1, current.CostInDepth, false, ref isInClosed, ref ClosedStates, ref isTheGoal ,goalIndex);
                    if (isTheGoal)
                    {
                        Console.WriteLine("Number of Movements = " + s.CostInDepth);
                        //return;
                        PrintSolution(s);
                        return;
                    }
                    if (!isInClosed)
                        OpenStats.insert(s);
                    
                }
                //After finishing the state put it into the ClosedStates
                ClosedStates.Add(current.unique);
            }
        }
        void PrintSolution(State S)
        {
            int size = S.integers.GetLength(0);
            int step = 1;
            Stack<State> ss=new Stack<State>();
            while (S.parent !=null)
            {
                ss.Push(S);
                S = S.parent;
            }
            ss.Push(S);
            State solution = ss.Pop();
            while (ss.Count>0)
            {
                for(int i = 0; i < size; i++)
                {
                    for(int j = 0; j < size; j++)
                    {
                        Console.Write(solution.integers[i, j]);
                        Console.Write(" ");
                    }
                    Console.WriteLine("\n");
                }
                Console.WriteLine("\n");
                solution = ss.Pop();
                Console.WriteLine("Step Number : " + step.ToString());
                Console.WriteLine("\n");
                step++;
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(solution.integers[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine("\n");
            }
            return;
        }
    }
}

