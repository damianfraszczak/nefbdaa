using Microsoft.Extensions.Logging;
using NEFBDAACommons.Shared.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Xml.Serialization;

namespace NEFBDAACommons.Shared.Helpers
{
    public static class SerializationHelper
    {
        public static string JsonSerialize(object obj)
        {
            return JsonConvert.SerializeObject(obj,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }

        public static object JsonDeserialize(string val, Type type)
        {
            return JsonConvert.DeserializeObject(val, type, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public static T DeserializeXMLFromStreamToObject<T>(TextReader xmlStream)
        {
            T returnObject = default(T);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                returnObject = (T) serializer.Deserialize(xmlStream);
            }
            catch (Exception ex)
            {
                ApplicationLogger.CreateLogger<TextReader>().Log(LogLevel.Error, "Serialziation XML Error", ex);
            }

            return returnObject;
        }

        public static T DeserializeXMLFileToObject<T>(string XmlFilename)
        {
            if (string.IsNullOrEmpty(XmlFilename)) return default(T);
            return DeserializeXMLFromStreamToObject<T>(new StreamReader(XmlFilename));
        }

        public static T DeserializeXMLStringToObject<T>(string xmlString)
        {
            if (string.IsNullOrEmpty(xmlString)) return default(T);
            return DeserializeXMLFromStreamToObject<T>(new StringReader(xmlString));
        }
    }
}