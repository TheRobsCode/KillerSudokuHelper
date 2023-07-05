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
        private readonly ICalcService _calcService = new CalcService();
        private int _currentTargetNumber;
        private int _currentNumNumber;
        private const int MinTargetNumber = 3;
        private const int MaxTargetNumber = 45;
        private const int MinNumberOfNumbers = 2;
        private const int MaxNumberOfNumbers = 9;
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
            SelectedNumbers.GetAlwaysAndNeverIncludedNumbers(out var mustHave, out var neverHave);
            var result = _calcService.GetCombinations(targetNumber, inMany, neverHave, mustHave);
            ShowResults(result);

            ShowCommonNumbers(result, mustHave);
        }

        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Started)
            {
                _currentTargetNumber = TextToInt(TargetNumber.Text, MinTargetNumber);
                _currentNumNumber = TextToInt(NumberOfNumbers.Text, MinNumberOfNumbers);
            }
            else if (e.StatusType == GestureStatus.Running)
            {
                var x = (int)(e.TotalX * 5 / (Application.Current.MainPage.Width));
                var y = -(int)(e.TotalY * (20) / (Application.Current.MainPage.Height));

                if (IsSwippingVertically(e.TotalX, e.TotalY))
                {
                    SetTargetNumber(_currentTargetNumber + y);
                }
                else
                {
                    SetNumberOfNumbers(_currentNumNumber + x);
                }
            }

        }

        private void SetNumberOfNumbers(int numOfNumbers)
        {
            if (numOfNumbers < MinNumberOfNumbers)
                numOfNumbers = MinNumberOfNumbers;
            if (numOfNumbers > MaxNumberOfNumbers)
                numOfNumbers = MaxNumberOfNumbers;
            NumberOfNumbers.Text = numOfNumbers.ToString();
        }

        private void SetTargetNumber(int targetNumber)
        {
            if (targetNumber < MinTargetNumber)
                targetNumber = MinTargetNumber;
            if (targetNumber > MaxTargetNumber)
                targetNumber = MaxTargetNumber;
            TargetNumber.Text = targetNumber.ToString();
        }

        private bool IsSwippingVertically(double totalX, double totalY)
        {
            return Math.Abs(totalY) > Math.Abs(totalX);
        }
        private int TextToInt(string text, int defaultVal)
        {
            if (int.TryParse(text, out var res))
            {
                return res;
            }
            return defaultVal;
        }
        private void ShowResults(List<int[]> result)
        {
            Result.Children.Clear();
            var textResult = "";
            foreach (var res in result)
            {
                /*var first = true;
                foreach (var num in res)
                {
                    if (!first)
                    {
                        textResult += ",";
                    }
                    first = false;
                    textResult += num;
                }*/
                textResult = String.Join(",", res);
                
                Result.Children.Add(new Label()
                {
                    Text = textResult,
                    Margin = new Thickness(10),
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Start,
                    FlowDirection = FlowDirection.LeftToRight,
                    FontSize =22
                    
                });
            }
            //Result.Text = textResult;
        }

        private void ShowCommonNumbers(List<int[]> result, int[] mustHave)
        {
            var commonNumbers = _calcService.GetCommonNumbers(result, mustHave);
            if (commonNumbers.Any())
            {
                CommonNumbers.Text = "Common:" + string.Join(",", commonNumbers);
                CommonNumbers.IsVisible = true;
            }
            else
            {
                CommonNumbers.IsVisible = false;
            }
        }
    }
}
