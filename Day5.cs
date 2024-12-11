using System.IO;

namespace AdventOfCode2024;

public class Day5
{
    public string? Part1()
    {
        var rules = new List<(int first, int second)>();
        
        using var lineReader = File.ReadLines("Day5.txt").GetEnumerator();
        while (lineReader.MoveNext())
        {
            if (string.IsNullOrEmpty(lineReader.Current))
                break;
            
            var line = lineReader.Current;
            var parts = line.Split('|').Select(int.Parse).ToArray();
            rules.Add((parts[0], parts[1]));
        }

        int value = 0;
        while (lineReader.MoveNext())
        {
            var row = lineReader.Current.Split(',').Select(int.Parse).ToList();
            if (ValidateRules(rules, row))
                value += row[row.Count / 2];
        }
            
        return value.ToString();
    }

    private static bool ValidateRules(List<(int first, int second)> rules, List<int> row)
    {
        return rules.All(r => ValidateRule(r.first, r.second, row));
    }

    private static bool ValidateRule(int first, int second, List<int> row)
    {
        var ix1 = row.IndexOf(first);
        var ix2 = row.IndexOf(second);
        if (ix1 == -1 || ix2 == -1)
            return true;
        return ix1 < ix2;
    }

    public string Part2()
    {
        var rules = new List<(int first, int second)>();
        
        using var lineReader = File.ReadLines("Day5.txt").GetEnumerator();
        while (lineReader.MoveNext())
        {
            if (string.IsNullOrEmpty(lineReader.Current))
                break;
            
            var line = lineReader.Current;
            var parts = line.Split('|').Select(int.Parse).ToArray();
            rules.Add((parts[0], parts[1]));
        }

        int value = 0;
        while (lineReader.MoveNext())
        {
            var row = lineReader.Current.Split(',').Select(int.Parse).ToList();
            if (ValidateRules(rules, row) == false)
            {
                var row2 = CorrectOrdering(rules, row);
                value += row2[row2.Count / 2];
            }
        }
         
        return value.ToString();
    }

    private static List<int> CorrectOrdering(List<(int first, int second)> rules, List<int> row)
    {
        var result = new List<int>();
        while (row.Count > 0)
        {
            var leftsMost = row
                .Select(r => (val: r, rightMatches: rules.Count(q => r == q.second && row.Contains(q.first))))
                .OrderBy(q => q.rightMatches)
                .First();
            result.Add(leftsMost.val);
            row.Remove(leftsMost.val);
        }
        
        return result;
    }
}