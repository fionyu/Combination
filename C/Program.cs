using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace C
{
    class Program
    {
        public static readonly string ProjectPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));

        static readonly int[] inputA = new int[] { 100, 101, 102, 103 };
        static readonly int[] inputB = new int[] { 100, 101, 102, 103 };
        static readonly int[] inputC = new int[] { 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131 };

        static void Main(string[] args)
        {

            //var minimunSum = 304;
            //StreamWriter writer = new StreamWriter(Path.Combine(ProjectPath, "log.txt"));
            //for (int i = 1; i <= 16; i++)
            //{
            //    var sw = new Stopwatch();
            //    sw.Start();
            //    long totalCount = 0;

            //    writer.WriteLine($"C({inputC.Length}, {i}) x C({inputA.Length}, 2) x C({inputB.Length}, 2)");
            //    Console.WriteLine($"C({inputC.Length}, {i}) x C({inputA.Length}, 2) x C({inputB.Length}, 2)");

            //    foreach (var CA in C(inputA, 2))
            //    {
            //        foreach (var CB in C(inputB, 2))
            //        {
            //            foreach (var CC in C(inputC, i))
            //            {
            //                totalCount++;
            //                if (totalCount % 10000000 == 0)
            //                {
            //                    Console.Write(".");
            //                }
            //            }
            //        }
            //    }
            //    sw.Stop();

            //    writer.WriteLine();
            //    writer.WriteLine($"TotalConbinationCount: {totalCount}");
            //    writer.WriteLine($"End: {sw.Elapsed.TotalSeconds} seconds");
            //    writer.WriteLine($"======================");
            //    writer.Flush();

            //    Console.WriteLine();
            //    Console.WriteLine($"TotalConbinationCount: {totalCount}");
            //    Console.WriteLine($"End: {sw.Elapsed.TotalSeconds} seconds");
            //    Console.WriteLine($"======================");
            //}

            Test();
        }

        static IEnumerable<int[]> C(int[] nums, int count)
        {

            int[] indexes = new int[count];
            Init(indexes);

            int numsLength = nums.Length;
            int indexsLength = indexes.Length;

            int matchedLast = 0;
            while (true)
            {
                // 若 index 還能移動
                if (matchedLast > 0 && matchedLast < indexsLength)
                {
                    // 中間的 target index 往右一步
                    indexes[indexsLength - matchedLast - 1]++;
                    for (int i = indexsLength - matchedLast; i < indexsLength; i++)
                    {
                        // 中間的 arget index 之後的 index 都往 target index 右邊一步
                        indexes[i] = indexes[i - 1] + 1;
                    }

                    matchedLast = 0;
                }

                // 計算有幾個 index 已經到最後面不能再移動了
                for (int i = indexsLength - 1; i >= 0; i--)
                {
                    if (indexes[i] == (numsLength - (indexsLength - i)))
                    {
                        matchedLast++;
                    }
                    else
                    {
                        break;
                    }
                }

                // 拋出每一次結果
                yield return GetResult(nums, indexes);

                // 最後一個 index
                indexes[indexsLength - 1]++;

                if (matchedLast == indexsLength)
                    break;
            }
        }

        private static int[] GetResult(int[] nums, int[] indexes)
        {
            var indexsLength = indexes.Length;
            int[] result = new int[indexsLength];
            for (int i = 0; i < indexsLength; i++)
            {
                result[i] = nums[indexes[i]];
            }
            return result;
        }

        private static void Init(int[] indexes)
        {
            for (int i = 0; i < indexes.Length; i++)
                indexes[i] = i;
        }

        private static void Test()
        {
            int totalCount;
            int matchedCount;

            for (int i = 1; i <= inputC.Length; i++)
            {
                Console.WriteLine($"C({inputC.Length}, {i})");

                totalCount = 0;
                matchedCount = 0;

                var sw = new Stopwatch();
                sw.Start();

                foreach (var c in C(inputC, i))
                {
                    totalCount++;
                    if (totalCount % 1000000 == 0)
                    {
                        Console.Write(".");
                    }
                }

                sw.Stop();
                Console.WriteLine();
                Console.WriteLine($"TotalConbinationCount: {totalCount}");
                Console.WriteLine($"TotalMatchedCount: {matchedCount}");
                Console.WriteLine($"End: {sw.Elapsed.TotalSeconds} seconds");
                Console.WriteLine($"======================");
            }
        }
    }
}
