using System.IO;

namespace AdventOfCode2024;

public class Day4
{
    public string Part1()
    {
        var lines = File.ReadAllLines("Day4.txt");

        int num = 0;
        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[row].Length; col++)
            {
                num += GetStringsStartingHere(row, col, "XMAS", lines);
            }
        }
        
        return num.ToString();
    }
    
    private readonly (int dRow, int dCol)[] _directions =
    [
        (1,-1),(1,0),(1,1),(0,1),(0,-1),(-1,-1),(-1,0),(-1,1)
    ];
    
    private int GetStringsStartingHere(int row, int col, string searchFor, string[] lines)
    {
        int num = 0;
        foreach (var dir in _directions)
        {
            if (GetStringsStartingHereInDirection(row, col, dir, searchFor, lines))
                num++;
        }

        return num;
    }

    private bool GetStringsStartingHereInDirection(int row, int col, (int dRow, int dCol) dir, string searchFor,
        string[] lines)
    {
        for (int i = 0; i < searchFor.Length; i++)
        {
            if(row < 0 || row >= lines.Length ||col < 0 || col >= lines[row].Length)
                return false;
            
            if (lines[row][col] != searchFor[i])
                return false;
            row += dir.dRow;
            col += dir.dCol;
        }

        return true;
    }

    public string Part2()
    {
        var lines = File.ReadAllLines("Day4.txt");

        int num = 0;
        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[row].Length; col++)
            {
                if(IsXMax(row, col, lines))
                    num++;
            }
        }
        
        return num.ToString();
    }

    private bool IsXMax(int row, int col, string[] lines)
    {
        var first = GetThreeCarsDownRight(row, col, lines);
        if (first != "MAS" && first != "SAM")
            return false;
        var second = GetThreeCarsDownLeft(row, col + 2, lines);
        if (second != "MAS" && second != "SAM")
            return false;
        return true;
    }

    private static string GetThreeCarsDownRight(int row, int col, string[] lines)
    {
        string str = "";
        for (int i = 0; i < 3; i++)
        {
            if (row > lines.Length - 1 || col > lines.Length - 1)
                return "";
            str += lines[row][col];

            row++;
            col++;
        }

        return str;
    }
    
    private static string GetThreeCarsDownLeft(int row, int col, string[] lines)
    {
        string str = "";
        for (int i = 0; i < 3; i++)
        {
            if (row > lines.Length - 1 || col < 0)
                return "";
            str += lines[row][col];

            row++;
            col--;
        }

        return str;
    }
}