using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSettings.Model.Components
{
    [ElementClass("Item", "ComboBox Item")]
    class ComboBoxItem : Element
    {
        public override string ToString()
        {
            return Content;
        }
    }
}
