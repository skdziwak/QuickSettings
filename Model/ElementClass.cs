using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSettings.Model
{
    public class ElementClass : Attribute
    {
        public string Tag { get; set; }
        public string Description { get; set; }
        public ElementClass(string tag)
        {
            Tag = tag;
        }

        public ElementClass(string tag, string description)
        {
            Tag = tag;
            Description = description;
        }
    }
}
