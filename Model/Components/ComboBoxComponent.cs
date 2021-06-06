using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QuickSettings.Model.Components
{
    [ElementClass("ComboBox", "Item selection tag")]
    class ComboBoxComponent : Setting
    {
        private ComboBox comboBox;

        public override object Value { get => comboBox.SelectedIndex; set => comboBox.SelectedIndex = (int)value; }
        public override string ValueString { get => Value.ToString(); set => Value = int.Parse(value); }

        public override UIElement UIElement => comboBox;

        public ComboBoxComponent()
        {
            comboBox = new ComboBox();
            comboBox.Margin = new Thickness(5);
        }

        public override void PostInit()
        {
            comboBox.ItemsSource = Elements;
            if (Elements.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }
    }
}
