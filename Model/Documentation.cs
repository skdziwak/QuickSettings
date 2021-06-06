using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace QuickSettings.Model
{
    public class Documentation
    {
        public static string GetMarkdownDocumentation()
        {
            string staticDocumentation;
            using(var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("QuickSettings.Resources.static_documentation.md"))
            {
                using (var reader = new StreamReader(stream))
                {
                    staticDocumentation = reader.ReadToEnd();
                }
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("# QuickSettings C# Library");
            stringBuilder.AppendLine(staticDocumentation);
            stringBuilder.AppendLine("## Built-in XML tags");
            stringBuilder.AppendLine("| Tag | Description | Attributes |");
            stringBuilder.AppendLine("| --- | ----------- | ---------- |");

            foreach(var e in (
                    from type in Assembly.GetExecutingAssembly().GetTypes()
                    let ec = (type.GetCustomAttribute(typeof(ElementClass))) as ElementClass
                    where ec != null
                    select new { Type = type, ElementClass = ec }
                    
                ))
            {
                var attrsStringBuilder = new StringBuilder();
                bool first = true;
                foreach(var attr in (
                    from prop in e.Type.GetProperties()
                    from attr in prop.GetCustomAttributes()
                    let eattr = attr as ElementProperty
                    where eattr != null && prop.CanWrite && prop.PropertyType == typeof(string)
                    select eattr
                                     ))
                {
                    if (first)
                    {
                        first = false;
                    }  else
                    {
                        attrsStringBuilder.Append("<br>");
                    }
                    if (attr.Description != null)
                    {
                        attrsStringBuilder.Append(String.Format("{0} - {1}", attr.Name, attr.Description));
                    }
                    else
                    {
                        attrsStringBuilder.Append(String.Format("{0}", attr.Name));
                    }
                }

                string attrs = attrsStringBuilder.ToString();
                if (e.ElementClass.Description != null)
                {
                    stringBuilder.AppendLine(String.Format("| {0} | {1} | {2} |", e.ElementClass.Tag, e.ElementClass.Description, attrs));
                } else
                {
                    stringBuilder.AppendLine(String.Format("| {0} | [No description] | {1} |", e.ElementClass.Tag, attrs));
                }             
            }

            return stringBuilder.ToString();
        }
        public static void SaveMarkdownDocumentation(string path)
        {
            File.WriteAllText(path, GetMarkdownDocumentation());
        }
    }
}
