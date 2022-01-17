﻿using KillerSudoku;
using Xunit;

namespace TestKillerSudoku
{
    public class CalcServiceTests
    {
        private ICalcService _calcService = new CalcService();

        [Theory]
        [InlineData(17,8,9)]
        [InlineData(16,7,9)]
        [InlineData(3,1,2)]
        [InlineData(4,1,3)]
        public void TestSingleResultReturn(int number, int res1, int res2)
        {
            var result = _calcService.GetCombinations(number, 2, new int[0], new int[0]).First(); 
            Assert.Contains(res1, result);  
            Assert.Contains(res2, result);  
        }

        [Fact]
        public void TestWithNotInValue()
        {
            var result = _calcService.GetCombinations(8, 3, new int[] {2}, new int[0]).First(); 
            Assert.Contains(1, result);
            Assert.Contains(3, result);
            Assert.Contains(4, result);
        }
        [Fact]
        public void TestWithManditoryValue()
        {
            var result = _calcService.GetCombinations(8, 3, new int[0], new int[] { 2 }).First(); 
            Assert.Contains(1, result);
            Assert.Contains(2, result);
            Assert.Contains(5, result);
        }

        [Fact]
        public void TestLargeNumberOfReturns()
        {
            var result = _calcService.GetCombinations(20, 4, new int[0], new int[0]);
            var simplifiedList = new List<string>();
            foreach (var res in result)
            {
                simplifiedList.Add(String.Join(",", res));
            }
            Assert.Equal(12, result.Count);
            Assert.Contains("1,2,8,9", simplifiedList);
            Assert.Contains("1,3,7,9", simplifiedList);
            Assert.Contains("1,4,6,9", simplifiedList);
            Assert.Contains("1,4,7,8", simplifiedList);
            Assert.Contains("1,5,6,8", simplifiedList);
            Assert.Contains("2,3,6,9", simplifiedList);
            Assert.Contains("2,3,7,8", simplifiedList);
            Assert.Contains("2,4,5,9", simplifiedList);
            Assert.Contains("2,4,6,8", simplifiedList);
            Assert.Contains("2,5,6,7", simplifiedList);
            Assert.Contains("3,4,5,8", simplifiedList);
            Assert.Contains("3,4,6,7", simplifiedList);
        }
        [Fact]
        public void TestLargeNumberOfReturnsWithNoIn()
        {
            var result = _calcService.GetCombinations(20, 4, new int[] {1,2}, new int[0]);
            var simplifiedList = new List<string>();
            foreach (var res in result)
            {
                simplifiedList.Add(String.Join(",", res));
            }
            Assert.Equal(2, result.Count);
            Assert.Contains("3,4,5,8", simplifiedList);
            Assert.Contains("3,4,6,7", simplifiedList);
        }
        [Fact]
        public void TestLargeNumberOfReturnsWithMustHave()
        {
            var result = _calcService.GetCombinations(20, 4, new int[0], new int[] {1,9});
            var simplifiedList = new List<string>();
            foreach (var res in result)
            {
                simplifiedList.Add(String.Join(",", res));
            }
            Assert.Equal(3, result.Count);
            Assert.Contains("1,2,8,9", simplifiedList);
            Assert.Contains("1,3,7,9", simplifiedList);
            Assert.Contains("1,4,6,9", simplifiedList);
        }
        [Fact]
        public void TestLargeNumberOfReturnsWithMustHaveAndNotIt()
        {
            var result = _calcService.GetCombinations(20, 4, new int[] {3,4,6}, new int[] { 1, 9 });
            var simplifiedList = new List<string>();
            foreach (var res in result)
            {
                simplifiedList.Add(String.Join(",", res));
            }
            Assert.Single(result);
            Assert.Contains("1,2,8,9", simplifiedList);
        }
        [Fact]
        public void TestCommonNumbers()
        {
            var result = _calcService.GetCombinations(8, 3, new int[0], new int[0]);
            var common = _calcService.GetCommonNumbers(result, new int[0]);
            
            Assert.Single(common);
            Assert.Contains(1, common);
        }

        [Fact]
        public void TestNoCommonNumbersForOneResult()
        {
            var result = _calcService.GetCombinations(3, 2, new int[0], new int[0]);
            var common = _calcService.GetCommonNumbers(result, new int[0]);

            Assert.True(common.Length == 0);
        }

        [Fact]
        public void TestAllCombinations()
        {
            var dic = new Dictionary<string, string[]>
            {
                { "3,2", new[] { "1,2" } },
                { "4,2", new[] { "1,3" } },
                { "5,2", new[] { "1,4", "2,3" } },
                { "6,2", new[] { "1,5", "2,4" } },
                { "6,3", new[] { "1,2,3" } },
                { "7,2", new[] { "1,6", "2,5", "3,4" } },
                { "7,3", new[] { "1,2,4" } },
                { "8,2", new[] { "1,7", "2,6", "3,5" } },
                { "8,3", new[] { "1,2,5", "1,3,4" } },
                { "9,2", new[] { "1,8", "2,7", "3,6", "4,5" } },
                { "9,3", new[] { "1,2,6", "1,3,5", "2,3,4" } },
                { "10,2", new[] { "1,9", "2,8", "3,7", "4,6" } },
                { "10,3", new[] { "1,2,7", "1,3,6", "1,4,5", "2,3,5" } },
                { "10,4", new[] { "1,2,3,4" } },
                { "11,2", new[] { "2,9", "3,8", "4,7", "5,6" } },
                { "11,3", new[] { "1,2,8", "1,3,7", "1,4,6", "2,3,6", "2,4,5" } },
                { "11,4", new[] { "1,2,3,5" } },
                { "12,2", new[] { "3,9", "4,8", "5,7" } },
                { "12,3", new[] { "1,2,9", "1,3,8", "1,4,7", "1,5,6", "2,3,7", "2,4,6", "3,4,5" } },
                { "12,4", new[] { "1,2,3,6", "1,2,4,5" } },
                { "13,2", new[] { "4,9", "5,8", "6,7" } },
                { "13,3", new[] { "1,3,9", "1,4,8", "1,5,7", "2,3,8", "2,4,7", "2,5,6", "3,4,6" } },
                { "13,4", new[] { "1,2,3,7", "1,2,4,6", "1,3,4,5" } },
                { "14,2", new[] { "5,9", "6,8" } },
                { "14,3", new[] { "1,4,9", "1,5,8", "1,6,7", "2,3,9", "2,4,8", "2,5,7", "3,4,7", "3,5,6" } },
                { "14,4", new[] { "1,2,3,8", "1,2,4,7", "1,2,5,6", "1,3,4,6", "2,3,4,5" } },
                { "15,2", new[] { "6,9", "7,8" } },
                { "15,3", new[] { "1,5,9", "1,6,8", "2,4,9", "2,5,8", "2,6,7", "3,4,8", "3,5,7", "4,5,6" } },
                { "15,4", new[] { "1,2,3,9", "1,2,4,8", "1,2,5,7", "1,3,4,7", "1,3,5,6", "2,3,4,6" } },
                { "15,5", new[] { "1,2,3,4,5" } },
                { "16,2", new[] { "7,9" } },
                { "16,3", new[] { "1,6,9", "1,7,8", "2,5,9", "2,6,8", "3,4,9", "3,5,8", "3,6,7", "4,5,7" } },
                { "16,4", new[] { "1,2,4,9", "1,2,5,8", "1,2,6,7", "1,3,4,8", "1,3,5,7", "1,4,5,6", "2,3,4,7", "2,3,5,6" } },
                { "16,5", new[] { "1,2,3,4,6" } },
                { "17,2", new[] { "8,9" } },
                { "17,3", new[] { "1,7,9", "2,6,9", "2,7,8", "3,5,9", "3,6,8", "4,5,8", "4,6,7" } },
                { "17,4", new[] { "1,2,5,9", "1,2,6,8", "1,3,4,9", "1,3,5,8", "1,3,6,7", "1,4,5,7", "2,3,4,8", "2,3,5,7", "2,4,5,6" } },
                { "17,5", new[] { "1,2,3,4,7", "1,2,3,5,6" } },
                { "18,3", new[] { "1,8,9", "2,7,9", "3,6,9", "3,7,8", "4,5,9", "4,6,8", "5,6,7" } },
                { "18,4", new[] { "1,2,6,9", "1,2,7,8", "1,3,5,9", "1,3,6,8", "1,4,5,8", "1,4,6,7", "2,3,4,9", "2,3,5,8", "2,3,6,7", "2,4,5,7", "3,4,5,6" } },
                { "18,5", new[] { "1,2,3,4,8", "1,2,3,5,7", "1,2,4,5,6" } },
                { "19,3", new[] { "2,8,9", "3,7,9", "4,6,9", "4,7,8", "5,6,8" } },
                { "19,4", new[] { "1,2,7,9", "1,3,6,9", "1,3,7,8", "1,4,5,9", "1,4,6,8", "1,5,6,7", "2,3,5,9", "2,3,6,8", "2,4,5,8", "2,4,6,7", "3,4,5,7" } },
                { "19,5", new[] { "1,2,3,4,9", "1,2,3,5,8", "1,2,3,6,7", "1,2,4,5,7", "1,3,4,5,6" } },
                { "20,3", new[] { "3,8,9", "4,7,9", "5,6,9", "5,7,8" } },
                { "20,4", new[] { "1,2,8,9", "1,3,7,9", "1,4,6,9", "1,4,7,8", "1,5,6,8", "2,3,6,9", "2,3,7,8", "2,4,5,9", "2,4,6,8", "2,5,6,7", "3,4,5,8", "3,4,6,7" } },
                { "20,5", new[] { "1,2,3,5,9", "1,2,3,6,8", "1,2,4,5,8", "1,2,4,6,7", "1,3,4,5,7", "2,3,4,5,6" } },
                { "21,3", new[] { "4,8,9", "5,7,9", "6,7,8" } },
                { "21,4", new[] { "1,3,8,9", "1,4,7,9", "1,5,6,9", "1,5,7,8", "2,3,7,9", "2,4,6,9", "2,4,7,8", "2,5,6,8", "3,4,5,9", "3,4,6,8", "3,5,6,7" } },
                { "21,5", new[] { "1,2,3,6,9", "1,2,3,7,8", "1,2,4,5,9", "1,2,4,6,8", "1,2,5,6,7", "1,3,4,5,8", "1,3,4,6,7", "2,3,4,5,7" } },
                { "21,6", new[] { "1,2,3,4,5,6" } },
                { "22,3", new[] { "5,8,9", "6,7,9" } },
                { "22,4", new[] { "1,4,8,9", "1,5,7,9", "1,6,7,8", "2,3,8,9", "2,4,7,9", "2,5,6,9", "2,5,7,8", "3,4,6,9", "3,4,7,8", "3,5,6,8", "4,5,6,7" } },
                { "22,5", new[] { "1,2,3,7,9", "1,2,4,6,9", "1,2,4,7,8", "1,2,5,6,8", "1,3,4,5,9", "1,3,4,6,8", "1,3,5,6,7", "2,3,4,5,8", "2,3,4,6,7" } },
                { "22,6", new[] { "1,2,3,4,5,7" } },
                { "23,3", new[] { "6,8,9" } },
                { "23,4", new[] { "1,5,8,9", "1,6,7,9", "2,4,8,9", "2,5,7,9", "2,6,7,8", "3,4,7,9", "3,5,6,9", "3,5,7,8", "4,5,6,8" } },
                { "23,5", new[] { "1,2,3,8,9", "1,2,4,7,9", "1,2,5,6,9", "1,2,5,7,8", "1,3,4,6,9", "1,3,4,7,8", "1,3,5,6,8", "1,4,5,6,7", "2,3,4,5,9", "2,3,4,6,8", "2,3,5,6,7" } },
                { "23,6", new[] { "1,2,3,4,5,8", "1,2,3,4,6,7" } },
                { "24,3", new[] { "7,8,9" } },
                { "24,4", new[] { "1,6,8,9", "2,5,8,9", "2,6,7,9", "3,4,8,9", "3,5,7,9", "3,6,7,8", "4,5,6,9", "4,5,7,8" } },
                { "24,5", new[] { "1,2,4,8,9", "1,2,5,7,9", "1,2,6,7,8", "1,3,4,7,9", "1,3,5,6,9", "1,3,5,7,8", "1,4,5,6,8", "2,3,4,6,9", "2,3,4,7,8", "2,3,5,6,8", "2,4,5,6,7" } },
                { "24,6", new[] { "1,2,3,4,5,9", "1,2,3,4,6,8", "1,2,3,5,6,7" } },
                { "25,4", new[] { "1,7,8,9", "2,6,8,9", "3,5,8,9", "3,6,7,9", "4,5,7,9", "4,6,7,8" } },
                { "25,5", new[] { "1,2,5,8,9", "1,2,6,7,9", "1,3,4,8,9", "1,3,5,7,9", "1,3,6,7,8", "1,4,5,6,9", "1,4,5,7,8", "2,3,4,7,9", "2,3,5,6,9", "2,3,5,7,8", "2,4,5,6,8", "3,4,5,6,7" } },
                { "25,6", new[] { "1,2,3,4,6,9", "1,2,3,4,7,8", "1,2,3,5,6,8", "1,2,4,5,6,7" } },
                { "26,4", new[] { "2,7,8,9", "3,6,8,9", "4,5,8,9", "4,6,7,9", "5,6,7,8" } },
                { "26,5", new[] { "1,2,6,8,9", "1,3,5,8,9", "1,3,6,7,9", "1,4,5,7,9", "1,4,6,7,8", "2,3,4,8,9", "2,3,5,7,9", "2,3,6,7,8", "2,4,5,6,9", "2,4,5,7,8", "3,4,5,6,8" } },
                { "26,6", new[] { "1,2,3,4,7,9", "1,2,3,5,6,9", "1,2,3,5,7,8", "1,2,4,5,6,8", "1,3,4,5,6,7" } },
                { "27,4", new[] { "3,7,8,9", "4,6,8,9", "5,6,7,9" } },
                { "27,5", new[] { "1,2,7,8,9", "1,3,6,8,9", "1,4,5,8,9", "1,4,6,7,9", "1,5,6,7,8", "2,3,5,8,9", "2,3,6,7,9", "2,4,5,7,9", "2,4,6,7,8", "3,4,5,6,9", "3,4,5,7,8" } },
                { "27,6", new[] { "1,2,3,4,8,9", "1,2,3,5,7,9", "1,2,3,6,7,8", "1,2,4,5,6,9", "1,2,4,5,7,8", "1,3,4,5,6,8", "2,3,4,5,6,7" } },
                { "28,4", new[] { "4,7,8,9", "5,6,8,9" } },
                { "28,5", new[] { "1,3,7,8,9", "1,4,6,8,9", "1,5,6,7,9", "2,3,6,8,9", "2,4,5,8,9", "2,4,6,7,9", "2,5,6,7,8", "3,4,5,7,9", "3,4,6,7,8" } },
                { "28,6", new[] { "1,2,3,5,8,9", "1,2,3,6,7,9", "1,2,4,5,7,9", "1,2,4,6,7,8", "1,3,4,5,6,9", "1,3,4,5,7,8", "2,3,4,5,6,8" } },
                { "28,7", new[] { "1,2,3,4,5,6,7" } },
                { "29,4", new[] { "5,7,8,9" } },
                { "29,5", new[] { "1,4,7,8,9", "1,5,6,8,9", "2,3,7,8,9", "2,4,6,8,9", "2,5,6,7,9", "3,4,5,8,9", "3,4,6,7,9", "3,5,6,7,8" } },
                { "29,6", new[] { "1,2,3,6,8,9", "1,2,4,5,8,9", "1,2,4,6,7,9", "1,2,5,6,7,8", "1,3,4,5,7,9", "1,3,4,6,7,8", "2,3,4,5,6,9", "2,3,4,5,7,8" } },
                { "29,7", new[] { "1,2,3,4,5,6,8" } },
                { "30,4", new[] { "6,7,8,9" } },
                { "30,5", new[] { "1,5,7,8,9", "2,4,7,8,9", "2,5,6,8,9", "3,4,6,8,9", "3,5,6,7,9", "4,5,6,7,8" } },
                { "30,6", new[] { "1,2,3,7,8,9", "1,2,4,6,8,9", "1,2,5,6,7,9", "1,3,4,5,8,9", "1,3,4,6,7,9", "1,3,5,6,7,8", "2,3,4,5,7,9", "2,3,4,6,7,8" } },
                { "30,7", new[] { "1,2,3,4,5,6,9", "1,2,3,4,5,7,8" } },
                { "31,5", new[] { "1,6,7,8,9", "2,5,7,8,9", "3,4,7,8,9", "3,5,6,8,9", "4,5,6,7,9" } },
                { "31,6", new[] { "1,2,4,7,8,9", "1,2,5,6,8,9", "1,3,4,6,8,9", "1,3,5,6,7,9", "1,4,5,6,7,8", "2,3,4,5,8,9", "2,3,4,6,7,9", "2,3,5,6,7,8" } },
                { "31,7", new[] { "1,2,3,4,5,7,9", "1,2,3,4,6,7,8" } },
                { "32,5", new[] { "2,6,7,8,9", "3,5,7,8,9", "4,5,6,8,9" } },
                { "32,6", new[] { "1,2,5,7,8,9", "1,3,4,7,8,9", "1,3,5,6,8,9", "1,4,5,6,7,9", "2,3,4,6,8,9", "2,3,5,6,7,9", "2,4,5,6,7,8" } },
                { "32,7", new[] { "1,2,3,4,5,8,9", "1,2,3,4,6,7,9", "1,2,3,5,6,7,8" } },
                { "33,5", new[] { "3,6,7,8,9", "4,5,7,8,9" } },
                { "33,6", new[] { "1,2,6,7,8,9", "1,3,5,7,8,9", "1,4,5,6,8,9", "2,3,4,7,8,9", "2,3,5,6,8,9", "2,4,5,6,7,9", "3,4,5,6,7,8" } },
                { "33,7", new[] { "1,2,3,4,6,8,9", "1,2,3,5,6,7,9", "1,2,4,5,6,7,8" } },
                { "34,5", new[] { "4,6,7,8,9" } },
                { "34,6", new[] { "1,3,6,7,8,9", "1,4,5,7,8,9", "2,3,5,7,8,9", "2,4,5,6,8,9", "3,4,5,6,7,9" } },
                { "34,7", new[] { "1,2,3,4,7,8,9", "1,2,3,5,6,8,9", "1,2,4,5,6,7,9", "1,3,4,5,6,7,8" } },
                { "35,5", new[] { "5,6,7,8,9" } },
                { "35,6", new[] { "1,4,6,7,8,9", "2,3,6,7,8,9", "2,4,5,7,8,9", "3,4,5,6,8,9" } },
                { "35,7", new[] { "1,2,3,5,7,8,9", "1,2,4,5,6,8,9", "1,3,4,5,6,7,9", "2,3,4,5,6,7,8" } },
                { "36,6", new[] { "1,5,6,7,8,9", "2,4,6,7,8,9", "3,4,5,7,8,9" } },
                { "36,7", new[] { "1,2,3,6,7,8,9", "1,2,4,5,7,8,9", "1,3,4,5,6,8,9", "2,3,4,5,6,7,9" } },
                { "36,8", new[] { "1,2,3,4,5,6,7,8" } },
                { "37,6", new[] { "2,5,6,7,8,9", "3,4,6,7,8,9" } },
                { "37,7", new[] { "1,2,4,6,7,8,9", "1,3,4,5,7,8,9", "2,3,4,5,6,8,9" } },
                { "37,8", new[] { "1,2,3,4,5,6,7,9" } },
                { "38,6", new[] { "3,5,6,7,8,9" } },
                { "38,7", new[] { "1,2,5,6,7,8,9", "1,3,4,6,7,8,9", "2,3,4,5,7,8,9" } },
                { "38,8", new[] { "1,2,3,4,5,6,8,9" } },
                { "39,6", new[] { "4,5,6,7,8,9" } },
                { "39,7", new[] { "1,3,5,6,7,8,9", "2,3,4,6,7,8,9" } },
                { "39,8", new[] { "1,2,3,4,5,7,8,9" } },
                { "40,7", new[] { "1,4,5,6,7,8,9", "2,3,5,6,7,8,9" } },
                { "40,8", new[] { "1,2,3,4,6,7,8,9" } },
                { "41,7", new[] { "2,4,5,6,7,8,9" } },
                { "41,8", new[] { "1,2,3,5,6,7,8,9" } },
                { "42,7", new[] { "3,4,5,6,7,8,9" } },
                { "42,8", new[] { "1,2,4,5,6,7,8,9" } },
                { "43,8", new[] { "1,3,4,5,6,7,8,9" } },
                { "44,8", new[] { "2,3,4,5,6,7,8,9" } },
                { "45,9", new[] { "1,2,3,4,5,6,7,8,9" } }
            };
            for (var num = 3; num <= 45; num++)
            {
                for (var many = 2; many <= 9; many++)
                {
                    var results = _calcService.GetCombinations(num, many, new int[] {}, new int[] { });
                    if (!results.Any())
                        continue;
                    var expected = dic[num + "," + many];
                    foreach(var res in results)
                    {
                        Assert.Contains(string.Join(",", res), expected);
                    }
                }
            }
        }

    }
}