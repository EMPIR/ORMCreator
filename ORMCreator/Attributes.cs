using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMCreator
{
    class Attributes
    {
        public static string Includes = "using System;\r\n" +
                                        "using System.Collections;\r\n" +
                                        "using System.Configuration;\r\n" +
                                        "using System.Data;\r\n" +
                                        "using System.Linq;\r\n" +
                                        "using System.Web;\r\n" +
                                        "using System.Collections.Generic;\r\n" +
                                        "using System.Net.Mail;\r\n" +
                                        "using System.Xml.Serialization;\r\n" +
                                        "using System.IO;\r\n" +
                                        "using System.Xml;\r\n" +
                                        "using System.Text;\r\n\r\n\r\n\r\n";
        public static string StartClass = 
"namespace SLXapi\r\n"+
"{\r\n"+
"    [XmlRootAttribute(ElementName = \"%ClassName%\")]\r\n"+
"    public class %ClassName%\r\n" +
"    {\r\n";

        public static string MemeberIndent = "        ";
        public static string CloseClass =  "}";
        public static string CloseNamespace = "   }\r\n";
        public static string CloseFunction = 
"        }\r\n";
        public static string ReturnTrue =
"             return true;\r\n";

        public static string ClassXMLSerialization =
"\r\n\r\n\r\n"+
"        /// <summary>\r\n" +
"        /// Method to convert a custom Object to XML string\r\n" +
"        /// </summary>\r\n" +
"        /// <param name=\"pObject\">Object that is to be serialized to XML</param>\r\n" +
"        /// <returns>XML string</returns>\r\n" +
"        public static String SerializeObject(Object pObject)\r\n" +
"        {\r\n" +
"            try\r\n" +
"            {\r\n" +
"                String XmlizedString = null;\r\n" +
"                MemoryStream memoryStream = new MemoryStream();\r\n" +
"                XmlSerializer xs = new XmlSerializer(typeof(%ClassName%));\r\n" +
"                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);\r\n" +
"                xs.Serialize(xmlTextWriter, pObject);\r\n" +
"                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;\r\n" +
"                XmlizedString = SLXapi.Helpers.Serialization.UTF8ByteArrayToString(memoryStream.ToArray());\r\n" +
"                int removeHeadLength = 39;\r\n" +
"                XmlizedString = XmlizedString.Remove(0, removeHeadLength);\r\n" +
"                return XmlizedString;\r\n" +
"            }\r\n" +
"\r\n" +
"            catch (Exception e)\r\n" +
"            {\r\n" +
"                System.Console.WriteLine(e);\r\n" +
"                return null;\r\n" +
"            }\r\n" +
"        }\r\n" +
"\r\n\r\n\r\n" +
"        /// <summary>\r\n" +
"        /// Method to reconstruct an Object from XML string\r\n" +
"        /// </summary>\r\n" +
"        /// <param name=\"pXmlizedString\"></param>\r\n" +
"        /// <returns></returns>\r\n" +
"        public static Object DeserializeObject(String pXmlizedString)\r\n" +
"        {\r\n" +
"            XmlSerializer xs = new XmlSerializer(typeof(%ClassName%));\r\n" +
"            MemoryStream memoryStream = new MemoryStream(Helpers.Serialization.StringToUTF8ByteArray(pXmlizedString));\r\n" +
"            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);\r\n" +
"            return xs.Deserialize(memoryStream);\r\n" +
"        }\r\n";

        public static string ClassConstructors =
"\r\n\r\n\r\n" +
"        public %ClassName%()\r\n" +
"        {\r\n" +
"        }\r\n" +
"\r\n" +
"        public %ClassName%(%ClassName% o)\r\n" +
"        {\r\n" +
"            Copy(o);\r\n" +
"        }\r\n";

        public static string CopyFunctionStart =
"        public void Copy(%ClassName% o)\r\n" +
"        {\r\n";

        public static string EqualsFunctionStart =
"        public void Equals(%ClassName% o)\r\n" +
"        {\r\n";

        public static string LoadFunctionStart =
"       static public void LoadOpportunity(SLXapi.App_Data.SLXapiDB.%ClassName%sRow row, %ClassName% d)\r\n" +
"        {\r\n";


        public static string PublicStaticFunctions =

"        public static bool Exists(%ClassName% o)\r\n" +
"        {\r\n" +
"            return Exists(o.%ClassName%ID);\r\n" +
"        }\r\n" +
"        public static bool Exists(string id)\r\n" +
"        {\r\n" +
"            SLXapi.App_Data.SLXapiDBTableAdapters.%TableAdapter% ta = new SLXapi.App_Data.SLXapiDBTableAdapters.%TableAdapter%();\r\n" +
"            if (ta.GetSumByID(id) == 0)\r\n" +
"                return false;\r\n" +
"            return true;\r\n" +
"        }\r\n" +
"\r\n" +
"        public static bool isValid(%ClassName% o)\r\n" +
"        {\r\n" +
"            if (o.%ClassName%ID == String.Empty)\r\n" +
"                return false;\r\n" +
"            if (o.Type == String.Empty)\r\n" +
"                return false;\r\n" +
"            return true;\r\n" +
"        }\r\n" +
"\r\n" +
"        public static bool Remove(%ClassName% j)\r\n" +
"        {\r\n" +
"            if (Exists(j))\r\n" +
"            {\r\n" +
"                return Delete(j);\r\n" +
"            }\r\n" +
"            return false;\r\n" +
"        }\r\n" +
"\r\n" +
"        public static bool Save(%ClassName% j)\r\n" +
"        {\r\n" +
"            if (!%ClassName%.isValid(j))\r\n" +
"                return false;\r\n" +
"            if (Exists(j))\r\n" +
"            {\r\n" +
"                return %ClassName%.Update(j);\r\n" +
"            }\r\n" +
"            return %ClassName%.Insert(j);\r\n" +
"        }\r\n" +
"\r\n" +
"        public bool Refresh()\r\n" +
"        {\r\n" +
"            if (!%ClassName%.Exists(this))\r\n" +
"                return false;\r\n" +
"            %ClassName% ret = %ClassName%.GetByID(%ClassName%ID);\r\n" +
"            Copy(ret);\r\n" +
"            return true;\r\n" +
"        }\r\n" +
"\r\n" +
"        public static List<%ClassName%> GetAll()\r\n" +
"        {\r\n" +
"            SLXapi.App_Data.SLXapiDBTableAdapters.%TableAdapter% ta = new SLXapi.App_Data.SLXapiDBTableAdapters.%TableAdapter%();\r\n" +
"            SLXapi.App_Data.SLXapiDB.%ClassName%DataTable table = ta.GetData();\r\n" +
"            return LoadFromModel(table);\r\n" +
"        }\r\n" +
"\r\n" +
"        public static %ClassName% GetByID(string id)\r\n" +
"        {\r\n" +
"            SLXapi.App_Data.SLXapiDBTableAdapters.%TableAdapter% ta = new SLXapi.App_Data.SLXapiDBTableAdapters.%TableAdapter%();\r\n" +
"            SLXapi.App_Data.SLXapiDB.%ClassName%DataTable table = ta.GetDataByID(id);\r\n" +
"            SLXapi.App_Data.SLXapiDB.%ClassName%Row row = (SLXapi.App_Data.SLXapiDB.%ClassName%Row)table.Rows[0];\r\n" +
"\r\n" +
"            return %ClassName%.LoadFromModel(row);\r\n" +
"        }\r\n" +
"\r\n" +
"        static bool Delete(%ClassName% p)\r\n" +
"        {\r\n" +
"            throw new NotImplementedException();\r\n" +
"        }\r\n" +
"\r\n" +
"        static bool Insert(%ClassName% j)\r\n" +
"        {\r\n" +
"            throw new NotImplementedException();\r\n" +
"        }\r\n" +
"\r\n" +
"        static bool Update(%ClassName% j)\r\n" +
"        {\r\n" +
"            j.ModifyDate = DateTime.Now;\r\n" +
"            throw new NotImplementedException();\r\n" +
"        }\r\n" +
"\r\n" +
"        static List<%ClassName%> LoadFromModel(SLXapi.App_Data.SLXapiDB.%ClassName%DataTable table)\r\n" +
"        {\r\n" +
"            List<%ClassName%> list = new List<%ClassName%>();\r\n" +
"            foreach (SLXapi.App_Data.SLXapiDB.%ClassName%Row row in table.Rows)\r\n" +
"            {\r\n" +
"                %ClassName% d = %ClassName%.LoadFromModel(row);\r\n" +
"                list.Add(d);\r\n" +
"            }\r\n" +
"            return list;\r\n" +
"\r\n" +
"        }\r\n" +
"\r\n" +
"        static %ClassName% LoadFromModel(SLXapi.App_Data.SLXapiDB.%ClassName%Row row)\r\n" +
"        {\r\n" +
"            %ClassName% d = new %ClassName%();\r\n" +
"            Load%ClassName%(row, d);\r\n" +
"            return d;\r\n" +
"        }";



    }
}
