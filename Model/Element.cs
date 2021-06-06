using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSettings.Model
{
    public class Element
    {
        public string Content { get; set; }
        public List<Element> Elements { get; set; }

        public virtual void PostInit() { }

    }
}
