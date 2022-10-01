using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NftApi.Common
{
    public static class Extensions
    {
        public static T FromXElement<T>(this XElement xElement)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            return (T)xmlSerializer.Deserialize(xElement.CreateReader());
        }

        public static XmlDocument ToXmlDocument(this XElement xelement)
        {
            var xmldoc = new XmlDocument();
            xmldoc.Load(xelement.CreateReader());
            return xmldoc;
        }
    }
}
