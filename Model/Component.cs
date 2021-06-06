using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuickSettings.Model
{
    public abstract class Component : Element
    {
        public abstract UIElement @UIElement { get; }
    }
}
