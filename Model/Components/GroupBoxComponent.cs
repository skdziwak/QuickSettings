using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QuickSettings.Model.Components
{
    [ElementClass("Group", "GroupBox for settings.")]
    class GroupBoxComponent : Component
    {
        private GroupBox groupBox;
        private StackPanel stackPanel;

        public override UIElement UIElement => groupBox;

        [ElementProperty("title")]
        public string Title { set => groupBox.Header = value; }

        [ElementProperty("horizontal", "Default: false")]
        public string Horizontal { set => stackPanel.Orientation = (value.ToLower() == "true") ? Orientation.Horizontal : Orientation.Vertical; }

        public GroupBoxComponent()
        {
            groupBox = new GroupBox();
            groupBox.Margin = new Thickness(5);
            stackPanel = new StackPanel();
            groupBox.Content = stackPanel;
        }

        public override void PostInit()
        {
            foreach(var component in (from e in Elements where e is Component select (Component)e)) {
                stackPanel.Children.Add(component.UIElement);
            }
        } 
    }
}
