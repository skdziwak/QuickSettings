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
    [ElementClass("Grid", "Grid for other components")]
    class GridComponent : Component
    {
        private string[] columns;
        private string[] rows;
        private int index;
        private Grid grid;

        [ElementProperty("rows", "Format: \"150:0.7*:0.3*\" or \"auto\"")]
        public string Rows { set => rows = value.Split(':'); }
        [ElementProperty("columns", "Format: \"150:0.7*:0.3*\"")]
        public string Columns { set => columns = value.Split(':'); }

        public override UIElement UIElement => grid;

        public GridComponent()
        {
            grid = new Grid();
            columns = new string[] { "1*" };
            rows = new string[] { "1*" };
            index = 0;
        }

        public override void PostInit()
        {
            foreach(string col in columns)
            {
                double size;
                GridUnitType type;
                if (col.EndsWith("*"))
                {
                    size = double.Parse(col.Substring(0, col.Length - 1), CultureInfo.InvariantCulture);
                    type = GridUnitType.Star;
                } else
                {
                    size = double.Parse(col, CultureInfo.InvariantCulture);
                    type = GridUnitType.Pixel;
                }
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(size, type)});
            }
            if (rows.Length == 1 && rows[0] == "auto")
            {
                if (Elements != null)
                {
                    int r = (int)Math.Ceiling(((double)Elements.Count) / ((double)columns.Length));
                    for (int i = 0; i < r; i++)
                    {
                        grid.RowDefinitions.Add(new RowDefinition());
                    }
                }
            }
            else
            {

                foreach(string row in rows)
                {
                    double size;
                    GridUnitType type;
                    if (row.EndsWith("*"))
                    {
                        size = double.Parse(row.Substring(0, row.Length - 1), CultureInfo.InvariantCulture);
                        type = GridUnitType.Star;
                    } else
                    {
                        size = double.Parse(row, CultureInfo.InvariantCulture);
                        type = GridUnitType.Pixel;
                    }
                    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(size, type)});
                }
            }
            if (Elements != null)
            {
                foreach (Component component in (from c in Elements where c is Component select (Component)c))
                {
                    Grid.SetColumn(component.UIElement, index % columns.Length);
                    Grid.SetRow(component.UIElement, index / columns.Length);
                    grid.Children.Add(component.UIElement);
                    index++;
                }
            }
        }
    }
}
