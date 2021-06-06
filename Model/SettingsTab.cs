using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSettings.Model
{
    [ElementClass("Tab", "Container for all input tags.")]
    public class SettingsTab : Element
    {
        [ElementProperty("title")]
        public string Title { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
