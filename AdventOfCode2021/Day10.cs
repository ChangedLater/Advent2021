using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal static class Day10
    {
        public static void Part1(string fileName)
        {
            var lines = ReadFile(fileName);
            int score = 0;
            foreach (var line in lines)
            {
                score += ProcessLinePart1(line);
            }
            Console.WriteLine(score);
        }

        public static int ProcessLinePart1(char[] line)
        {
            var stack = new Stack<char>();
            for (var i = 0; i < line.Length; i++)
            {
                var currentChar = line[i];
                switch (currentChar)
                {
                    case ')':
                        if (stack.Pop() != '(')
                        {
                            return 3;
                        }
                        break;
                    case ']':
                        if (stack.Pop() != '[')
                        {
                            return 57;
                        }
                        break;
                    case '}':
                        if (stack.Pop() != '{')
                        {
                            return 1197;
                        }
                        break;
                    case '>':
                        if (stack.Pop() != '<')
                        {
                            return 25137;
                        }
                        break;
                    default:
                        stack.Push(currentChar);
                        break;
                }
            }
            return 0;
        }

        public static void Part2(string fileName)
        {
            var lines = ReadFile(fileName);
            var scores = new List<long>();
            foreach (var line in lines)
            {
                var score = ProcessLinePart2(line);
                if(score > 0)
                {
                    scores.Add(score);
                }
            }
            scores.Sort();
            Console.WriteLine(scores[scores.Count()/2]);
        }

        public static long ProcessLinePart2(char[] line)
        {
            var stack = new Stack<char>();
            long score = 0;
            for (var i = 0; i < line.Length; i++)
            {
                var currentChar = line[i];
                switch (currentChar)
                {
                    case ')':
                        if (stack.Pop() != '(')
                        {
                            return 0;
                        }
                        break;
                    case ']':
                        if (stack.Pop() != '[')
                        {
                            return 0;
                        }
                        break;
                    case '}':
                        if (stack.Pop() != '{')
                        {
                            return 0;
                        }
                        break;
                    case '>':
                        if (stack.Pop() != '<')
                        {
                            return 0;
                        }
                        break;
                    default:
                        stack.Push(currentChar);
                        break;
                }
            }
            while(stack.Count > 0)
            {
                score *= 5;
                score += Part2Scores[stack.Pop()];
            }
            return score;
        }

        private static Dictionary<char, int> Part2Scores = new Dictionary<char, int>
        {
            { '(', 1 },
            { '[', 2 },
            { '{', 3 },
            { '<', 4 },
        };

        public static char[][] ReadFile(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            var values = new char[lines.Length][];
            for(var lineNum = 0; lineNum < lines.Length; lineNum++)
            {
                values[lineNum] = lines[lineNum].ToCharArray();
            }
            return values;
        }
    }
}
