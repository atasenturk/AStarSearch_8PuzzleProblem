using AStarSearch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarSearch
{
    public static class AStar
    {
        public static Puzzle Perform(Puzzle initial)
        {
            Puzzle current;
            PriorityQueue<Puzzle, int> pq = new PriorityQueue<Puzzle, int>();
            pq.Enqueue(initial, initial.HeuristicCost());

            List<Puzzle> visited = new List<Puzzle>();
            visited.Add(initial);

            while (pq.Count != 0)
            {
                current = pq.Dequeue();
                
                if (current.IsFinal())
                {
                    return current;
                }

                current.GenerateChilds();

                foreach (var Node in current.Childs)
                {
                    if (visited.FirstOrDefault(i => i.getCurrentBoard().SequenceEqual(Node.getCurrentBoard())) == null)
                    {
                        visited.Add(Node);
                        Node.IsVisited = true;
                        Node.Parent = current;
                        Node.RealCost = current.RealCost + 1;
                        pq.Enqueue(Node, Node.HeuristicCost() + Node.RealCost);
                    }
                }


                //for (int i = 0; i < pq.heap.Count; i++)
                //{
                //    Console.WriteLine(pq.heap[i].Key + " ");
                //}
                //Console.WriteLine("\n");
            }

            return null;
        }
    }
}
