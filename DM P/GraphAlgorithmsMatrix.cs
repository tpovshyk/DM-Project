namespace DM_P;

public static class GraphAlgorithmsMatrix
{
    public static bool[,] DfsMatrixToMatrix(bool[,] edges, int n)
    {
        var matrix = new bool[n, n];
        var visited = new bool[n];

        for (var i = 0; i < edges.GetLength(0); i++)
        {
            for (var j = 0; j < edges.GetLength(1); j++)
            {
                if (edges[i, j])
                {
                    Dfs(i, j, visited, matrix);
                }
            }
        }

        return matrix;
    }

    private static void Dfs(int start, int current, bool[] visited, bool[,] matrix)
    {
        visited[current] = true;
        matrix[start, current] = true;

        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            if (!visited[i] && matrix[current, i])
            {
                Dfs(start, i, visited, matrix);
            }
        }
    }

    public static bool[,] BfsMatrixToMatrix(bool[,] edges, int n)
    {
        var matrix = new bool[n, n];

        for (var i = 0; i < edges.GetLength(0); i++)
        {
            for (var j = 0; j < edges.GetLength(1); j++)
            {
                if (edges[i, j])
                {
                    Bfs(i, edges, matrix, n);
                }
            }
        }

        return matrix;
    }

    public static void Bfs(int start, bool[,] edges, bool[,] matrix, int n)
    {
        var queue = new Queue<int>();
        var visited = new bool[n];
        visited[start] = true;
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            for (var i = 0; i < n; i++)
            {
                if (!visited[i] && edges[current, i])
                {
                    visited[i] = true;
                    matrix[current, i] = true;
                    queue.Enqueue(i);
                }
            }
        }
    }
}