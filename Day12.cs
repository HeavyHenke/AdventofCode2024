using System.IO;

namespace AdventOfCode2024;

public class Day12
{
    public object Part1()
    {
        var map = File.ReadAllLines("Day12.txt");
        var visited = new HashSet<Coord>();

        long sum = 0;
        for(int y = 0; y < map.Length; y++)
        for (int x = 0; x < map[y].Length; x++)
        {
            var pos = new Coord(x, y);
            if (visited.Contains(pos))
                continue;
            sum += CalcRegion(pos, map, visited);
        }
        
        return sum;
    }

    private static int CalcRegion(Coord pos, string[] map, HashSet<Coord> visited)
    {
        var type = map.GetAt(pos);
        var searchQueue = new Queue<Coord>();
        searchQueue.Enqueue(pos);

        int area = 0;
        int perimiter = 0;
        while (searchQueue.Count > 0)
        {
            var current = searchQueue.Dequeue();
            if(visited.Add(current) == false)
                continue;
            
            area++;
            foreach (var n in current.GetAllAdjacent())
            {
                if (n.X < 0 || n.Y < 0 || n.X >= map.Length || n.Y >= map[0].Length)
                    perimiter++;
                else if (map.GetAt(n) == type)
                    searchQueue.Enqueue(n);
                else
                    perimiter++;
            }
        }

        return area * perimiter;
    }
    
    public object Part2()
    {
        var map = File.ReadAllLines("Day12.txt");
        var visited = new HashSet<Coord>();

        long sum = 0;
        for (int y = 0; y < map.Length; y++)
        for (int x = 0; x < map[y].Length; x++)
        {
            var pos = new Coord(x, y);
            if (visited.Contains(pos))
                continue;
            sum += CalcRegion2(pos, map, visited);
        }
        
        return sum;
    }
    
    private static int CalcRegion2(Coord pos, string[] map, HashSet<Coord> visited)
    {
        var type = map.GetAt(pos);
        var searchQueue = new Queue<Coord>();
        searchQueue.Enqueue(pos);

        var fence = new HashSet<Coord>();
        
        int area = 0;
        while (searchQueue.Count > 0)
        {
            var current = searchQueue.Dequeue();
            if(visited.Add(current) == false)
                continue;
            
            area++;
            
            foreach (var n in current.GetAllAdjacent())
            {
                if(IsValidAndSame(n, map, type))
                    searchQueue.Enqueue(n);
            }
            
            // Calc where the fence should be by making a new coord system that is 3 times larger.
            var c3 = current * 3 + (1, 1);
            if (!IsValidAndSame(current + (0, -1), map, type))
                fence.Add(c3 + (0, -1)); // Up
            if(!IsValidAndSame(current + (0, -1), map, type) || !IsValidAndSame(current + (1, -1), map, type) || !IsValidAndSame(current + (1, 0), map, type))
                fence.Add(c3 + (1, -1)); // Right up
            if(!IsValidAndSame(current + (1, 0), map, type))
                fence.Add(c3 + (1, 0)); // Right
            if(!IsValidAndSame(current + (1, 0), map, type) || !IsValidAndSame(current + (1, 1), map, type) || !IsValidAndSame(current + (0, 1), map, type))
                fence.Add(c3 + (1, 1)); // Right down
            if(!IsValidAndSame(current + (0, 1), map, type))
                fence.Add(c3 + (0, 1)); // Down
            if(!IsValidAndSame(current + (0, 1), map, type) || !IsValidAndSame(current + (-1, 1), map, type) || !IsValidAndSame(current + (-1, 0), map, type))
                fence.Add(c3 + (-1, 1)); // Down left
            if(!IsValidAndSame(current + (-1, 0), map, type))
                fence.Add(c3 + (-1, 0)); // Left
            if(!IsValidAndSame(current + (-1, 0), map, type) || !IsValidAndSame(current + (-1, -1), map, type) || !IsValidAndSame(current + (0, -1), map, type))
                fence.Add(c3 + (-1, -1)); // Left up
        }
        
        // Calculate straight fence sections
        var minX = fence.Min(f => f.X);
        var maxX = fence.Max(f => f.X);
        var minY = fence.Min(f => f.Y);
        var maxY = fence.Max(f => f.Y);
        var straightFenceSections = new HashSet<Coord>();
        for (int y = minY; y <= maxY; y++)
        for (int x = minX; x <= maxX; x++)
        {
            if(fence.Contains(new Coord(x, y)) == false)
                continue;
            if (fence.Contains(new Coord(x + 1, y)) && fence.Contains(new Coord(x - 1, y)))
                straightFenceSections.Add(new Coord(x, y));
            if (fence.Contains(new Coord(x, y+1)) && fence.Contains(new Coord(x, y-1)))
                straightFenceSections.Add(new Coord(x, y));
        }
        // Console.WriteLine($"Fence for: {type}");
        // for(int y = minY; y <= maxY; y++)
        // {
        //     for (int x = minX; x <= maxX; x++)
        //     {
        //         if(straightFenceSections.Contains(new Coord(x, y)))
        //             Console.Write("*");
        //         else if(fence.Contains(new Coord(x, y)))
        //             Console.Write("#");
        //         else
        //             Console.Write(".");
        //     }
        //     Console.WriteLine();
        // }
        //
        // Console.WriteLine($"Fence {type} area {area} fence {fence.Count - straightFenceSections.Count}");

        return area * (fence.Count - straightFenceSections.Count);
    }

    private static bool IsValidAndSame(Coord coord, string[] map, char type)
    {
        return IsCoordValid(coord, map) && map.GetAt(coord) == type;
    }
    
    private static bool IsCoordValid(Coord coord, string[] map)
    {
        return coord.X >= 0 && coord.X < map[0].Length && coord.Y >= 0 && coord.Y < map.Length;
    }

}