using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public static class Day8
    {
        public static void Part1(string fileName)
        {
            var count = 0;
            foreach (var pattern in ReadFile(fileName))
            {
                foreach(var output in pattern.Outputs)
                {
                    //is it 1, 4, 7, 8
                    if( output.Length == 2 || output.Length == 4 || output.Length == 3 || output.Length == 7)
                    {
                        count++;
                    }
                }
            }
            Console.WriteLine(count);
        }

        public static void Part2(string fileName)
        {
            var sum = 0.0;
            foreach (var pattern in ReadFile(fileName))
            {
                var knownPatterns = new char[]?[10];
                // get the easy ones
                var found = new List<string>();
                foreach (var signal in pattern.Patterns)
                {
                    switch (signal.Length)
                    {
                        case 2:
                            knownPatterns[1] = signal.ToCharArray();
                            found.Add(signal);
                            break;
                        case 4:
                            knownPatterns[4] = signal.ToCharArray();
                            found.Add(signal);
                            break;
                        case 3:
                            knownPatterns[7] = signal.ToCharArray();
                            found.Add(signal);
                            break;
                        case 7:
                            knownPatterns[8] = signal.ToCharArray();
                            found.Add(signal);
                            break;
                    }
                }
                var patterns = pattern.Patterns.Where(p => !found.Contains(p)).Select(c => (c, c.ToCharArray()));
                var three = patterns.Where(p => p.Item2.Count() == 5 && p.Item2.Where(i => !knownPatterns[7]!.Contains(i)).Count() == 2).First();
                knownPatterns[3] = three.Item2;
                patterns = patterns.Where(p => p.c != three.c);

                var nine = patterns.Where(p => p.Item2.Count() == 6 && p.Item2.Where(i => !knownPatterns[3]!.Contains(i)).Count() == 1).First();
                knownPatterns[9] = nine.Item2;
                patterns = patterns.Where(p => p.c != nine.c);

                // complete overlap of 7 in 0
                var zero = patterns.Where(p => p.Item2.Intersect(knownPatterns[7]!).Count() == 3).First();
                knownPatterns[0] = zero.Item2;
                patterns = patterns.Where(p => p.c != zero.c);

                var six = patterns.Where(p => p.Item2.Count() == 6).First();
                knownPatterns[6] = six.Item2;
                patterns = patterns.Where(p => p.c != six.c);

                var five = patterns.Where(p => p.Item2.Intersect(knownPatterns[9]!).Count() == 5).First();
                knownPatterns[5] = five.Item2;

                knownPatterns[2] = patterns.First(p => p.c != five.c).Item2;

                Dictionary<string, int> values = new Dictionary<string, int>();
                for(int i =0; i < knownPatterns.Count(); i++)
                {
                    var str = string.Join("", knownPatterns[i]!.OrderBy(c => c));
                    values[str] = i;
                }

                var localSum = 0.0;
                for(int i = 0; i < pattern.Outputs.Count(); i++)
                {
                    var output = string.Join("", pattern.Outputs[i].ToCharArray()!.OrderBy(c => c));
                    var value = values[output];
                    localSum += value * Math.Pow(10, 3-i);
                }
                Console.WriteLine(localSum);
                sum += localSum;
            }
            Console.WriteLine($"total sum:{sum}\n\n\n\n");
        }

        public static List<SignalPattern> ReadFile(string filename)
        {
            var lines = File.ReadAllLines(filename);
            var patterns = new List<SignalPattern>();
            foreach(var line in lines)
            {
                var tokens = line.Split(' ');
                var pattern = new SignalPattern();
                for(var i = 0; i < tokens.Length; i++)
                {
                    var token = tokens[i];
                    if (token == "|") continue;
                    else if (i < 10) pattern.Patterns.Add(token);
                    else pattern.Outputs.Add(token);
                }
                patterns.Add(pattern);
            }
            return patterns;
        }

        public class SignalPattern
        {
            public List<string> Patterns { get; set; } = new List<string>();
            public List<string> Outputs { get; set; } = new List<string>();
        }
    }
}
