using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuickSettings.Model
{
    public abstract class Setting : Component
    {
        public abstract object Value { get; set; }
        public abstract string ValueString { get;  set; }

        [ElementProperty("id")]
        public string Id { get; set; }
    }
}
