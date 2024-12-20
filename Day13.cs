using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public class Day13
{
    public object? Part1()
    {
        var input = File.ReadAllLines("Day13.txt");
        var machines = new List<(int adx, int ady, int bdx, int bdy, int px, int py)>();
        for (int i = 0; i < input.Length; i++)
        {
            var m = Regex.Match(input[i++], @"Button [AB]: X\+(\d+), Y\+(\d+)");
            var adx = int.Parse(m.Groups[1].Value);
            var ady = int.Parse(m.Groups[2].Value);
            
            m = Regex.Match(input[i++], @"Button [AB]: X\+(\d+), Y\+(\d+)");
            var bdx = int.Parse(m.Groups[1].Value);
            var bdy = int.Parse(m.Groups[2].Value);

            m = Regex.Match(input[i++], @"Prize: X=(\d+), Y=(\d+)");
            var px = int.Parse(m.Groups[1].Value);
            var py = int.Parse(m.Groups[2].Value);

            machines.Add((adx, ady, bdx, bdy, px, py));
        }

        int ret = 0;
        foreach (var machine in machines)
        {
            int cost = 0;
            int x = 0, y = 0;
            while (true)
            {
                // Push B
                int numb = 0;
                while (x < machine.px && y < machine.py)
                {
                    x += machine.bdx;
                    y += machine.bdy;
                    cost++;
                    numb++;
                }

                if (x == machine.px && y == machine.py)
                {
                    ret += cost;
                    break;
                }

                int numa = 0;
                while (numb > 0)
                {
                    numb--;
                    cost--;
                    x -= machine.bdx;
                    y -= machine.bdy;
                    
                    while (x < machine.px && y < machine.py)
                    {
                        x += machine.adx;
                        y += machine.ady;
                        cost += 3;
                        numa++;
                    }
                    if (x == machine.px && y == machine.py)
                    {
                        ret += cost;
                        break;
                    }
                }

                break;
            }
        }
        
        
        return ret;
    }
    
    public object? Part2()
    {
        var input = File.ReadAllLines("Day13.txt");
        var machines = new List<(long adx, long ady, long bdx, long bdy, long px, long py)>();
        for (int i = 0; i < input.Length; i++)
        {
            var m = Regex.Match(input[i++], @"Button [AB]: X\+(\d+), Y\+(\d+)");
            var adx = int.Parse(m.Groups[1].Value);
            var ady = int.Parse(m.Groups[2].Value);
            
            m = Regex.Match(input[i++], @"Button [AB]: X\+(\d+), Y\+(\d+)");
            var bdx = int.Parse(m.Groups[1].Value);
            var bdy = int.Parse(m.Groups[2].Value);

            m = Regex.Match(input[i++], @"Prize: X=(\d+), Y=(\d+)");
            var px = long.Parse(m.Groups[1].Value) + 10000000000000;
            var py = long.Parse(m.Groups[2].Value) + 10000000000000;

            machines.Add((adx, ady, bdx, bdy, px, py));
        }

        long ret = 0;
        foreach (var machine in machines)
        {
            long cost = 0;
            long x = 0, y = 0;
            
            // Push B
            long numbX = machine.px / machine.bdx + 1;
            long numbY = machine.py / machine.bdy + 1;
                
            long numb = Math.Min(numbX, numbY);
            x = numb * machine.bdx;
            y = numb * machine.bdy;
            cost += numb;
                
            if (x == machine.px && y == machine.py)
            {
                ret += cost;
                break;
            }

            long numa = 0;
            while (numb > 0)
            {
                if (x == machine.px && y == machine.py)
                {
                    ret += cost;
                    break;
                }
                
                long numax = (machine.px - x + machine.adx - 1) / machine.adx;
                long numay = (machine.py - y + machine.ady - 1) / machine.ady;
                var deltaNuma = Math.Max(numax, numay);
                if (deltaNuma > 0)
                {
                    cost += 3 * deltaNuma;
                    x += deltaNuma * machine.adx;
                    y += deltaNuma * machine.ady;
                    numa += deltaNuma;

                    if (x == machine.px && y == machine.py)
                    {
                        ret += cost;
                        break;
                    }
                }
                
                numbX = (machine.px - x - machine.bdx + 1) / machine.bdx;
                numbY = (machine.py - y - machine.bdy + 1) / machine.bdy;
                var deltaNumb = -Math.Min(numbX, numbY);

                if (deltaNumb > 0)
                {
                    numb -= deltaNumb;
                    cost -= deltaNumb;
                    x -= deltaNumb * machine.bdx;
                    y -= deltaNumb * machine.bdy;
                }

                if (deltaNuma == 0 && deltaNumb == 0)
                    break;

            }
        }
        
        
        return ret;
    }

}