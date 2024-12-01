using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public class Day1
{
    public string Part1()
    {
        var lines = File.ReadAllLines("Day1.txt");
        var left = new List<int>();
        var right = new List<int>();
         
        foreach(var match in lines.Select(l => Regex.Match(l, @"^(\d+)\s+(\d+)$")))
        {
            left.Add(int.Parse(match.Groups[1].Value));
            right.Add(int.Parse(match.Groups[2].Value));
        }
        
        left.Sort();
        right.Sort();

        var totalDiff = 0;
        for (int i = 0; i < left.Count; i++)
        {
            totalDiff += Math.Abs(left[i] - right[i]);
        }
        
        return totalDiff.ToString();
    }
    
    public string Part2()
    {
        var lines = File.ReadAllLines("Day1.txt");
        var left = new List<int>();
        var right = new List<int>();
         
        foreach(var match in lines.Select(l => Regex.Match(l, @"^(\d+)\s+(\d+)$")))
        {
            left.Add(int.Parse(match.Groups[1].Value));
            right.Add(int.Parse(match.Groups[2].Value));
        }

        var rightLookup = right
            .GroupBy(key => key)
            .ToDictionary(key => key.Key, val => val.Count());

        var totalDiff = 0;
        foreach (var val in left)
        {
            if (rightLookup.TryGetValue(val, out var count))
                totalDiff += val * count;
        }
        
        return totalDiff.ToString();
    }
}