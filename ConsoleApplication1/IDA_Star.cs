using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class IDA_Star
    {
        public static PriorityQueue<State> OpenStats = null;
        public static HashSet<State> ClosedStates;
        public static State solution, initialState, goalState;
        public static int accessed, maxSize, depthLimit, expanded;
        public static bool isTheGoal;
        public static void setup(int[,]start, Dictionary<int,KeyValuePair<int,int>> goal,int size,int heauristic)
        {
            solution = null;
            initialState.cost = heauristic;
            goalState = new State(null, start, 0, true,ref isTheGoal,goal);
            ClosedStates = new HashSet<State>();
            accessed = 0;
            expanded = 0;
            maxSize = 0;
        }
        public static void IDA(int[,] start, Dictionary<int, KeyValuePair<int, int>> goal, int size, int heauristic,int beam)
        {
            setup(start, goal, size, heauristic);
            int count = 0;
            while (solution==null&&depthLimit<Math.Pow(size,Math.PI)+initialState.getCost())
            {
                ClosedStates.Add(initialState);
                depthLimit = beam * count + initialState.getCost();
                //limitedDFS(initialState, goalState, heauristic);
                expanded += ClosedStates.Count();
                if (ClosedStates.Count() > 3500000)
                    break;
                ClosedStates.Clear();
                Console.WriteLine("Current Limit: " + (depthLimit - initialState.getCost()));
                count++;
            }
        }
        //public static State limitedDFS(State currentState,State goalState,int heau)
        //{
        //    if (ClosedStates.Count()> 3500000)
        //    {
        //        return null;
        //    }
        //    State local = null;
        //    if (!(currentState.Equals(goalState)))
        //    {
                
        //    }
        //}
    }
}
