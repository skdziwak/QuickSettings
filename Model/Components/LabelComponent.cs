using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QuickSettings.Model.Components
{
    [ElementClass("Label", "Text label")]
    public class LabelComponent : Component
    {
        private Label label;
        public override UIElement UIElement => label;

        [ElementProperty("font")]
        public string FontFamily { 
            set
            {
                label.FontFamily = new System.Windows.Media.FontFamily(value);
            } 
        }

        [ElementProperty("size")]
        public string FontSize
        {
            set
            {
                label.FontSize = int.Parse(value);
            }
        }

        [ElementProperty("style", "(italic, normal or oblique)")]
        public string FontStyle
        {
            set
            {
                string v = value.ToLower();
                try
                {
                    label.FontStyle = (
                        from property in typeof(FontStyles).GetProperties()
                        where property.Name.ToLower() == v
                        select (FontStyle) property.GetValue(null)
                                  ).First();
                } catch(InvalidOperationException ex)
                {
                    Console.Error.WriteLine(ex);
                }
            }
        }

        [ElementProperty("weight", "(Thin, ExtraLight, UltraLight, Light, Normal, Regular, Medium, DemiBold, SemiBold, Bold, ExtraBold, UltraBold, Black, Heavy, ExtraBlack, UltraBlack)")]
        public string FontWeight
        {
            set
            {
                string v = value.ToLower();
                try
                {
                    label.FontWeight = (
                        from property in typeof(FontWeights).GetProperties()
                        where property.Name.ToLower() == v
                        select (FontWeight) property.GetValue(null)
                                  ).First();
                } catch(InvalidOperationException ex)
                {
                    Console.Error.WriteLine(ex);
                }
            }
        }


        public LabelComponent()
        {
            label = new Label();
        }

        public override void PostInit()
        {
            label.Content = Content;
        }
    }
}
