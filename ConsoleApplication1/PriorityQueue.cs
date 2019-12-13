using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class PriorityQueue<T> where T:State
    {
        public int NumOfNodes = 0;
        public int capacity;
        public State[] Nodes;
        public PriorityQueue(int cap)
        {
            NumOfNodes = 0;
            capacity = cap;
            Nodes = new T[cap];
        }
        public int left(int i)
        {
            return (2 * i) + 1;
        }
        public int right(int i)
        {
            return (2 * i) + 2;
        }
        public int getParent(int ind)
        {
            // if ((ind > 1) && (ind < Nodes.Length))
            {
                return ind / 2;
            }
        }
        public void swap(ref State node1, ref State node2)
        {
            State temp = node1;
            node1 = node2;
            node2 = temp;
        }
        public void MinHeapify(int index)
        {
            int smallest = index;
            int le = left(index);
            int ri = right(index);
            if (le < NumOfNodes && Nodes[le].cost < Nodes[index].cost)
            {
                smallest = le;
            }
            if (ri < NumOfNodes && Nodes[ri].cost < Nodes[smallest].cost)
            {
                smallest = ri;
            }
            if (smallest != index)
            {
                swap(ref Nodes[smallest], ref Nodes[index]);
                MinHeapify(smallest);
            }
        }
        public void BuildMinHeap(ref int[] Arr)
        {
            for (int i = Arr.Length / 2; i >= 0; i--)
            {
                MinHeapify(i);
            }
        }
        public int minimum(int[] A)
        {
            return A[1];
        }
        public int heap_extract_min(ref int[] Arr)
        {
            if (NumOfNodes < 1)
            {
                Console.WriteLine("Error,Heab underflow");
            }
            int min = Arr[0];
            Arr[0] = Arr[NumOfNodes];
            NumOfNodes -= 1;
            MinHeapify(0);
            return min;
        }
        public State extractMin()
        {
            if (NumOfNodes <= 0)
                return null;
            
            State min = Nodes[0];
            Nodes[0] = Nodes[NumOfNodes - 1];
            NumOfNodes--;
            MinHeapify(0);
            return min;
        }
        public void IncreaseHeabSize()
        {
            State[] arr = new T[Nodes.Length * 2];
            for (int i = 0; i < Nodes.Length; i++)
                arr[i] = Nodes[i];
            Nodes = arr;
        }
        public void insert(State key)
        {
            int i = NumOfNodes;
            NumOfNodes++;
            if (i == Nodes.Length)
                IncreaseHeabSize();
            Nodes[i] = key;
            while (i > 0 && Nodes[getParent(i)].cost > Nodes[i].cost)
            {
                swap(ref Nodes[getParent(i)], ref Nodes[i]);
                i = getParent(i);
            }
        }
    }
}
