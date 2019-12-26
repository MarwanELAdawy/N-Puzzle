using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class PriorityQueue
    {
        public int NumOfNodes = 0;
        public int capacity;
        public KeyValuePair<int, int> [] Nodes;
        public PriorityQueue(int cap )
        {
            NumOfNodes = 0;
            capacity = cap;
            Nodes = new KeyValuePair<int, int>[cap];
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
        public void swap(ref KeyValuePair<int, int> node1, ref KeyValuePair<int, int> node2)
        {
            KeyValuePair<int, int> temp = node1;
            node1 = node2;
            node2 = temp;
        }
        public void MinHeapify(int index)
        {
            int smallest = index;
            int le = left(index);
            int ri = right(index);
            if (le < NumOfNodes && Nodes[le].Value < Nodes[index].Value)
            {
                smallest = le;
            }
            if (ri < NumOfNodes && Nodes[ri].Value < Nodes[smallest].Value)
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
        public KeyValuePair<int, int> extractMin()
        {

            if (NumOfNodes <= 0)
            {
                KeyValuePair<int, int> x = new KeyValuePair<int, int>(-1,-1);
                return x;
            }
            
            KeyValuePair<int, int> min = Nodes[0];
            Nodes[0] = Nodes[NumOfNodes - 1];
            NumOfNodes--;
            MinHeapify(0);
            return min;
        }
        public void IncreaseHeabSize()
        {
            KeyValuePair<int, int>[] arr = new KeyValuePair<int, int>[Nodes.Length * 2];
            for (int i = 0; i < Nodes.Length; i++)
                arr[i] = Nodes[i];
            Nodes = arr;
        }
        public void insert(KeyValuePair<int, int> key)
        {
            int i = NumOfNodes;
            NumOfNodes++;
            if (i == Nodes.Length)
                IncreaseHeabSize();
            Nodes[i] = key;
            while (i > 0 && Nodes[getParent(i)].Value > Nodes[i].Value)
            {
                swap(ref Nodes[getParent(i)], ref Nodes[i]);
                i = getParent(i);
            }
        }
    }
}
