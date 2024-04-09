using DM_P;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        var continueLoop = true;
        var leaderboardFilePath = "leaderboard_results.txt";
        var leaderboard = new Leaderboard();

        while (continueLoop)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Compute");
            Console.WriteLine("2. Leaderboard");
            Console.WriteLine("3. Quit");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.WriteLine("\nCompute Menu:");
                    Console.WriteLine("Enter the number of vertices (between 20 and 200):");
                    int n;
                    while (!int.TryParse(Console.ReadLine(), out n) || n < 20 || n > 200)
                    {
                        Console.WriteLine("Invalid input. Please enter a number between 20 and 200.");
                    }

                    Console.WriteLine("Enter the density (between 0 and 1):");
                    double density;
                    while (!double.TryParse(Console.ReadLine(), out density) || density < 0 || density > 1)
                    {
                        Console.WriteLine("Invalid input. Please enter a number between 0 and 1.");
                    }

                    Console.WriteLine("\nChoose the data structure for representing graphs:");
                    Console.WriteLine("1. Matrix");
                    Console.WriteLine("2. List");
                    var structureChoice = Console.ReadLine().ToLower();

                    Console.WriteLine("\nChoose the algorithm for computing reachability matrices:");
                    Console.WriteLine("1. DFS");
                    Console.WriteLine("2. BFS");
                    int algorithmChoice;
                    while (!int.TryParse(Console.ReadLine(), out algorithmChoice) ||
                           (algorithmChoice != 1 && algorithmChoice != 2))
                    {
                        Console.WriteLine("Invalid input. Please enter 1 or 2.");
                    }

                    double totalMatrixTime = 0;

                    for (var i = 0; i < 20; i++)
                    {
                        Console.WriteLine($"\nIteration {i + 1}:");

                        var stopwatch = new Stopwatch();
                        stopwatch.Start();

                        bool[,] reachabilityMatrix;

                        if (structureChoice == "1")
                        {
                            var randomGraphMatrix = RandomGraphGenerator.GenerateRandomGraphAsMatrix(n, density);

                            stopwatch.Start();

                            if (algorithmChoice == 1)
                            {
                                reachabilityMatrix = GraphAlgorithmsMatrix.DfsMatrixToMatrix(randomGraphMatrix, n);
                            }
                            else
                            {
                                reachabilityMatrix = GraphAlgorithmsMatrix.BfsMatrixToMatrix(randomGraphMatrix, n);
                            }
                        }
                        else
                        {
                            List<Tuple<int, int>> randomGraph = RandomGraphGenerator.GenerateRandomGraph(n, density);

                            stopwatch.Start();

                            if (algorithmChoice == 2)
                            {
                                reachabilityMatrix = GraphAlgorithms.BfsToMatrix(randomGraph, n);
                            }
                            else
                            {
                                reachabilityMatrix = GraphAlgorithms.DfsToMatrix(randomGraph, n);
                            }
                        }
                        
                        Console.WriteLine("Reachability Matrix:");
                        GraphAlgorithms.PrintMatrix(reachabilityMatrix);

                        stopwatch.Stop();
                        var elapsedTime = stopwatch.Elapsed.TotalMilliseconds;
                        totalMatrixTime += elapsedTime;

                        var key = $"Vertices: {n}, Density: {density}, Algorithm: {(algorithmChoice == 1
                            ? "DFS" : "BFS")}, Structure: {(structureChoice == "1" ? "Matrix" : "List")}";
                        leaderboard.AddResult(key, elapsedTime);

                        Console.WriteLine($"Time elapsed for matrix construction: {elapsedTime} milliseconds");
                    }

                    var averageMatrixTime = totalMatrixTime / 20;
                    Console.WriteLine($"\nAverage time elapsed for matrix construction over 20 iterations:" +
                                      $" {averageMatrixTime} milliseconds");

                    leaderboard.WriteToFile(leaderboardFilePath);
                    break;

                case "2":
                    Console.WriteLine("\nLeaderboard Menu:");
                    Console.WriteLine("1. View Leaderboard");
                    Console.WriteLine("2. Clear Leaderboard");
                    var leaderboardOption = Console.ReadLine();

                    switch (leaderboardOption)
                    {
                        case "1":
                            if (File.Exists(leaderboardFilePath))
                            {
                                var lines = File.ReadAllLines(leaderboardFilePath);
                                foreach (var line in lines)
                                {
                                    Console.WriteLine(line);
                                }
                            }
                            break;

                        case "2":
                            leaderboard.Clear(leaderboardFilePath);
                            Console.WriteLine("Leaderboard cleared.");
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please choose a valid option.");
                            break;
                    }
                    break;

                case "3":
                    break;
            }

            string continueInput;
            do
            {
                Console.WriteLine("\nDo you want to continue? (yes/no)");
                continueInput = Console.ReadLine().ToLower();

                if (continueInput == "no")
                {
                    return;
                }
                
            } while (continueInput != "yes");
        }
    }
}
