using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal static class Day13
    {
        public static void Part1(string fileName, bool firstOnly = false)
        {
            var results = Fold(fileName, firstOnly);
            Console.WriteLine(results.Count);
        }

        public static void Part2(string fileName)
        {
            var results = Fold(fileName, false);
            // actually solved it by plotting the points in libre office & flipping vertically
            Console.WriteLine(results.Count);
        }

        public static HashSet<(int, int)> Fold(string fileName, bool firstOnly = false)
        {
            var sheet = ReadFile(fileName);
            var results = new HashSet<(int, int)>();
            int Fold(int value, int foldAt) => value > foldAt ? 2 * foldAt - value : value;
            foreach (var point in sheet.Points)
            {
                var x = point.x;
                var y = point.y;
                for (int i = 0; i < sheet.Folds.Count; i++)
                {
                    var (dir, fold) = sheet.Folds[i];
                    switch (dir)
                    {
                        case 'x':
                            x = Fold(x, fold);
                            break;
                        default:
                            y = Fold(y, fold);
                            break;
                    }
                    // part 1
                    if (firstOnly) break;
                }

                if (!results.Contains((x, y)))
                {
                    results.Add((x, y));
                }
            }
            return results;
        }

        private class Sheet
        {
            public List<Point> Points { get; set; } = new List<Point>();
            public List<(char, int)> Folds { get; set; } = new List<(char, int)>();
        }

        private class Point
        {
            public int x { get; set; }
            public int y { get; set; }
        }

        private static Sheet ReadFile(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            var sheet = new Sheet();

            for(var lineNum = 0; lineNum < lines.Length; lineNum++)
            {
                var line = lines[lineNum];
                if (line.StartsWith('f'))
                {
                    var parts = line.Split('=');
                    var value = int.Parse(parts[1]);
                    if(parts[0] == "fold along x") {
                        sheet.Folds.Add(('x',value));
                    }
                    else
                    {
                        sheet.Folds.Add(('y', value));
                    }
                } else if(line.Length > 0)
                {
                    var parts = line.Split(',');
                    sheet.Points.Add(new Point
                    {
                        x = int.Parse(parts[0]),
                        y = int.Parse(parts[1])
                    });
                }
            }
            return sheet;
        }
    }
}
