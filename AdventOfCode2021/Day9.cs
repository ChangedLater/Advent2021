using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal static class Day9
    {
        public static void Part1(string fileName)
        {
            var values = ReadFile(fileName);
            var riskSum = 0;
            for(int y = 0; y < values.Length; y++)
            {
                for (int x = 0; x < values[y].Length; x++)
                {
                    if (IsLowPoint(values, x, y))
                    {
                        //Console.WriteLine($"x:{x} y:{y} value:{height}");
                        riskSum += values[y][x] + 1;
                    }
                }
            }
            Console.WriteLine(riskSum);
        }

        public static void Part2(string fileName)
        {
            var values = ReadFile(fileName);

            var largest = 0;
            var largest2 = 0;
            var largest3 = 0;

            for (int y = 0; y < values.Length; y++)
            {
                for (int x = 0; x < values[y].Length; x++)
                {
                    if (IsLowPoint(values, x, y))
                    {
                        var size = CalculateNeigbourBasin(values, x, y, 0, new HashSet<(int, int)>());
                        if(size >= largest)
                        {
                            largest3 = largest2;
                            largest2 = largest;
                            largest = size;
                        }
                        else if (size >= largest2)
                        {
                            largest3 = largest2;
                            largest2 = size;
                        }
                        else if (size >= largest3)
                        {
                            largest3 = size;
                        }
                    }
                }
            }
            Console.WriteLine(largest * largest2 * largest3);
        }

        public static bool IsLowPoint(int[][] values, int x, int y)
        {
            var height = values[y][x];
            var above = y > 0 ? values[y-1][x] : int.MaxValue;
            var below = y < values.Length - 1 ? values[y+1][x] : int.MaxValue;
            var left = x > 0 ? values[y][x-1] : int.MaxValue;
            var right = x < values[y].Length - 1 ? values[y][x+1] : int.MaxValue;
            return height < above && height < below && height < left && height < right;
        }

        public static int CalculateNeigbourBasin(int[][] values, int x, int y, int count, HashSet<(int, int)> checkedPoints)
        {
            if (checkedPoints.Contains((x, y))) return count;
            checkedPoints.Add((x, y));
            if (y < 0 || y > values.Length - 1 || x < 0 || x > values[y].Length -1) return count;
            if (values[y][x] == 9) return count;
            // add this point
            count++;
            // add neighbours
            count = CalculateNeigbourBasin(values, x - 1, y, count, checkedPoints);
            count = CalculateNeigbourBasin(values, x + 1, y, count, checkedPoints);
            count = CalculateNeigbourBasin(values, x, y - 1, count, checkedPoints);
            count = CalculateNeigbourBasin(values, x, y + 1, count, checkedPoints);
            return count;
        }

        public static int[][] ReadFile(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            var values = new int[lines.Length][];
            for(var lineNum = 0; lineNum < lines.Length; lineNum++)
            {
                values[lineNum] = lines[lineNum].ToCharArray().Select(c => c - '0').ToArray();
            }
            return values;
        }
    }
}
