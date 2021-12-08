using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal static class Day4
    {
        internal static void Part1(string fileName)
        {
            Console.WriteLine(FindFirstWinningBoard(fileName));
        }

        internal static int FindFirstWinningBoard(string fileName)
        {
            var (nums, boards) = ReadFile(fileName);

            foreach (var num in nums)
            {
                foreach (var board in boards)
                {
                    if (board.HitNumber(num))
                    {
                        return board.GetScore(num);
                    }
                }
            }
            return -1;
        }

        internal static void Part2(string fileName)
        {
            var (nums, boards) = ReadFile(fileName);
            var lastScore = 0;

            foreach (var num in nums)
            {
                foreach (var board in boards.Where(b => !b.HasWon))
                {
                    if (board.HitNumber(num))
                    {
                        lastScore = board.GetScore(num);
                    }
                }
            }
            Console.WriteLine(lastScore);
        }

        public static (List<int>, List<Board>) ReadFile(string filename)
        {
            var lines = File.ReadAllLines(filename);
            var boards = new List<Board>();
            var nums = new List<int>();
            var currentBoard = new Board();
            var currentLine = 1;
            for(int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (i == 0)
                {
                    nums = line.Split(',').Select(n => int.Parse(n)).ToList();
                }
                else if(line != "")
                {
                    var numsOnLine = line.Split(" ").Where(S => S.Trim() != "").Select(n => int.Parse(n)).ToList();
                    for(int column = 0; column < numsOnLine.Count(); column++)
                    {
                        currentBoard.AddNum(numsOnLine[column], currentLine, column+1);
                    }
                    currentLine++;
                    if( currentLine > 5)
                    {
                        if (i > 1)
                        {
                            boards.Add(currentBoard);
                            currentBoard = new Board();
                            currentLine = 1;
                        }
                    }
                }
            }
            return (nums, boards);
        }


        internal class Board
        {
            public bool HasWon { get; set; }

            public Dictionary<int, (int x,int y)> NumToPosition { get; set; } = new Dictionary<int, (int x, int y)>();

            public Dictionary<int, int> RowScoreCount { get; set; } = Enumerable.Range(1, 5).ToDictionary(i=>i, _ => 0);
            public Dictionary<int, int> ColumnScoreCount { get; set; } = Enumerable.Range(1, 5).ToDictionary(i => i, _ => 0);

            public List<int> HitNumbers { get; set; } = new List<int>();

            public bool HitNumber(int number)
            {
                if (NumToPosition.ContainsKey(number))
                {
                    var (x,y) = NumToPosition[number];
                    RowScoreCount[x]++;
                    ColumnScoreCount[y]++;

                    NumToPosition.Remove(number);
                    HitNumbers.Add(number);
                    if (RowScoreCount[x] == 5 || ColumnScoreCount[y] == 5)
                    {
                        HasWon = true;
                    }
                }
                return HasWon;
            }

            public void AddNum(int num, int x, int y)
            {
                NumToPosition[num] = (x, y);
            }

            public int GetScore(int lastCalled)
            {
                var sumUnmarked = NumToPosition.Select(k => k.Key).Aggregate(0, (acc,v) => acc + v);
                return sumUnmarked * lastCalled;
            }
        }
    }
}
