namespace DM_P;
using System.Collections.Generic;
using System.IO;

public class Leaderboard
{
    private readonly Dictionary<string, List<double>> _results = new();

    public void AddResult(string key, double time)
    {
        if (!_results.ContainsKey(key))
        {
            _results[key] = [];
        }
        
        _results[key].Add(time);
    }

    public void WriteToFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
        }
        
        using (var writer = new StreamWriter(filePath, true))
        {
            foreach (var (key, value) in _results)
            {
                var averageTime = CalculateAverage(value);
                writer.WriteLine($"{key}: {averageTime} milliseconds");
            }
        }

        _results.Clear();
    }

    private double CalculateAverage(List<double> times)
    {
        double sum = times.Sum();

        return sum / times.Count;
    }

    public void Clear(string filePath)
    {
        _results.Clear();
        File.WriteAllText(filePath, string.Empty);
    }

    public void View(string leaderboardFilePath)
    {
        if (File.Exists(leaderboardFilePath))
        {
            var lines = File.ReadAllLines(leaderboardFilePath);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
        else
        {
            Console.WriteLine("Leaderboard file does not exist.");
        }
    }
}
