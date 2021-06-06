using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSettings.Model
{
    public class ElementProperty : Attribute
    {
        public string Name;
        public string Description;

        public ElementProperty(string name)
        {
            Name = name;
        }

        public ElementProperty(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
