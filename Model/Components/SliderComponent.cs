using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;

namespace QuickSettings.Model.Components
{
    [ElementClass("Slider", "Decimal number input")]
    public class SliderComponent : Setting
    {
        private Slider slider;

        public override object Value { get => slider.Value; set => slider.Value = (double)value; }
        public override string ValueString { 
            get => ((double)Value).ToString(CultureInfo.InvariantCulture); 
            set => Value = double.Parse(value, CultureInfo.InvariantCulture); 
        }

        public override UIElement UIElement => slider;

        [ElementProperty("max")]
        public string Max { set => slider.Maximum = double.Parse(value, CultureInfo.InvariantCulture); }

        [ElementProperty("min")]
        public string Min { set => slider.Minimum = double.Parse(value, CultureInfo.InvariantCulture); }

        public SliderComponent()
        {
            slider = new Slider();
            slider.Minimum = 0;
            slider.Maximum = 100;
            slider.Margin = new Thickness(5);
        }

        public override void PostInit()
        {
            
        }

    }
}
