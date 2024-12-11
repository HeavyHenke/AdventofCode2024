using System.IO;

namespace AdventOfCode2024;

public class Day2
{
    public string Part1()
    {
        var lines = File.ReadAllLines("Day2.txt")
            .Select(l => l.Split(' ').Select(int.Parse).ToArray()).ToArray();
        
        var numSafe = lines.Count(l => IsSafeAscending(l) || IsSafeDescending(l));
        
        return numSafe.ToString();
    }

    private static bool IsSafeAscending(IList<int> numbers)
    {
        for (int i = 1; i < numbers.Count; i++)
        {
            if (numbers[i] <= numbers[i - 1])
                return false;
            if (numbers[i] > numbers[i - 1] + 3)
                return false;
        }

        return true;
    }

    private static bool IsSafeDescending(IList<int> numbers)
    {
        for (int i = 1; i < numbers.Count; i++)
        {
            if (numbers[i] >= numbers[i - 1])
                return false;
            if (numbers[i] < numbers[i - 1] - 3)
                return false;
        }

        return true;
    }

    
    public string Part2()
    {
        var lines = File.ReadAllLines("Day2.txt")
            .Select(l => l.Split(' ').Select(int.Parse).ToArray());
        
        var numSafe = lines.Count(IsSafeWithDamper);
        return numSafe.ToString();
    }

    private static bool IsSafeWithDamper(int[] numbers)
    {
        if (IsSafeAscending(numbers) || IsSafeDescending(numbers))
            return true;

        for (int i = 0; i < numbers.Length; i++)
        {
            var damped = numbers.ToList();
            damped.RemoveAt(i);
            
            if (IsSafeAscending(damped) || IsSafeDescending(damped))
                return true;
        }
        return false;
    }
    


}