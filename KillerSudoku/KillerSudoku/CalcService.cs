using System;
using System.Collections.Generic;
using System.Linq;

namespace KillerSudoku
{
    public interface ICalcService
    {
        List<int[]> GetCombinations(int total, int inMany, int[] exemptNumbers, int[] mustHave);
        int[] GetCommonNumbers(List<int[]> results, int[] mustHave);
    }
    public class CalcService : ICalcService
    {
        public List<int[]> GetCombinations(int total, int inMany, int[] exemptNumbers, int[] mustHave)
        {
            var array = GetRange(exemptNumbers);
            var results = new List<int[]>();
            GetCombinationsImplementation(array, total, inMany, 0, mustHave.ToList(), results, mustHave.Sum());
            return results;
        }
        public int[] GetCommonNumbers(List<int[]> results, int[] mustHave)
        {
            var length = results.Count;
            if (length < 2)
                return new int[0];

            var res = new List<int>();

            for (var i = 1; i <= 9; i++)
            {
                if (mustHave.Contains(i))
                    continue;
                if (results.Count(x => x.Contains(i)) == length)
                    res.Add(i);
            }
            return res.ToArray();
        }
        private int[] GetRange(int[] exemptNumbers)
        {
            var result = new List<int>();
            for (var i = 1; i <= 9; i++)
            {
                if (exemptNumbers.Contains(i))
                    continue;
                result.Add(i);
            }
            return result.ToArray();
        }

        private void GetCombinationsImplementation(int[] array, int total, int inMany, int point, List<int> found, List<int[]> results, int sum)
        {
            if (sum > total)
                return;

            if (inMany == found.Count)
            {
                if (sum == total)
                {
                    found.Sort();
                    results.Add(found.ToArray());
                }
                return;
            }

            for (var i = point; i < array.Length; i++)
            {
                if (found.Contains(array[i]))
                    continue;
                sum += array[i];
                var tempFound = new List<int>(found)
                {
                    array[i]
                };
                GetCombinationsImplementation(array, total, inMany, i + 1, tempFound, results, sum);
                sum -= array[i];
            }
        }
    }
}