// See https://aka.ms/new-console-template for more information
using AStarSearch;

Console.WriteLine("Hello, World!");

int[,] CurrBoard = {{ 5, 0, 8 },
                             {4, 2, 1 },
                             {7, 3, 6 }};

int[,] GoalBoard =  {{ 1, 2, 3 },
                             {4, 5, 6 },
                             {7, 8, 0 }};

Puzzle puzzle = new Puzzle(CurrBoard, GoalBoard, 3);
Puzzle solutionAStar = AStar.Perform(puzzle);

//for (int i = 0; i < 3; i++)
//{
//    for (int j = 0; j < 3; j++)
//    {
//        Console.WriteLine(solutionAStar.CurrBoard[i, j] + " ");
//    }
//}

Console.WriteLine("A* Search ---> \n");
Console.WriteLine("A* Search Steps:\n");
Console.WriteLine(solutionAStar.GetPath());
Console.ReadKey();