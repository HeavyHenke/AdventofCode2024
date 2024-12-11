using System.IO;

namespace AdventOfCode2024;

public class Day7
{
    public object Part1()
    {
        long total = 0;

        foreach (var row in File.ReadAllLines("Day7.txt"))
        {
            var split1 = row.Split(':');
            var result = long.Parse(split1[0]);

            var operands = split1[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();

            if (IsCorrect1(result, 0, operands))
                total += result;

        }
        
        return total;
    }

    private static bool IsCorrect1(long result, long currentValue, Span<long> operands)
    {
        if (operands.Length == 0)
            return result == currentValue;
        return IsCorrect1(result, currentValue + operands[0], operands[1..]) ||
               IsCorrect1(result, currentValue * operands[0], operands[1..]);
    }
    
    public object Part2()
    {
        long total = 0;

        foreach (var row in File.ReadAllLines("Day7.txt"))
        {
            var split1 = row.Split(':');
            var result = long.Parse(split1[0]);

            var operands = split1[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();

            if (IsCorrect2(result, 0, operands))
                total += result;

        }
        
        return total;
    }
    
    private static bool IsCorrect2(long result, long currentValue, Span<long> operands)
    {
        if (operands.Length == 0)
            return result == currentValue;
        return IsCorrect2(result, currentValue + operands[0], operands[1..]) ||
               IsCorrect2(result, currentValue * operands[0], operands[1..]) ||
               IsCorrect2(result, long.Parse(currentValue.ToString() + operands[0]), operands[1..]);
    }
}