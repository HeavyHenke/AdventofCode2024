using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public class Day3
{
    public string Part1()
    {
        var instructions = File.ReadAllText("Day3.txt");
        int sum = 0;

        var regex = new Regex(@"mul\((?<i1>\d{1,3}),(?<i2>\d{1,3})\)", RegexOptions.Singleline);
        foreach (Match match in regex.Matches(instructions))
        {
            var v1 = int.Parse(match.Groups["i1"].Value);
            var v2 = int.Parse(match.Groups["i2"].Value);
            sum += v1 * v2;
        }
        
        return sum.ToString();
    }
    
    public string Part2()
    {
        var instructions = File.ReadAllText("Day3.txt");
        int sum = 0;

        bool enable = true;
        var regex = new Regex(@"mul\((?<i1>\d{1,3}),(?<i2>\d{1,3})\)|(do\(\))|(don't\(\))");
        foreach (Match match in regex.Matches(instructions))
        {
            if (match.Groups[0].Value == "do()")
            {
                enable = true;
            }
            else if (match.Groups[0].Value == "don't()")
            {
                enable = false;
            }
            else if(enable)
            {
                var v1 = int.Parse(match.Groups["i1"].Value);
                var v2 = int.Parse(match.Groups["i2"].Value);
                sum += v1 * v2;
            }
        }
        
        return sum.ToString();
    }

}