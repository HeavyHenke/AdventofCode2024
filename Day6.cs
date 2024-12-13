using System.IO;

namespace AdventOfCode2024;

public class Day6
{
    private readonly (int dx, int dy)[] _dirDelta =
    [
        (0, -1),
        (1, 0),
        (0, 1),
        (-1, 0)
    ];

    public string Part1()
    {
        string[] map = File.ReadAllLines("Day6.txt");
        Coord coord = new Coord();
        for (int y = 0; y < map.Length; y++)
        {
            var ix = map[y].IndexOf('^');
            if (ix != -1)
            {
                coord = new Coord(ix, y);
                break;
            }
        }

        var visited = new HashSet<Coord>();
        var dir = 0;
        while (coord.IsInsideMap(map))
        {
            visited.Add(coord);

            (int dx, int dy) dir1 = _dirDelta[dir];
            var c2 = new Coord(coord.X + dir1.dx, coord.Y + dir1.dy);
            if (c2.IsInsideMap(map) == false)
                break;
            
            while (map[c2.Y][c2.X] == '#')
            {
                dir++;
                if (dir == 4)
                    dir = 0;
                (int dx, int dy) dir2 = _dirDelta[dir];
                c2 = new Coord(coord.X + dir2.dx, coord.Y + dir2.dy);
            }

            coord = c2;
        }
        
        
        return visited.Count.ToString();
    }

    public string Part2()
    {
        string[] map = File.ReadAllLines("Day6.txt");
        Coord startPos = new Coord();
        for (int y = 0; y < map.Length; y++)
        {
            var ix = map[y].IndexOf('^');
            if (ix != -1)
            {
                startPos = new Coord(ix, y);
                break;
            }
        }

        int numLoops = 0;
        Enumerable.Range(0, map.Length).AsParallel().ForAll(y =>
        {
            var visited = new bool[4, map.Length, map[y].Length];
            for (int x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] != '.' && IsLoop(map, startPos, new Coord(x, y), visited)) 
                    Interlocked.Increment(ref numLoops);
            }
        });
        return numLoops.ToString();
    }

    private bool IsLoop(string[] map, Coord coord, Coord extraObstacle, bool[,,] visited)
    {
        Array.Clear(visited);
        
        var dir = 0;
        while (true)
        {
            if (visited[dir, coord.Y, coord.X])
                return true;
            visited[dir, coord.Y, coord.X] = true;

            var dir1 = _dirDelta[dir];
            var c2 = new Coord(coord.X + dir1.dx, coord.Y + dir1.dy);
            if (c2.IsInsideMap(map) == false)
                return false;
            
            while (map[c2.Y][c2.X] == '#' | c2 == extraObstacle)
            {
                dir++;
                if (dir == 4)
                    dir = 0;
                var delta = _dirDelta[dir];
                c2 = new Coord(coord.X + delta.dx, coord.Y + delta.dy);
            }

            coord = c2;
        }
    }
}

internal readonly record struct Coord
{
    public readonly int X;
    public readonly int Y;

    public Coord(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public bool IsInsideMap(string[] map)
    {
        return X >= 0 && Y >= 0 && Y < map.Length && X < map[0].Length;
    }

    public static Coord operator +(Coord a, (int dx, int dy) b)
    {
        return new Coord(a.X + b.dx, a.Y + b.dy);
    }
    
    public static Coord operator -(Coord a, (int dx, int dy) b)
    {
        return new Coord(a.X - b.dx, a.Y - b.dy);
    }
}