using System.IO;

namespace AdventOfCode2024;

public class Day8
{
    public object Part1()
    {
        var map = File.ReadAllLines("Day8.txt");

        var antennas = new Dictionary<char, List<Coord>>();
        for (int y = 0; y < map.Length; y++)
        for (int x = 0; x < map[y].Length; x++)
        {
            var freq = map[y][x];
            if (freq != '.')
            {
                if (antennas.TryGetValue(freq, out var list) == false)
                {
                    list = new List<Coord>();
                    antennas.Add(freq, list);
                }

                list.Add(new Coord(x, y));
            }
        }

        var antinodes = new List<Coord>();
        foreach (var freq in antennas)
            for (int i = 0; i < freq.Value.Count; i++)
            for (int j = i + 1; j < freq.Value.Count; j++)
            {
                AddAntiNodes(freq.Value[i], freq.Value[j], antinodes, map);
            }

        return antinodes.Distinct().Count();
    }

    private void AddAntiNodes(Coord a1, Coord a2, List<Coord> antinodes, string[] map)
    {
        var vector = (x:a2.X - a1.X, y: a2.Y - a1.Y);

        var node1 = new Coord(a1.X - vector.x, a1.Y - vector.y);
        if(IsOnMap(node1, map))
            antinodes.Add(node1);
        node1 = new Coord(a2.X + vector.x, a2.Y + vector.y);
        if(IsOnMap(node1, map))
            antinodes.Add(node1);

        if (vector.y % 3 == 0 && vector.x % 3 == 0)
        {
            var node2 = new Coord(a1.X + vector.x/3, a1.Y + vector.y/3);
            if(IsOnMap(node2, map))
                antinodes.Add(node2);
            node2 = new Coord(a2.X - vector.x/3, a2.Y - vector.y/3);
            if(IsOnMap(node2, map))
                antinodes.Add(node2);
        }
    }

    private bool IsOnMap(Coord coordinate, string[] map)
    {
        return coordinate.X >= 0 && coordinate.X < map.Length && coordinate.Y >= 0 && coordinate.Y < map.Length;
    }
    
    
    public object Part2()
    {
        var map = File.ReadAllLines("Day8.txt");

        var antennas = new Dictionary<char, List<Coord>>();
        for (int y = 0; y < map.Length; y++)
        for (int x = 0; x < map[y].Length; x++)
        {
            var freq = map[y][x];
            if (freq != '.')
            {
                if (antennas.TryGetValue(freq, out var list) == false)
                {
                    list = new List<Coord>();
                    antennas.Add(freq, list);
                }

                list.Add(new Coord(x, y));
            }
        }

        var antinodes = new HashSet<Coord>();
        foreach (var freq in antennas)
            for (int i = 0; i < freq.Value.Count; i++)
            for (int j = i + 1; j < freq.Value.Count; j++)
            {
                AddAntiNodes2(freq.Value[i], freq.Value[j], antinodes, map);
            }

        return antinodes.Distinct().Count();
    }
    
    private void AddAntiNodes2(Coord a1, Coord a2, HashSet<Coord> antinodes, string[] map)
    {
        var vector = (x:a2.X - a1.X, y: a2.Y - a1.Y);
        
        // Move away toward other antenna
        var pos = a1;
        while (IsOnMap(pos, map))
        {
            antinodes.Add(pos);
            pos += vector;
        }
        
        // Move away from other antenna
        pos = a1 - vector;
        while (IsOnMap(pos, map))
        {
            antinodes.Add(pos);
            pos -= vector;
        }
    }
}