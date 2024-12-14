using System.IO;

namespace AdventOfCode2024;

public class Day10
{
    public object Part1()
    {
        var map = File.ReadAllLines("Day10.txt");

        var result = map.EnumerateAll()
            .Where(r => r.obj == '0')
            .Select(r => Reachable9Positions('0', r.cord, map).Distinct().Count())
            .Sum();
            
        return result;
    }

    public object Part2()
    {
        var map = File.ReadAllLines("Day10.txt");

        var result = map.EnumerateAll()
            .Where(r => r.obj == '0')
            .Select(r => Reachable9Positions('0', r.cord, map).Count())
            .Sum();
            
        return result;
    }

    private static IEnumerable<Coord> Reachable9Positions(char currentHeight, Coord pos, string[] map)
    {
        if (currentHeight == '9')
        {
            yield return pos;
            yield break;
        }
        
        var nextChar = (char) (currentHeight + 1);
        var ret = pos.GetAdjacent(map[0].Length - 1, map.Length - 1)
            .Where(p => map.GetAt(p) == nextChar)
            .SelectMany(a => Reachable9Positions(nextChar, a, map));

        foreach (var c in ret)
            yield return c;
    }
}