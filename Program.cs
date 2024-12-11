﻿using System.Diagnostics;

namespace AdventOfCode2024;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        var sw = Stopwatch.StartNew();

        var result = new Day6().Part2();

        sw.Stop();

        Console.WriteLine($"Done, it took {sw.Elapsed}, answer:");
        Console.WriteLine(result);

        Console.WriteLine("");
        Console.WriteLine("Press C to copy to clipboard");
        var key = Console.ReadKey();
        if(key.Key == ConsoleKey.C)
            System.Windows.Clipboard.SetText(result);
        Console.WriteLine();
        Console.WriteLine("Exited");
    }
}