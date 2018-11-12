using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Dot.Net.Core.Common.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string CreateXML(this Object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                var xns = new XmlSerializerNamespaces();
                xns.Add(string.Empty, string.Empty);

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var settings = new XmlWriterSettings
                {
                    Encoding = Encoding.GetEncoding(1252),
                    OmitXmlDeclaration = true
                };
                using (var writer = XmlWriter.Create(stringWriter, settings))
                {
                    var xmlSerializer = new XmlSerializer(obj.GetType());
                    xmlSerializer.Serialize(writer, obj, xns);
                }
                return stringWriter.ToString();
            }
        }
        public static DateTime GetLocalDateTime(this DateTime inputDt)
        {
            DateTime localTime;
            if (inputDt.Kind == DateTimeKind.Utc)
            {
                TimeZoneInfo localZone = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
                localTime = TimeZoneInfo.ConvertTimeFromUtc(inputDt, localZone);
                return localTime;
            }
            else
            {
                return inputDt;
            }

        }

        public static DateTime? GetLocalDateTime(this DateTime? inputDt)
        {
            if (!inputDt.HasValue)
            {
                return null;
            }

            if (inputDt == default(DateTime))
            {
                return null;
            }

            if (inputDt.Value.Kind == DateTimeKind.Utc)
            {
                TimeZoneInfo localZone = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
                return TimeZoneInfo.ConvertTimeFromUtc(inputDt.Value, localZone);
            }
            else
            {
                return inputDt;
            }

        }
    }
}
