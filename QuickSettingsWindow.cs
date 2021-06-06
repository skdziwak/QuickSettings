using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using QuickSettings.Model;

namespace QuickSettings
{
    public class QuickSettingsWindow : Window
    {
        public SettingsModel model;
        public SettingsModel Model { get => model; }

        private StackPanel stackPanel;
        private ListView listView;
        
        public QuickSettingsWindow(SettingsModel model)
        {
            this.model = model;
            Title = model.Title;
            Width = model.Width;
            Height = model.Height;
            ResizeMode = model.Resizeable ? ResizeMode.CanResize : ResizeMode.NoResize;
            InitializeComponents();
            if (model.Elements.Count > 0)
            {
                listView.SelectedIndex = 0;
            }
        }

        private void InitializeComponents()
        {
            var grid = new Grid();
            var col1 = new ColumnDefinition();
            var col2 = new ColumnDefinition();
            col1.Width = new GridLength(250);
            col2.Width = new GridLength(1, GridUnitType.Star);
            grid.ColumnDefinitions.Add(col1);
            grid.ColumnDefinitions.Add(col2);
            this.AddChild(grid);

            listView = new ListView();
            listView.ItemsSource = Model.Elements;
            listView.SelectionChanged += TabSelectionChanged;
            Grid.SetColumn(listView, 0);
            grid.Children.Add(listView);

            stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(5, 0, 5, 0);
            Grid.SetColumn(stackPanel, 1);
            grid.Children.Add(stackPanel);
            

        }

        private void ShowTab(SettingsTab tab)
        {
            stackPanel.Children.Clear();
            if (tab != null && tab.Elements != null)
            {
                foreach(Element e in tab.Elements)
                {
                    Component c = e as Component;
                    if (c != null)
                    {
                        stackPanel.Children.Add(c.UIElement);
                    }
                }
            }
        }

        private void TabSelectionChanged(object sender, RoutedEventArgs args)
        {
            ShowTab(listView.SelectedItem as SettingsTab);
        }
    }
}
