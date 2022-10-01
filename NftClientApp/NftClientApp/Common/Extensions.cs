using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.Net;

namespace NftClientApp.Common
{
    public static class Extensions
    {
        public static T ConvertNode<T>(XmlNode node) where T : class
        {
            MemoryStream stm = new();

            StreamWriter stw = new(stm);
            stw.Write(node.OuterXml);
            stw.Flush();

            stm.Position = 0;

            XmlSerializer ser = new(typeof(T));
            T? result = ser.Deserialize(stm) as T;

            return result;
        }

        public static HttpWebRequest MakeRequest(string endpoint, string method, byte[]? bodyData)
        {
            HttpWebRequest request = WebRequest.CreateHttp(endpoint);
            request.Method = method;
            request.ContentType = "application/xml";
            request.Accept = "application/xml";

            if (bodyData != null)
            {
                var bodyHttp = request.GetRequestStream();
                bodyHttp.Write(bodyData, 0, bodyData.Length);
                bodyHttp.Close();
            }

            return request;
        }
    }
}
