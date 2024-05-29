using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Y.Infrastructure.Library.Core.Extensions
{
    public static class XmlExtensions
    {
        /// <summary>
        ///  XmlNode 转换为 String 字符串(XElement)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string ToXmlString(this XmlNode node)
        {
            XmlSerializer serializer = new XmlSerializer(node.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.Serialize(stream, node);
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int) stream.Length);
            return Encoding.UTF8.GetString(dataBytes);
        }
    }
}