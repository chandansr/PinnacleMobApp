using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Xml;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Text;
namespace ezDispatcherWebsite.WCF.DataAccessLayer
{
    public static class SerializationHelper
    {
        /// <summary>
        /// CREATED BY PRASHANT KUMAR VERMA
        /// THIS METHOD WILL SERIALIZE XML ALSO IGNORING THE PROPERTIES 
        /// THIS IS VERY HELP FULL FOR AVOIDING THE NAVIGATION PROPERTIES
        /// SERIALIZE.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertiesToIgnoreForSerialization"></param>   
        /// <param name="rootnode"></param>
        /// <returns></returns>      
        public static XmlDocument ConvertObjectToXml(object source, List<string> propertiesToIgnoreForSerialization, string rootnode = "data")
        {
            var jsonData = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeXmlNode(jsonData, rootnode);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="rootElementName"></param>
        /// <returns></returns>
        public static string ConvertJsonToXml(string jsonString, string rootElementName = "data")
        {
            if (!string.IsNullOrWhiteSpace(jsonString))
            {
                return JsonConvert.DeserializeXmlNode(jsonString, rootElementName, true).InnerXml.ToString();
            }
            return "<data></data>";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ConvertObjecttoJson(dynamic obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception)
            {

                return "unable to convert the Object to json";
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T DeserializeFromXml<T>(string xml)
        {
            T result;
            var ser = new XmlSerializer(typeof(T));
            using (TextReader tr = new StringReader(xml))
            {
                result = (T)ser.Deserialize(tr);
            }
            return result;
        }

        public static string ConvertXMltoJson(string xmlString = "<data></data>")
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);
            return JsonConvert.SerializeXmlNode(doc);
        }
        public static string[] convertXMLStringArray(string elementName = "", string xmlString = "<data></data>")
        {
            XDocument doc = XDocument.Parse(xmlString);
            return doc.Root.Elements(elementName)
                               .Select(element => element.Value)
                               .ToArray();
        }

        //Method to parse Text into HTML
        public static string ConvertTextToHTML(string text, bool allow)
        {
            //Create a StringBuilder object from the string input
            //parameter
            StringBuilder sb = new StringBuilder(text);
            //Replace all double white spaces with a single white space
            //and &nbsp;
            sb.Replace("  ", " &nbsp;");
            //Check if HTML tags are not allowed
            if (!allow)
            {
                //Convert the brackets into HTML equivalents
                sb.Replace("<", "&lt;");
                sb.Replace(">", "&gt;");
                //Convert the double quote
                sb.Replace("\"", "&quot;");
            }
            //Create a StringReader from the processed string of
            //the StringBuilder object
            StringReader sr = new StringReader(sb.ToString());
            StringWriter sw = new StringWriter();
            //Loop while next character exists
            while (sr.Peek() > -1)
            {
                //Read a line from the string and store it to a temp
                //variable
                string temp = sr.ReadLine();
                //write the string with the HTML break tag
                //Note here write method writes to a Internal StringBuilder
                //object created automatically
                sw.Write(temp + "<br>");
            }
            //Return the final processed text
            return sw.GetStringBuilder().ToString();
        }
    }
}











