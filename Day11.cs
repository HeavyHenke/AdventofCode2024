using System.IO;

namespace AdventOfCode2024;

public class Day11
{
    public object Part1()
    {
        var stones = File.ReadAllText("Day11.txt").Split(' ').Select(long.Parse).ToList();

        for (int i = 0; i < 25; i++)
            stones = Blink(stones);
        
        return stones.Count;
    }
    
    private List<long> Blink(List<long> stones)
    {
        var ret = new List<long>();

        foreach (var s in stones)
        {
            if(s ==0)
                ret.Add(1);
            else
            {
                var str = s.ToString();
                if (str.Length % 2 == 0)
                {
                    ret.Add(long.Parse(str[..(str.Length / 2)]));
                    ret.Add(long.Parse(str[(str.Length / 2)..]));
                }
                else
                    ret.Add(s * 2024);
            }
        }

        return ret;
    }
    
    public object Part2()
    {
        var stones = File.ReadAllText("Day11.txt").Split(' ').Select(long.Parse).ToList();

        var ret = stones.Select(s => NumberOfStonesAfterBlink(s, 75)).Sum();
        
        return ret;
    }

    private readonly Dictionary<(long stone, int numBlinks), long> _stonesAfterBlinks = new();  
    private long NumberOfStonesAfterBlink(long stone, int numBlinks)
    {
        if (numBlinks == 0)
            return 1;
        if (_stonesAfterBlinks.TryGetValue((stone, numBlinks), out var ret))
            return ret;


        if (stone == 0)
        {
            ret = NumberOfStonesAfterBlink(1, numBlinks - 1);
            _stonesAfterBlinks[(stone, numBlinks)] = ret;
            return ret;
        }
        
        var str = stone.ToString();
        if (str.Length % 2 == 0)
        {
            ret = NumberOfStonesAfterBlink(long.Parse(str[..(str.Length / 2)]), numBlinks - 1) +
                NumberOfStonesAfterBlink(long.Parse(str[(str.Length / 2)..]), numBlinks - 1);
            _stonesAfterBlinks[(stone, numBlinks)] = ret;
            return ret;
        }
        
        ret = NumberOfStonesAfterBlink(stone * 2024, numBlinks - 1);
        _stonesAfterBlinks[(stone, numBlinks)] = ret;
        return ret;
    }
}