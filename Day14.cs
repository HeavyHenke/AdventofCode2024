using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public class Day14
{
    public object Part1()
    {
        var lines = File.ReadAllLines("Day14.txt");
        var robots = new(int x, int y, int dx, int dy)[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            var m = Regex.Match(lines[i], @"p=(\d+),(\d+) v=(-?\d+),(-?\d+)");
            var x = int.Parse(m.Groups[1].Value);
            var y = int.Parse(m.Groups[2].Value);
            var dx = int.Parse(m.Groups[3].Value);
            var dy = int.Parse(m.Groups[4].Value);
            robots[i] = new (x, y, dx, dy);
        }

        const int maxx = 101;
        const int maxy = 103;

        for (int t = 0; t < 100; t++)
        {
            for (int r = 0; r < robots.Length; r++)
            {
                robots[r].x += robots[r].dx;
                robots[r].y += robots[r].dy;
                while(robots[r].x >= maxx)
                    robots[r].x -= maxx;
                while(robots[r].x < 0)
                    robots[r].x += maxx;
                while(robots[r].y >= maxy)
                    robots[r].y -= maxy;
                while(robots[r].y < 0)
                    robots[r].y += maxy;
            }
        }

        // for (int y = 0; y < maxy; y++)
        // {
        //     for (int x = 0; x < maxx; x++)
        //     {
        //         int num = robots.Count(r => r.y == y && r.x == x);
        //         Console.Write(num);
        //     }
        //     Console.WriteLine();
        // }

        var botsPerQuad = new int[4];
        botsPerQuad[0] = robots.Count(r => r is { y: < maxy / 2, x: < maxx / 2 });
        botsPerQuad[1] = robots.Count(r => r is { y: < maxy / 2, x: > maxx / 2 });
        botsPerQuad[2] = robots.Count(r => r is { y: > maxy / 2, x: < maxx / 2 });
        botsPerQuad[3] = robots.Count(r => r is { y: > maxy / 2, x: > maxx / 2 });
        
        return botsPerQuad[0]*botsPerQuad[1]*botsPerQuad[2]*botsPerQuad[3];
    }
}