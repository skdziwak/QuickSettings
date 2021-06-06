using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace QuickSettings.Model
{
    public class Parser
    {
        public Dictionary<string, Type> Types;

        public Parser()
        {
            Types = new Dictionary<string, Type>();
            foreach (var e in
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                let attr = type.GetCustomAttribute(typeof(ElementClass))
                where attr != null
                select new { Type = type, Tag = (attr as ElementClass).Tag })
            {
                Types.Add(e.Tag, e.Type);
            }
        }

        public Parser(Dictionary<string, Type> types)
        {
            Types = types;
        }

        public SettingsModel ParseXmlResource(string path)
        {
            return ParseXmlResource(Assembly.GetCallingAssembly(), path);
        }

        public SettingsModel ParseXmlResource(Assembly assembly, string path)
        {
            using (var stream = assembly.GetManifestResourceStream(path))
            {
                using (var reader = new StreamReader(stream))
                {
                    return ParseXmlString(reader.ReadToEnd());
                }
            }
        }

        public SettingsModel ParseXmlString(string xml)
        {
            var doc = XDocument.Parse(xml);
            return ParseElement(doc.Root) as SettingsModel;
        }

        private Element ParseElement(XElement xelement)
        {
            string typeName = xelement.Name.ToString();
            if (!Types.ContainsKey(typeName)) throw new ParserException(String.Format("Type {0} is not a proper Element type.", typeName));
            var type = Types[typeName];
            if (!typeof(Element).IsAssignableFrom(type)) throw new ParserException(String.Format("{0} does not derive from QuickSettings.Model.Element", typeName));
            var constructor = type.GetConstructor(new Type[0]);
            if (constructor == null) throw new ParserException("{0} doesn't have a default constructor.");
            var element = constructor.Invoke(new object[0]) as Element;

            var xmlAttributes = xelement.Attributes();
            var properties = type.GetProperties();

            foreach (var e in
                    from xattr in xmlAttributes
                    from prop in properties
                    from attr in prop.GetCustomAttributes()
                    let eattr = attr as ElementProperty
                    where eattr != null && eattr.Name == xattr.Name && prop.CanWrite && prop.PropertyType == typeof(string)
                    select new {Property = prop, Value = xattr.Value}
                ) {
                e.Property.SetValue(element, e.Value);
            }

            var xmlElements = xelement.Elements().ToList();
            if (xmlElements.Count != 0)
            {
                element.Elements = new List<Element>();
                foreach (var e in xelement.Elements())
                {
                    element.Elements.Add(ParseElement(e));
                }
            } else
            {
                element.Content = xelement.Value.Trim();
            }

            element.PostInit();

            return element;
        }

    }
}
