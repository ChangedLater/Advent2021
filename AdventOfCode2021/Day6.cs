using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal class Day6
    {
        internal static void Part1(long[] sourceData) => CalculateDays(sourceData, 80);
        internal static void Part2(long[] sourceData) => CalculateDays(sourceData, 256);

        internal static void CalculateDays(long[] sourceData, int days)
        {
            var dataBuckets = LoadDataIntoBuckets(sourceData);
            for (int i = 0; i < days; i++)
            {
                dataBuckets = IncrementDay(dataBuckets);
            }
            var sum = dataBuckets.Sum();
            Console.WriteLine(sum);
        }

        public static long[] IncrementDay(long[] data)
        {
            var newData = new long[9];
            for (int i = 1; i < 9; i++)
            {
                newData[i-1] = data[i];
            }
            newData[8] = data[0];
            newData[6] += data[0];
            return newData;
        }


        public static long[] LoadDataIntoBuckets(long[] dataPoints)
        {
            var data = new long[9];
            foreach(var point in dataPoints)
            {
                data[point]++;
            }
            return data;
        }

        internal static long[] SampleData { get; set; } = new long[]
        {
            3,4,3,1,2
        };

        internal static long[] ActualData { get; set; } = new long[]
        {
            2,5,5,3,2,2,5,1,4,5,2,1,5,5,1,2,3,3,4,1,4,1,4,4,2,1,5,5,3,5,4,3,4,1,5,4,1,5,5,5,4,3,1,2,1,5,1,4,4,1,4,1,3,1,1,1,3,1,1,2,1,3,1,1,1,2,3,5,5,3,2,3,3,2,2,1,3,1,3,1,5,5,1,2,3,2,1,1,2,1,2,1,2,2,1,3,5,4,3,3,2,2,3,1,4,2,2,1,3,4,5,4,2,5,4,1,2,1,3,5,3,3,5,4,1,1,5,2,4,4,1,2,2,5,5,3,1,2,4,3,3,1,4,2,5,1,5,1,2,1,1,1,1,3,5,5,1,5,5,1,2,2,1,2,1,2,1,2,1,4,5,1,2,4,3,3,3,1,5,3,2,2,1,4,2,4,2,3,2,5,1,5,1,1,1,3,1,1,3,5,4,2,5,3,2,2,1,4,5,1,3,2,5,1,2,1,4,1,5,5,1,2,2,1,2,4,5,3,3,1,4,4,3,1,4,2,4,4,3,4,1,4,5,3,1,4,2,2,3,4,4,4,1,4,3,1,3,4,5,1,5,4,4,4,5,5,5,2,1,3,4,3,2,5,3,1,3,2,2,3,1,4,5,3,5,5,3,2,3,1,2,5,2,1,3,1,1,1,5,1
        };
    }
}
