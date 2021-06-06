using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QuickSettings.Model.Components
{
    [ElementClass("CheckBox", "True/False input.")]
    public class CheckBoxComponent : Setting
    {
        private CheckBox checkBox;
        public override UIElement @UIElement => checkBox;

        [ElementProperty("title")]
        public string Title { set => checkBox.Content = value; }

        public override object Value { get => (bool)checkBox.IsChecked; set => checkBox.IsChecked = (bool)value; }

        [ElementProperty("checked")]
        public override string ValueString { get => Value.ToString(); set => Value = value.ToLower() == "true"; }

        public CheckBoxComponent()
        {
            checkBox = new CheckBox();
            checkBox.Margin = new Thickness(10, 5, 10, 5);
        }

    }
}
