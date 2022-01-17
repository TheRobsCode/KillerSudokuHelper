using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace KillerSudoku
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NumbersSelect : ContentView
    {
        private const int NormalState = 0;
        private const int AlwaysState = 1;
        private const int NeverState = 2;
        private Button[] _buttons;

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
        public int[] GetAlwaysIncludedNumbers()
        {
            return GetStateButtons(AlwaysState);
        }
        public int[] GetNeverIncludedNumbers()
        {
            return GetStateButtons(NeverState);
        }
        private int[] GetStateButtons(int stateRequest)
        {
            var result = new List<int>();
            for (int i = 0; i < _buttons.Length; i++)
            {
                Button button = _buttons[i];
                int.TryParse(button.CommandParameter.ToString(), out var state);
                if (state == stateRequest)
                    result.Add(i + 1);
            }
            return result.ToArray();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            int.TryParse(button.CommandParameter.ToString(), out var state);
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