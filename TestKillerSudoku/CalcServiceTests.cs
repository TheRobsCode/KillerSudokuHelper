﻿using KillerSudoku;
using Xunit;

namespace TestKillerSudoku
{
    public class CalcServiceTests
    {
        private readonly ICalcService _calcService = new CalcService();

        [Theory]
        [InlineData(17,8,9)]
        [InlineData(16,7,9)]
        [InlineData(3,1,2)]
        [InlineData(4,1,3)]
        public void TestSingleResultReturn(int number, int res1, int res2)
        {
            var result = _calcService.GetCombinations(number, 2, EmptyArray, EmptyArray).First(); 
            Assert.Contains(res1, result);  
            Assert.Contains(res2, result);  
        }

        [Fact]
        public void TestWithNotInValue()
        {
            var result = _calcService.GetCombinations(8, 3, new int[] {2}, EmptyArray).First(); 
            Assert.Contains(1, result);
            Assert.Contains(3, result);
            Assert.Contains(4, result);
        }
        [Fact]
        public void TestWithManditoryValue()
        {
            var result = _calcService.GetCombinations(8, 3, EmptyArray, new int[] { 2 }).First(); 
            Assert.Contains(1, result);
            Assert.Contains(2, result);
            Assert.Contains(5, result);
        }

        [Fact]
        public void TestLargeNumberOfReturns()
        {
            var result = _calcService.GetCombinations(20, 4, EmptyArray, EmptyArray);
            var simplifiedList = new List<string>();
            foreach (var res in result)
            {
                simplifiedList.Add(string.Join(",", res));
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
            var result = _calcService.GetCombinations(20, 4, new int[] {1,2}, EmptyArray);
            var simplifiedList = new List<string>();
            foreach (var res in result)
            {
                simplifiedList.Add(string.Join(",", res));
            }
            Assert.Equal(2, result.Count);
            Assert.Contains("3,4,5,8", simplifiedList);
            Assert.Contains("3,4,6,7", simplifiedList);
        }
        [Fact]
        public void TestLargeNumberOfReturnsWithMustHave()
        {
            var result = _calcService.GetCombinations(20, 4, EmptyArray, new int[] {1,9});
            var simplifiedList = new List<string>();
            foreach (var res in result)
            {
                simplifiedList.Add(string.Join(",", res));
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
                simplifiedList.Add(string.Join(",", res));
            }
            Assert.Single(result);
            Assert.Contains("1,2,8,9", simplifiedList);
        }
        [Fact]
        public void TestCommonNumbers()
        {
            var result = _calcService.GetCombinations(8, 3, EmptyArray, EmptyArray);
            var common = _calcService.GetCommonNumbers(result, EmptyArray);
            
            Assert.Single(common);
            Assert.Contains(1, common);
        }

        [Fact]
        public void TestNoCommonNumbersForOneResult()
        {
            var result = _calcService.GetCombinations(3, 2, EmptyArray, EmptyArray);
            var common = _calcService.GetCommonNumbers(result, EmptyArray);

            Assert.True(common.Length == 0);
        }

        [Fact]
        public void TestAllCombinations()
        {
            var dic = new Dictionary<string, string[]>
            {
                { "3 in 2", new[] { "1,2" } },
                { "4 in 2", new[] { "1,3" } },
                { "5 in 2", new[] { "1,4", "2,3" } },
                { "6 in 2", new[] { "1,5", "2,4" } },
                { "6 in 3", new[] { "1,2,3" } },
                { "7 in 2", new[] { "1,6", "2,5", "3,4" } },
                { "7 in 3", new[] { "1,2,4" } },
                { "8 in 2", new[] { "1,7", "2,6", "3,5" } },
                { "8 in 3", new[] { "1,2,5", "1,3,4" } },
                { "9 in 2", new[] { "1,8", "2,7", "3,6", "4,5" } },
                { "9 in 3", new[] { "1,2,6", "1,3,5", "2,3,4" } },
                { "10 in 2", new[] { "1,9", "2,8", "3,7", "4,6" } },
                { "10 in 3", new[] { "1,2,7", "1,3,6", "1,4,5", "2,3,5" } },
                { "10 in 4", new[] { "1,2,3,4" } },
                { "11 in 2", new[] { "2,9", "3,8", "4,7", "5,6" } },
                { "11 in 3", new[] { "1,2,8", "1,3,7", "1,4,6", "2,3,6", "2,4,5" } },
                { "11 in 4", new[] { "1,2,3,5" } },
                { "12 in 2", new[] { "3,9", "4,8", "5,7" } },
                { "12 in 3", new[] { "1,2,9", "1,3,8", "1,4,7", "1,5,6", "2,3,7", "2,4,6", "3,4,5" } },
                { "12 in 4", new[] { "1,2,3,6", "1,2,4,5" } },
                { "13 in 2", new[] { "4,9", "5,8", "6,7" } },
                { "13 in 3", new[] { "1,3,9", "1,4,8", "1,5,7", "2,3,8", "2,4,7", "2,5,6", "3,4,6" } },
                { "13 in 4", new[] { "1,2,3,7", "1,2,4,6", "1,3,4,5" } },
                { "14 in 2", new[] { "5,9", "6,8" } },
                { "14 in 3", new[] { "1,4,9", "1,5,8", "1,6,7", "2,3,9", "2,4,8", "2,5,7", "3,4,7", "3,5,6" } },
                { "14 in 4", new[] { "1,2,3,8", "1,2,4,7", "1,2,5,6", "1,3,4,6", "2,3,4,5" } },
                { "15 in 2", new[] { "6,9", "7,8" } },
                { "15 in 3", new[] { "1,5,9", "1,6,8", "2,4,9", "2,5,8", "2,6,7", "3,4,8", "3,5,7", "4,5,6" } },
                { "15 in 4", new[] { "1,2,3,9", "1,2,4,8", "1,2,5,7", "1,3,4,7", "1,3,5,6", "2,3,4,6" } },
                { "15 in 5", new[] { "1,2,3,4,5" } },
                { "16 in 2", new[] { "7,9" } },
                { "16 in 3", new[] { "1,6,9", "1,7,8", "2,5,9", "2,6,8", "3,4,9", "3,5,8", "3,6,7", "4,5,7" } },
                { "16 in 4", new[] { "1,2,4,9", "1,2,5,8", "1,2,6,7", "1,3,4,8", "1,3,5,7", "1,4,5,6", "2,3,4,7", "2,3,5,6" } },
                { "16 in 5", new[] { "1,2,3,4,6" } },
                { "17 in 2", new[] { "8,9" } },
                { "17 in 3", new[] { "1,7,9", "2,6,9", "2,7,8", "3,5,9", "3,6,8", "4,5,8", "4,6,7" } },
                { "17 in 4", new[] { "1,2,5,9", "1,2,6,8", "1,3,4,9", "1,3,5,8", "1,3,6,7", "1,4,5,7", "2,3,4,8", "2,3,5,7", "2,4,5,6" } },
                { "17 in 5", new[] { "1,2,3,4,7", "1,2,3,5,6" } },
                { "18 in 3", new[] { "1,8,9", "2,7,9", "3,6,9", "3,7,8", "4,5,9", "4,6,8", "5,6,7" } },
                { "18 in 4", new[] { "1,2,6,9", "1,2,7,8", "1,3,5,9", "1,3,6,8", "1,4,5,8", "1,4,6,7", "2,3,4,9", "2,3,5,8", "2,3,6,7", "2,4,5,7", "3,4,5,6" } },
                { "18 in 5", new[] { "1,2,3,4,8", "1,2,3,5,7", "1,2,4,5,6" } },
                { "19 in 3", new[] { "2,8,9", "3,7,9", "4,6,9", "4,7,8", "5,6,8" } },
                { "19 in 4", new[] { "1,2,7,9", "1,3,6,9", "1,3,7,8", "1,4,5,9", "1,4,6,8", "1,5,6,7", "2,3,5,9", "2,3,6,8", "2,4,5,8", "2,4,6,7", "3,4,5,7" } },
                { "19 in 5", new[] { "1,2,3,4,9", "1,2,3,5,8", "1,2,3,6,7", "1,2,4,5,7", "1,3,4,5,6" } },
                { "20 in 3", new[] { "3,8,9", "4,7,9", "5,6,9", "5,7,8" } },
                { "20 in 4", new[] { "1,2,8,9", "1,3,7,9", "1,4,6,9", "1,4,7,8", "1,5,6,8", "2,3,6,9", "2,3,7,8", "2,4,5,9", "2,4,6,8", "2,5,6,7", "3,4,5,8", "3,4,6,7" } },
                { "20 in 5", new[] { "1,2,3,5,9", "1,2,3,6,8", "1,2,4,5,8", "1,2,4,6,7", "1,3,4,5,7", "2,3,4,5,6" } },
                { "21 in 3", new[] { "4,8,9", "5,7,9", "6,7,8" } },
                { "21 in 4", new[] { "1,3,8,9", "1,4,7,9", "1,5,6,9", "1,5,7,8", "2,3,7,9", "2,4,6,9", "2,4,7,8", "2,5,6,8", "3,4,5,9", "3,4,6,8", "3,5,6,7" } },
                { "21 in 5", new[] { "1,2,3,6,9", "1,2,3,7,8", "1,2,4,5,9", "1,2,4,6,8", "1,2,5,6,7", "1,3,4,5,8", "1,3,4,6,7", "2,3,4,5,7" } },
                { "21 in 6", new[] { "1,2,3,4,5,6" } },
                { "22 in 3", new[] { "5,8,9", "6,7,9" } },
                { "22 in 4", new[] { "1,4,8,9", "1,5,7,9", "1,6,7,8", "2,3,8,9", "2,4,7,9", "2,5,6,9", "2,5,7,8", "3,4,6,9", "3,4,7,8", "3,5,6,8", "4,5,6,7" } },
                { "22 in 5", new[] { "1,2,3,7,9", "1,2,4,6,9", "1,2,4,7,8", "1,2,5,6,8", "1,3,4,5,9", "1,3,4,6,8", "1,3,5,6,7", "2,3,4,5,8", "2,3,4,6,7" } },
                { "22 in 6", new[] { "1,2,3,4,5,7" } },
                { "23 in 3", new[] { "6,8,9" } },
                { "23 in 4", new[] { "1,5,8,9", "1,6,7,9", "2,4,8,9", "2,5,7,9", "2,6,7,8", "3,4,7,9", "3,5,6,9", "3,5,7,8", "4,5,6,8" } },
                { "23 in 5", new[] { "1,2,3,8,9", "1,2,4,7,9", "1,2,5,6,9", "1,2,5,7,8", "1,3,4,6,9", "1,3,4,7,8", "1,3,5,6,8", "1,4,5,6,7", "2,3,4,5,9", "2,3,4,6,8", "2,3,5,6,7" } },
                { "23 in 6", new[] { "1,2,3,4,5,8", "1,2,3,4,6,7" } },
                { "24 in 3", new[] { "7,8,9" } },
                { "24 in 4", new[] { "1,6,8,9", "2,5,8,9", "2,6,7,9", "3,4,8,9", "3,5,7,9", "3,6,7,8", "4,5,6,9", "4,5,7,8" } },
                { "24 in 5", new[] { "1,2,4,8,9", "1,2,5,7,9", "1,2,6,7,8", "1,3,4,7,9", "1,3,5,6,9", "1,3,5,7,8", "1,4,5,6,8", "2,3,4,6,9", "2,3,4,7,8", "2,3,5,6,8", "2,4,5,6,7" } },
                { "24 in 6", new[] { "1,2,3,4,5,9", "1,2,3,4,6,8", "1,2,3,5,6,7" } },
                { "25 in 4", new[] { "1,7,8,9", "2,6,8,9", "3,5,8,9", "3,6,7,9", "4,5,7,9", "4,6,7,8" } },
                { "25 in 5", new[] { "1,2,5,8,9", "1,2,6,7,9", "1,3,4,8,9", "1,3,5,7,9", "1,3,6,7,8", "1,4,5,6,9", "1,4,5,7,8", "2,3,4,7,9", "2,3,5,6,9", "2,3,5,7,8", "2,4,5,6,8", "3,4,5,6,7" } },
                { "25 in 6", new[] { "1,2,3,4,6,9", "1,2,3,4,7,8", "1,2,3,5,6,8", "1,2,4,5,6,7" } },
                { "26 in 4", new[] { "2,7,8,9", "3,6,8,9", "4,5,8,9", "4,6,7,9", "5,6,7,8" } },
                { "26 in 5", new[] { "1,2,6,8,9", "1,3,5,8,9", "1,3,6,7,9", "1,4,5,7,9", "1,4,6,7,8", "2,3,4,8,9", "2,3,5,7,9", "2,3,6,7,8", "2,4,5,6,9", "2,4,5,7,8", "3,4,5,6,8" } },
                { "26 in 6", new[] { "1,2,3,4,7,9", "1,2,3,5,6,9", "1,2,3,5,7,8", "1,2,4,5,6,8", "1,3,4,5,6,7" } },
                { "27 in 4", new[] { "3,7,8,9", "4,6,8,9", "5,6,7,9" } },
                { "27 in 5", new[] { "1,2,7,8,9", "1,3,6,8,9", "1,4,5,8,9", "1,4,6,7,9", "1,5,6,7,8", "2,3,5,8,9", "2,3,6,7,9", "2,4,5,7,9", "2,4,6,7,8", "3,4,5,6,9", "3,4,5,7,8" } },
                { "27 in 6", new[] { "1,2,3,4,8,9", "1,2,3,5,7,9", "1,2,3,6,7,8", "1,2,4,5,6,9", "1,2,4,5,7,8", "1,3,4,5,6,8", "2,3,4,5,6,7" } },
                { "28 in 4", new[] { "4,7,8,9", "5,6,8,9" } },
                { "28 in 5", new[] { "1,3,7,8,9", "1,4,6,8,9", "1,5,6,7,9", "2,3,6,8,9", "2,4,5,8,9", "2,4,6,7,9", "2,5,6,7,8", "3,4,5,7,9", "3,4,6,7,8" } },
                { "28 in 6", new[] { "1,2,3,5,8,9", "1,2,3,6,7,9", "1,2,4,5,7,9", "1,2,4,6,7,8", "1,3,4,5,6,9", "1,3,4,5,7,8", "2,3,4,5,6,8" } },
                { "28 in 7", new[] { "1,2,3,4,5,6,7" } },
                { "29 in 4", new[] { "5,7,8,9" } },
                { "29 in 5", new[] { "1,4,7,8,9", "1,5,6,8,9", "2,3,7,8,9", "2,4,6,8,9", "2,5,6,7,9", "3,4,5,8,9", "3,4,6,7,9", "3,5,6,7,8" } },
                { "29 in 6", new[] { "1,2,3,6,8,9", "1,2,4,5,8,9", "1,2,4,6,7,9", "1,2,5,6,7,8", "1,3,4,5,7,9", "1,3,4,6,7,8", "2,3,4,5,6,9", "2,3,4,5,7,8" } },
                { "29 in 7", new[] { "1,2,3,4,5,6,8" } },
                { "30 in 4", new[] { "6,7,8,9" } },
                { "30 in 5", new[] { "1,5,7,8,9", "2,4,7,8,9", "2,5,6,8,9", "3,4,6,8,9", "3,5,6,7,9", "4,5,6,7,8" } },
                { "30 in 6", new[] { "1,2,3,7,8,9", "1,2,4,6,8,9", "1,2,5,6,7,9", "1,3,4,5,8,9", "1,3,4,6,7,9", "1,3,5,6,7,8", "2,3,4,5,7,9", "2,3,4,6,7,8" } },
                { "30 in 7", new[] { "1,2,3,4,5,6,9", "1,2,3,4,5,7,8" } },
                { "31 in 5", new[] { "1,6,7,8,9", "2,5,7,8,9", "3,4,7,8,9", "3,5,6,8,9", "4,5,6,7,9" } },
                { "31 in 6", new[] { "1,2,4,7,8,9", "1,2,5,6,8,9", "1,3,4,6,8,9", "1,3,5,6,7,9", "1,4,5,6,7,8", "2,3,4,5,8,9", "2,3,4,6,7,9", "2,3,5,6,7,8" } },
                { "31 in 7", new[] { "1,2,3,4,5,7,9", "1,2,3,4,6,7,8" } },
                { "32 in 5", new[] { "2,6,7,8,9", "3,5,7,8,9", "4,5,6,8,9" } },
                { "32 in 6", new[] { "1,2,5,7,8,9", "1,3,4,7,8,9", "1,3,5,6,8,9", "1,4,5,6,7,9", "2,3,4,6,8,9", "2,3,5,6,7,9", "2,4,5,6,7,8" } },
                { "32 in 7", new[] { "1,2,3,4,5,8,9", "1,2,3,4,6,7,9", "1,2,3,5,6,7,8" } },
                { "33 in 5", new[] { "3,6,7,8,9", "4,5,7,8,9" } },
                { "33 in 6", new[] { "1,2,6,7,8,9", "1,3,5,7,8,9", "1,4,5,6,8,9", "2,3,4,7,8,9", "2,3,5,6,8,9", "2,4,5,6,7,9", "3,4,5,6,7,8" } },
                { "33 in 7", new[] { "1,2,3,4,6,8,9", "1,2,3,5,6,7,9", "1,2,4,5,6,7,8" } },
                { "34 in 5", new[] { "4,6,7,8,9" } },
                { "34 in 6", new[] { "1,3,6,7,8,9", "1,4,5,7,8,9", "2,3,5,7,8,9", "2,4,5,6,8,9", "3,4,5,6,7,9" } },
                { "34 in 7", new[] { "1,2,3,4,7,8,9", "1,2,3,5,6,8,9", "1,2,4,5,6,7,9", "1,3,4,5,6,7,8" } },
                { "35 in 5", new[] { "5,6,7,8,9" } },
                { "35 in 6", new[] { "1,4,6,7,8,9", "2,3,6,7,8,9", "2,4,5,7,8,9", "3,4,5,6,8,9" } },
                { "35 in 7", new[] { "1,2,3,5,7,8,9", "1,2,4,5,6,8,9", "1,3,4,5,6,7,9", "2,3,4,5,6,7,8" } },
                { "36 in 6", new[] { "1,5,6,7,8,9", "2,4,6,7,8,9", "3,4,5,7,8,9" } },
                { "36 in 7", new[] { "1,2,3,6,7,8,9", "1,2,4,5,7,8,9", "1,3,4,5,6,8,9", "2,3,4,5,6,7,9" } },
                { "36 in 8", new[] { "1,2,3,4,5,6,7,8" } },
                { "37 in 6", new[] { "2,5,6,7,8,9", "3,4,6,7,8,9" } },
                { "37 in 7", new[] { "1,2,4,6,7,8,9", "1,3,4,5,7,8,9", "2,3,4,5,6,8,9" } },
                { "37 in 8", new[] { "1,2,3,4,5,6,7,9" } },
                { "38 in 6", new[] { "3,5,6,7,8,9" } },
                { "38 in 7", new[] { "1,2,5,6,7,8,9", "1,3,4,6,7,8,9", "2,3,4,5,7,8,9" } },
                { "38 in 8", new[] { "1,2,3,4,5,6,8,9" } },
                { "39 in 6", new[] { "4,5,6,7,8,9" } },
                { "39 in 7", new[] { "1,3,5,6,7,8,9", "2,3,4,6,7,8,9" } },
                { "39 in 8", new[] { "1,2,3,4,5,7,8,9" } },
                { "40 in 7", new[] { "1,4,5,6,7,8,9", "2,3,5,6,7,8,9" } },
                { "40 in 8", new[] { "1,2,3,4,6,7,8,9" } },
                { "41 in 7", new[] { "2,4,5,6,7,8,9" } },
                { "41 in 8", new[] { "1,2,3,5,6,7,8,9" } },
                { "42 in 7", new[] { "3,4,5,6,7,8,9" } },
                { "42 in 8", new[] { "1,2,4,5,6,7,8,9" } },
                { "43 in 8", new[] { "1,3,4,5,6,7,8,9" } },
                { "44 in 8", new[] { "2,3,4,5,6,7,8,9" } },
                { "45 in 9", new[] { "1,2,3,4,5,6,7,8,9" } }
            };
            for (var num = 3; num <= 45; num++)
            {
                for (var many = 2; many <= 9; many++)
                {
                    var results = _calcService.GetCombinations(num, many, EmptyArray, EmptyArray);
                    if (!results.Any())
                        continue;
                    var expected = dic[num + " in " + many];
                    foreach(var res in results)
                    {
                        Assert.Contains(string.Join(",", res), expected);
                    }
                }
            }
        }

        private static int[] EmptyArray => Array.Empty<int>();
        

    }
}