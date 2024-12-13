using System.IO;

namespace AdventOfCode2024;

public class Day9
{
    public object Part1()
    {
        var input = File.ReadAllText("Day9.txt");

        var nodes = new List<Node>();
        int id = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (i % 2 == 0)
            {
                nodes.Add(new Node { Id = id++, Length = input[i] - '0' });
            }
            else
            {
                nodes.Add(new Node { Id = -1, Length = input[i] - '0' });
            }
        }

        while (true)
        {
            var firstEmpty = nodes.FindIndex(n => n.Id == -1);
            if (firstEmpty == -1)
                break;

            if (nodes[^1].Length == nodes[firstEmpty].Length)
            {
                nodes[firstEmpty].Id = nodes[^1].Id;
                nodes.RemoveAt(nodes.Count - 1);
            }
            else if (nodes[firstEmpty].Length < nodes[^1].Length)
            {
                nodes[firstEmpty].Id = nodes[^1].Id;
                nodes[^1].Length -= nodes[firstEmpty].Length;
            }
            else
            {
                var emptyLeft = nodes[firstEmpty].Length - nodes[^1].Length;

                nodes[firstEmpty].Id = nodes[^1].Id;
                nodes[firstEmpty].Length = nodes[^1].Length;
                nodes.RemoveAt(nodes.Count - 1);
                nodes.Insert(firstEmpty + 1, new Node { Id = -1, Length = emptyLeft });
            }

            while (nodes[^1].Id == -1)
                nodes.RemoveAt(nodes.Count - 1);
        }

        int pos = 0;
        long checksum = 0;
        foreach (var node in nodes)
        {
            for (int i = 0; i < node.Length; i++)
            {
                checksum += pos * node.Id;
                pos++;
            }
        }


        return checksum;
    }

    private static void Print(List<Node> nodes)
    {
        foreach (var node in nodes)
        {
            for (int i = 0; i < node.Length; i++)
            {
                if (node.Id == -1)
                    Console.Write('.');
                else
                    Console.Write(node.Id);
            }
        }
    }

    public object Part2()
    {
        var input = File.ReadAllText("Day9.txt");

        var nodes = new LinkedList<Node>();
        int id = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (i % 2 == 0)
            {
                nodes.AddLast(new Node { Id = id++, Length = input[i] - '0' });
            }
            else
            {
                nodes.AddLast(new Node { Id = -1, Length = input[i] - '0' });
            }
        }
        
        var node = nodes.Last;
        while (node != null)
        {
            if (node.Value.Id == -1)
            {
                node = node.Previous;
                continue;
            }
            
            var emptyFinder = nodes.First;
            while (emptyFinder != null)
            {
                if (emptyFinder == node)
                {
                    emptyFinder = null;
                    break;
                }
                if (emptyFinder.Value.Id == -1 && emptyFinder.Value.Length >= node.Value.Length)
                    break;
                emptyFinder = emptyFinder.Next;
            }

            if (emptyFinder != null)
            {
                var leftFree = emptyFinder.Value.Length - node.Value.Length;
                emptyFinder.Value.Id = node.Value.Id;
                if (leftFree > 0)
                {
                    emptyFinder.Value.Length = node.Value.Length;
                    var empt2 = new Node { Id = -1, Length = leftFree };
                    nodes.AddAfter(emptyFinder, empt2);
                }
                node.Value.Id = -1;
                node = node.Previous;
            }
            else
            {
                node = node.Previous;
            }
        }


        int pos = 0;
        long checksum = 0;
        foreach (var checksumNode in nodes)
        {
            for (int i = 0; i < checksumNode.Length; i++)
            {
                if (checksumNode.Id != -1)
                    checksum += pos * checksumNode.Id;
                pos++;
            }
        }


        return checksum;
    }
    
    class Node
    {
        public int Id;
        public int Length;
    }
}