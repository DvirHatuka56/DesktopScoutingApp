using System.Windows;
using System.Windows.Controls;

namespace ScoutingApp
{
    public partial class CustomSlider : UserControl
    {
        public CustomSlider()
        {
            InitializeComponent();
        }

        public int GetIntValue()
        {
            return (int) SliderControl.Value;
        }
        
        private void AutoSucsses_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderValue.Content = (int)SliderControl.Value;
        }
    }
}
