using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace QuickSettings.Model.Components
{
    [ElementClass("TextBox", "Text/number input.")]
    public class TextBoxComponent : Setting
    {
        private Grid grid;
        private Label label;
        private RestrictedTextBox textBox;

        public override UIElement UIElement => grid;

        public override object Value { get => textBox.Text; set => textBox.Text = value.ToString(); }

        public override string ValueString { get => textBox.Text; set => textBox.Text = value; }

        [ElementProperty("title")]
        public string Title { get => label.Content.ToString(); set => label.Content = value; }

        [ElementProperty("label_width")]
        public string LabelWidth { set => grid.ColumnDefinitions[0].Width = new GridLength(double.Parse(value)); }

        [ElementProperty("type", "(text, integer or decimal)")]
        public string @Type {
            set {
                switch(value.ToLower())
                {
                    case "text":
                        textBox.Restriction = Restriction.TEXT;
                        break;
                    case "integer":
                        textBox.Restriction = Restriction.INTEGER;
                        break;
                    case "decimal":
                        textBox.Restriction = Restriction.DECIMAL;
                        break;
                }
            }
        }

        public TextBoxComponent()
        {
            grid = new Grid();
            grid.Margin = new Thickness(5);
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.3, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.7, GridUnitType.Star) });
            textBox = new RestrictedTextBox();
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            label = new Label();
            Grid.SetColumn(label, 0);
            Grid.SetColumn(textBox, 1);
            grid.Children.Add(label);
            grid.Children.Add(textBox);
        }

        public override void PostInit()
        {
            Value = Content;
        }

        protected enum Restriction
        {
            TEXT, INTEGER, DECIMAL
        }

        protected class RestrictedTextBox : TextBox
        {
            public Restriction @Restriction;
            private static readonly Regex intRegex = new Regex("^[0-9]+$");
            private static readonly Regex decimalRegex = new Regex("^([0-9]*\\.[0-9]*)|[0-9]+$");

            protected override void OnPreviewTextInput(TextCompositionEventArgs e)
            {
                switch(Restriction)
                {
                    case Restriction.INTEGER:
                        if (!intRegex.IsMatch(e.Text))
                            e.Handled = true;
                        break;
                    case Restriction.DECIMAL:
                        if (!decimalRegex.IsMatch(e.Text) || (e.Text.Contains('.') && this.Text.Contains('.')))
                        {
                            e.Handled = true;
                        }
                        break;
                }
                base.OnPreviewTextInput(e);
            }

        }
    }

}
