using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QuickSettings.Model.Components
{
    
    [ElementClass("RadioButton", "Only one of RadioButtons in a Stack can be selected.")]
    public class RadioButtonComponent : Setting
    {
        private RadioButton radioButton;

        public override UIElement UIElement => radioButton;

        [ElementProperty("title")]
        public string Title { set => radioButton.Content = value; }

        public override object Value { get => (bool)radioButton.IsChecked; set => radioButton.IsChecked = (bool)value; }

        [ElementProperty("checked", "Default: false")]
        public override string ValueString { get => Value.ToString(); set => Value = value.ToLower() == "true"; }

        public RadioButtonComponent()
        {
            radioButton = new RadioButton();
            radioButton.Margin = new Thickness(10, 5, 10, 5);
        }
    }
}
