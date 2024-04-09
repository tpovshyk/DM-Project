namespace DM_P;

public static class GraphAlgorithms
{
    public static bool[,] DfsToMatrix(List<Tuple<int, int>> edges, int n)
    {
        var matrix = new bool[n, n];
        var adjacencyList = new List<int>[n];
        for (var i = 0; i < n; i++)
        {
            adjacencyList[i] = [];
        }

        foreach (var edge in edges)
        {
            var from = edge.Item1;
            var to = edge.Item2;
            adjacencyList[from].Add(to);
        }

        for (var i = 0; i < n; i++)
        {
            var visited = new bool[n];
            Dfs(adjacencyList, i, i, visited, matrix);
        }

        return matrix;
    }

    private static void Dfs(List<int>[] adjacencyList, int start, int current, bool[] visited, bool[,] matrix)
    {
        visited[current] = true;
        matrix[start, current] = true;

        foreach (var neighbor in adjacencyList[current])
        {
            if (!visited[neighbor])
            {
                Dfs(adjacencyList, start, neighbor, visited, matrix);
            }
        }
    }

    public static bool[,] BfsToMatrix(List<Tuple<int, int>> edges, int n)
    {
        var matrix = new bool[n, n];
        var adjacencyList = new List<int>[n];
        for (var i = 0; i < n; i++)
        {
            adjacencyList[i] = new List<int>();
        }

        foreach (var edge in edges)
        {
            var from = edge.Item1;
            var to = edge.Item2;
            adjacencyList[from].Add(to);
        }
        
        for (var i = 0; i < n; i++)
        {
            Bfs(i, adjacencyList, matrix, n);
        }

        return matrix;
    }

    public static void Bfs(int start, List<int>[] adjacencyList, bool[,] matrix, int n)
    {
        var queue = new Queue<int>();
        var visited = new bool[n];
        visited[start] = true;
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            matrix[start, current] = true;

            foreach (var neighbor in adjacencyList[current])
            {
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    queue.Enqueue(neighbor);
                }
            }
        }
    }

    public static void PrintMatrix(bool[,] matrix)
    {
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] ? "1 " : "0 ");
            }
            Console.WriteLine();
        }
    }
}
