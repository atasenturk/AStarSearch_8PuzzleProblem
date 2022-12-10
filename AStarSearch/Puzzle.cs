using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarSearch
{
    public class Puzzle
    {

        public int[,] CurrBoard;
        public int[,] GoalBoard;
        public int ZeroLocationX;
        public int ZeroLocationY;
        public int RealCost;
        public List<Puzzle> Childs;
        public Puzzle Parent;
        public int Size;
        public bool IsVisited { get; set; }
        public Puzzle(int[,] currBoard, int[,] goalBoard, int size)
        {
            this.CurrBoard = currBoard;
            this.GoalBoard = goalBoard;
            Childs = new List<Puzzle>();
            IsVisited = false;
            Parent = null;
            Size = size;
            RealCost = 0;
            SetZeroLocation();
        }

        private void SetZeroLocation()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (CurrBoard[i, j] == 0)
                    {
                        this.ZeroLocationX = i;
                        this.ZeroLocationY = j;
                    }
                }
            }
        }

        public int[] GetZeroLocation()
        {
            int[] RowCol = new int[2];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (CurrBoard[i, j] == 0)
                    {
                        RowCol[0] = i;
                        RowCol[1] = j;
                    }
                }
            }

            return RowCol;
        }

        // Generating the possible childs
        public void GenerateChilds()
        {
            // Sliding a tile leftwise into the hole 

            if (ZeroLocationY != 0)
            {
                Childs.Add(MakeMove(new int[] { ZeroLocationX, ZeroLocationY }, new int[] { ZeroLocationX, ZeroLocationY - 1 }));
            }

            // Sliding a tile rightwise into the hole 
            if (ZeroLocationY != Size - 1)
            {
                Childs.Add(MakeMove(new int[] { ZeroLocationX, ZeroLocationY }, new int[] { ZeroLocationX, ZeroLocationY + 1 }));
            }

            // Sliding a tile topwise into the hole 
            if (ZeroLocationX != 0)
            {
                Childs.Add(MakeMove(new int[] { ZeroLocationX, ZeroLocationY }, new int[] { ZeroLocationX - 1, ZeroLocationY }));
            }

            // Sliding a tile bottomwise into the hole 
            if (ZeroLocationX != Size - 1)
            {
                Childs.Add(MakeMove(new int[] { ZeroLocationX, ZeroLocationY }, new int[] { ZeroLocationX + 1, ZeroLocationY }));
            }
        }

        // Switching
        public Puzzle MakeMove(int[] zeroLocation, int[] newZeroLocation)
        {
            int[,] newBoard = new int[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    newBoard[i, j] = CurrBoard[i, j];
                }
            }

            int temp = newBoard[newZeroLocation[0], newZeroLocation[1]];
            newBoard[zeroLocation[0], zeroLocation[1]] = temp;
            newBoard[newZeroLocation[0], newZeroLocation[1]] = 0;

            return new Puzzle(newBoard, GoalBoard, Size);
        }

        // Heuristic function is numberofmisplacedtiles
        public int HeuristicCost()
        {
            return NumberOfMisplacedTiles() + ManhattanDistance();
        }

        public int NumberOfMisplacedTiles()
        {
            int misPlacedCount = 0;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if ((CurrBoard[i, j] != GoalBoard[i, j]) && CurrBoard[i, j] != 0) misPlacedCount++;
                }
            }
            return misPlacedCount;
        }

        public int ManhattanDistance()
        {
            List<int> board = ConvertToList(CurrBoard);
            List<int> goal = ConvertToList(GoalBoard);
            int row, col, distance, heuristic = 0;
            for (int i = 1; i < Size * Size; i++)
            {
                distance = Math.Abs(board.IndexOf(i) - goal.IndexOf(i));
                row = distance / 3;
                col = distance % 3;
                heuristic += row + col;
            }
            return heuristic;
        }


        public bool IsFinal()
        {
            bool isFinal = true;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (CurrBoard[i, j] != GoalBoard[i, j]) isFinal = false;
                }
            }

            return isFinal;
        }

        public List<int> getCurrentBoard()
        {
            List<int> currentBoardElements = new List<int>();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    currentBoardElements.Add(CurrBoard[i, j]);
                }
            }
            return currentBoardElements;
        }

        public List<int> ConvertToList(int[,] arr)
        {
            List<int> elements = new List<int>();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    elements.Add(arr[i, j]);
                }
            }
            return elements;
        }

        public string GetPath()
        {
            Puzzle curr = this;
            string final = "";

            List<Puzzle> list = new List<Puzzle>();
            while (curr != null)
            {
                list.Add(curr);
                curr = curr.Parent;
            }

            for (int i = list.Count - 1; i >= 0; i--)
            {
                final += "Step " + (list.Count - i).ToString() + "\n------\n";
                final += "Heuristic Cost: " + (list[i].HeuristicCost()).ToString()+ "  Path Cost: "+ list[i].RealCost + "\n";
                for (int j = 0; j < Size; j++)
                {
                    for (int k = 0; k < Size; k++)
                    {
                        final += list[i].CurrBoard[j, k] + "  ";
                        if (k == 2) final += "\n";
                    }
                }

                final += "\n\n";
            }

            return final;
        }
    }
}
