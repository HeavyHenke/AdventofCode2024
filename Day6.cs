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
                coord = new Coord{Y = y, X = ix};
                break;
            }
        }

        var visited = new HashSet<Coord>();
        var dir = 0;
        while (coord.IsInsideMap(map))
        {
            visited.Add(coord);

            var c2 = coord.MoveBy(_dirDelta[dir]);
            if (c2.IsInsideMap(map) == false)
                break;
            
            while (map[c2.Y][c2.X] == '#')
            {
                dir++;
                if (dir == 4)
                    dir = 0;
                c2 = coord.MoveBy(_dirDelta[dir]);
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
                startPos = new Coord{Y = y, X = ix};
                break;
            }
        }

        int numLoops = 0;
        for (var y = 0; y < map.Length; y++)
        for (int x = 0; x < map[y].Length; x++)
        {
            if (map[y][x] != '.')
                continue;

            if(IsLoop(map, startPos, new Coord{Y=y, X=x}))
                numLoops++;
        }

        return numLoops.ToString();
    }

    private bool IsLoop(string[] map, Coord coord, Coord extraObstacle)
    {
        var visited = new bool[4, map.Length, map[0].Length];
        
        var dir = 0;
        while (coord.IsInsideMap(map))
        {
            if (visited[dir, coord.Y, coord.X])
                return true;
            visited[dir, coord.Y, coord.X] = true;

            var c2 = coord.MoveBy(_dirDelta[dir]);
            if (c2.IsInsideMap(map) == false)
                return false;
            
            while (map[c2.Y][c2.X] == '#' || c2 == extraObstacle)
            {
                dir++;
                if (dir == 4)
                    dir = 0;
                c2 = coord.MoveBy(_dirDelta[dir]);
            }

            coord = c2;
        }

        return false;
    }
}

internal readonly record struct Coord
{
    public int X { get; init; }
    public int Y { get; init; }

    public bool IsInsideMap(string[] map)
    {
        if(X < 0 || Y < 0 || X >= map[0].Length || Y >= map.Length) 
            return false;
        return true;
    }

    public Coord MoveBy((int dx, int dy) dir)
    {
        return new Coord{X = X + dir.dx, Y = Y + dir.dy};
    }
}