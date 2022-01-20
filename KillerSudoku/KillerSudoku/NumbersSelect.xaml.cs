using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KillerSudoku
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NumbersSelect : ContentView
    {
        private const int NormalState = 0;
        private const int AlwaysState = 1;
        private const int NeverState = 2;
        private readonly Button[] _buttons;

        public event EventHandler<EventArgs> ItemTapped;
        public NumbersSelect()
        {
            InitializeComponent();
            Button1.Clicked += Button_Clicked;
            Button2.Clicked += Button_Clicked;
            Button3.Clicked += Button_Clicked;
            Button4.Clicked += Button_Clicked;
            Button5.Clicked += Button_Clicked;
            Button6.Clicked += Button_Clicked;
            Button7.Clicked += Button_Clicked;
            Button8.Clicked += Button_Clicked;
            Button9.Clicked += Button_Clicked;
            _buttons = new Button[] {Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8, Button9};
            
        }
        public void GetAlwaysAndNeverIncludedNumbers(out int[] mustHave, out int[] neverHave)
        {
            var must = new List<int>();
            var never = new List<int>();
            for (int i = 0; i < _buttons.Length; i++)
            {
                var button = _buttons[i];
                var srate = GetState(button);
                if (srate == AlwaysState)
                    must.Add(i + 1);
                else if (srate == NeverState)
                    never.Add(i + 1);

            }
            mustHave = must.ToArray();
            neverHave = never.ToArray();
        }
        private int GetState(Button button)
        {
            int.TryParse(button.CommandParameter.ToString(), out var state);
            return state;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var state = GetState(button);
            state++;
            if (state >= 3)
                state = 0;
            switch(state)
            {
                case NormalState:
                    button.BackgroundColor = Color.Gray;
                    button.TextColor = Color.White;
                    break;
                case AlwaysState:
                    button.BackgroundColor = Color.Black;
                    button.TextColor = Color.White;
                    break;
                case NeverState:
                    button.BackgroundColor = Color.Red;
                    button.TextColor = Color.Black;
                    break;
            }
            button.CommandParameter = state;
            ItemTapped(this, e);
        }
    }
}