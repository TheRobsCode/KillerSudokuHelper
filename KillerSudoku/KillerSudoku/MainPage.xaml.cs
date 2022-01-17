using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KillerSudoku
{
    public partial class MainPage : ContentPage
    {
        private ICalcService _calcService = new CalcService();
        public MainPage()
        {
            InitializeComponent();
            TargetNumber.TextChanged +=  (sender, e) => {  TargetNumber_TextChanged(sender, e); };
            NumberOfNumbers.TextChanged +=  (sender, e) => {  NumberOfNumbers_TextChanged(sender, e); };
            SelectedNumbers.ItemTapped +=  (sender, e) => {  SelectedNumbers_ItemTapped(sender, e); };
        }
        private void SelectedNumbers_ItemTapped(object sender, EventArgs e)
        {
            Calculate();
        }
        private void TargetNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            Calculate();
        }
        private void NumberOfNumbers_TextChanged(object sender, TextChangedEventArgs e)
        {
            Calculate();
        }
        private void Calculate()
        {
            if (!int.TryParse(TargetNumber.Text, out var targetNumber))
            {
                return;
            }
            if (!int.TryParse(NumberOfNumbers.Text, out var inMany))
            {
                return;
            }
            var mustHave = SelectedNumbers.GetAlwaysIncludedNumbers();
            var result = _calcService.GetCombinations(targetNumber, inMany, SelectedNumbers.GetNeverIncludedNumbers(), mustHave);
            ShowResults(result);

            ShowCommonNumbers(result, mustHave);
        }
        private int _currentTargetNumber;
        private int _currentNumNumber;

        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Started)
            {
                var targetNumber = 3;
                int.TryParse(TargetNumber.Text, out targetNumber);
                _currentTargetNumber = targetNumber;

                var numOfNumbers = 2;
                int.TryParse(NumberOfNumbers.Text, out numOfNumbers);
                _currentNumNumber = numOfNumbers;
            }
            if (e.StatusType == GestureStatus.Running)
            {
                var x = (int)(e.TotalX * 5 / (Application.Current.MainPage.Width));
                var y = -(int)(e.TotalY * (20) / (Application.Current.MainPage.Height));

                if (Math.Abs(e.TotalY) > Math.Abs(e.TotalX))
                {
                    var targetNumber = _currentTargetNumber;
                    targetNumber += y;
                    if (targetNumber < 3)
                        targetNumber = 3;
                    if (targetNumber > 45)
                        targetNumber = 45;
                    TargetNumber.Text = targetNumber.ToString();
                }
                else
                {
                    var numOfNumbers = _currentNumNumber;
                    numOfNumbers += x;
                    if (numOfNumbers < 2)
                        numOfNumbers = 2;
                    if (numOfNumbers > 9)
                        numOfNumbers = 9;
                    NumberOfNumbers.Text = numOfNumbers.ToString();
                }
            }

        }

        private void ShowResults(List<int[]> result)
        {
            var textResult = "";
            foreach (var res in result)
            {
                var first = true;
                foreach (var num in res)
                {
                    if (!first)
                    {
                        textResult += ",";
                    }
                    first = false;
                    textResult += num;
                }
                textResult += " \t\t";
            }
            Result.Text = textResult;
        }

        private void ShowCommonNumbers(List<int[]> result, int[] mustHave)
        {
            var commonNumbers = _calcService.GetCommonNumbers(result, mustHave);
            if (commonNumbers.Any())
            {
                CommonNumbers.Text = "Common:" + String.Join(",", commonNumbers);
                CommonNumbers.IsVisible = true;
            }
            else
            {
                CommonNumbers.IsVisible = false;
            }
        }
    }
}
