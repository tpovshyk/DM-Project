namespace DM_P;
    public static class RandomGraphGenerator
    {
        public static List<Tuple<int, int>> GenerateRandomGraph(int n, double density)
        {
            var random = new Random();
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();

            for (var i = 0; i < n; i++)
            {
                for (var j = i + 1; j < n; j++)
                {
                    if (random.NextDouble() < density)
                    {
                        edges.Add(new Tuple<int, int>(i, j));
                    }
                }
            }

            return edges;
        }

        public static bool[,] GenerateRandomGraphAsMatrix(int n, double density)
        {
            var random = new Random();
            var matrix = new bool[n, n];

            for (var i = 0; i < n; i++)
            {
                for (var j = i + 1; j < n; j++)
                {
                    if (random.NextDouble() < density)
                    {
                        matrix[i, j] = true;
                        matrix[j, i] = true;
                    }
                }
            }

            return matrix;
        }
    }