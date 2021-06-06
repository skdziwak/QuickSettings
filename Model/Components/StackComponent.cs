using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QuickSettings.Model.Components
{
    [ElementClass("Stack", "Container tag.")]
    public class StackComponent : Component
    {
        private StackPanel stackPanel;
        public override UIElement UIElement => stackPanel;

        [ElementProperty("horizontal", "Default: false")]
        public string Horizontal { set => stackPanel.Orientation = (value.ToLower() == "true") ? Orientation.Horizontal : Orientation.Vertical; }

        public StackComponent()
        {
            stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(5);
        }

        public override void PostInit()
        {
            foreach(var component in (from e in Elements where e is Component select (Component)e)) {
                stackPanel.Children.Add(component.UIElement);
            }
        } 


    }
}
