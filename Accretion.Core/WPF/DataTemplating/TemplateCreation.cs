using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Markup;

namespace Accretion.Core
{
    public static class TemplateCreation
    {
        public static DataTemplate CreateImplicitTemplate(Type dataType, Type contentType)
        {
            const string XamlTemplate = "<DataTemplate DataType=\"{{x:Type vm:{0}}}\"><v:{1} /></DataTemplate>";
            var xaml = string.Format(XamlTemplate, dataType.Name, contentType.Name, dataType.Namespace, contentType.Namespace);

            var context = new ParserContext { XamlTypeMapper = new XamlTypeMapper(Array.Empty<string>()) };

            context.XamlTypeMapper.AddMappingProcessingInstruction("vm", dataType.Namespace, dataType.Assembly.FullName);
            context.XamlTypeMapper.AddMappingProcessingInstruction("v", contentType.Namespace, contentType.Assembly.FullName);

            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("vm", "vm");
            context.XmlnsDictionary.Add("v", "v");

            var template = (DataTemplate)XamlReader.Parse(xaml, context);

            return template;
        }
    }
}
