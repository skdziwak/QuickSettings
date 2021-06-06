using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace QuickSettings.Model
{
    [ElementClass("SettingsModel", "Root tag of all settings models.")]
    public class SettingsModel : Element
    {
        public int Width { get; set; } = 800;
        public int Height { get; set; } = 600;
        public bool Resizeable { get; set; } = false;

        [ElementProperty("title")]
        public string Title { get; set; }

        [ElementProperty("width", "Default: 800")]
        public string WidthStr { set => Width = int.Parse(value); }
        
        [ElementProperty("height", "Default: 600")]
        public string HeightStr { set => Width = int.Parse(value); }

        [ElementProperty("resizeable", "Default: false")]
        public string ResizeableStr { set => Resizeable = value.ToLower() == "true";  }
    
        public Dictionary<string, object> ToDictionary()
        {
            var dict = new Dictionary<string, object>();

            var elements = new List<Element>();
            elements.AddRange(Elements);
            
            for(int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Elements != null)
                {
                    elements.AddRange(elements[i].Elements);
                }
                if (elements[i] is Setting)
                {
                    var setting = (Setting)elements[i];
                    if (setting.Id != null)
                        dict.Add(setting.Id, setting.Value);
                }
            }
            
            return dict;
        }

        public string ToXml()
        {
            var doc = new XDocument();

            var settingsElement = new XElement("Settings");
            doc.Add(settingsElement);

            var elements = new List<Element>();
            elements.AddRange(Elements);
            
            for(int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Elements != null)
                {
                    elements.AddRange(elements[i].Elements);
                }
                if (elements[i] is Setting)
                {
                    var setting = (Setting)elements[i];
                    if (setting.Id != null)
                    {
                        var e = new XElement("Setting");
                        e.SetAttributeValue("id", setting.Id);
                        e.Value = setting.ValueString;
                        settingsElement.Add(e);
                    }
                        
                }
            }

            return doc.ToString();
        }

        public void FromXml(string xml)
        {
            var doc = XDocument.Parse(xml);
            var dict = new Dictionary<string, string>();
            foreach(var e in doc.Root.Elements())
            {
                if (e.Name == "Setting")
                {
                    string id = e.Attribute("id").Value;
                    if (id != null)
                    {
                        dict.Add(id, e.Value);
                    }

                }
            }

            var elements = new List<Element>();
            elements.AddRange(Elements);
            
            for(int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Elements != null)
                {
                    elements.AddRange(elements[i].Elements);
                }
                if (elements[i] is Setting)
                {
                    var setting = (Setting)elements[i];
                    if (setting.Id != null && dict.ContainsKey(setting.Id))
                    {
                        setting.ValueString = dict[setting.Id];
                    }
                        
                }
            }

        }
        

    }
}
