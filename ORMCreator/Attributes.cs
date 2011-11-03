using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMCreator
{
    class Attributes
    {

        public static string Includes(string namespaceStr)
        {
            return "using System;\r\n" +
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
        }

        public static string StartClass(string namespaceStr)
        {
            return
                "namespace " + namespaceStr + "\r\n" +
                "{\r\n" +
                "    [XmlRootAttribute(ElementName = \"%ClassName%\")]\r\n" +
                "    public class %ClassName%\r\n" +
                "    {\r\n";
        }

        public static string MemeberIndent = "        ";
        public static string CloseClass =  "}";
        public static string CloseNamespace = "   }\r\n";
        public static string CloseFunction = 
"        }\r\n";
        public static string ReturnTrue =
"             return true;\r\n";

        public static string ClassXMLSerialization(string namespaceStr)
        {
            return
                "\r\n\r\n\r\n" +
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
                "                XmlizedString = " + namespaceStr + ".Helpers.Serialization.UTF8ByteArrayToString(memoryStream.ToArray());\r\n" +
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
        }

        public static string ClassConstructorStart =
"\r\n\r\n\r\n" +
"        public %ClassName%()\r\n" +
"        {\r\n";

        public static string ClassConstructorEnd =     
"        }\r\n" +
"\r\n";
        public static string ClassCopyConstructor =       
"\r\n\r\n\r\n" +
"        public %ClassName%(%ClassName% o)\r\n" +
"        {\r\n" +
"            Copy(o);\r\n" +
"        }\r\n";

        public static string CopyFunctionStart =
"        public void Copy(%ClassName% o)\r\n" +
"        {\r\n";

        public static string EqualsFunctionStart =
"        public bool Equals(%ClassName% o)\r\n" +
"        {\r\n";

        public static string LoadFunctionStart(string namespaceStr)
        {
            return
                "       static public void Load%ClassName%(" + namespaceStr + ".App_Data." + namespaceStr + "DB.%ClassName%Row row, %ClassName% d)\r\n" +
                "        {\r\n";
        }

        public static string UpdateSqlStart =
"       /*UPDATE %ClassName% Set \r\n";
        public static string UpdateSqlEnd =
"       WHERE %primarykey% = @%primarykey% */\r\n";

        public static string InsertSqlStart =
"       /*INSERT INTO %ClassName%(%primarykey%\r\n"; 
        public static string InsertSqlMiddle = 
"       )\r\n"+
"       VALUES\r\n"+
"       (@%primarykey%\r\n";
        public static string InsertSqlEnd = 
"       );SELECT SCOPE_IDENTITY()*/\r\n";

        /*
         INSERT INTO [SLXPRI].[sysdba].[ADDRESS]
           ([ADDRESSID]
           ,[ENTITYID]
           ,[TYPE]
           ,[DESCRIPTION]
           ,[ADDRESS1]
           ,[ADDRESS2]
           ,[CITY]
           ,[STATE]
           ,[POSTALCODE]
           ,[COUNTY]
           ,[COUNTRY]
           ,[ISPRIMARY]
           ,[ISMAILING]
           ,[SALUTATION]
           ,[CREATEDATE]
           ,[CREATEUSER]
           ,[MODIFYDATE]
           ,[MODIFYUSER]
           ,[ROUTING]
           ,[ADDRESS3]
           ,[ADDRESS4]
           ,[TIMEZONE])
     VALUES
           (<ADDRESSID, STANDARDID,>
           ,<ENTITYID, STANDARDID,>
           ,<TYPE, varchar(64),>
           ,<DESCRIPTION, varchar(64),>
           ,<ADDRESS1, varchar(64),>
           ,<ADDRESS2, varchar(64),>
           ,<CITY, varchar(32),>
           ,<STATE, varchar(32),>
           ,<POSTALCODE, varchar(24),>
           ,<COUNTY, varchar(32),>
           ,<COUNTRY, varchar(32),>
           ,<ISPRIMARY, BOOLEAN,>
           ,<ISMAILING, BOOLEAN,>
           ,<SALUTATION, varchar(64),>
           ,<CREATEDATE, datetime,>
           ,<CREATEUSER, STANDARDID,>
           ,<MODIFYDATE, datetime,>
           ,<MODIFYUSER, STANDARDID,>
           ,<ROUTING, varchar(64),>
           ,<ADDRESS3, varchar(64),>
           ,<ADDRESS4, varchar(64),>
           ,<TIMEZONE, varchar(64),>)
         */



        public static string UpdateByIDStart(string namespaceStr)
        {
            return
                "        public static bool UpdateByID(%ClassName% o)\r\n" +
                "        {\r\n" +
                "            DateTime? d = null;\r\n" +
                "            o.modifydate = DateTime.Now;\r\n" +
                "            " + namespaceStr + ".App_Data." + namespaceStr + "DBTableAdapters.%TableAdapter% ta = new " + namespaceStr + ".App_Data." + namespaceStr + "DBTableAdapters.%TableAdapter%();\r\n" +
                "            int ret  = ta.UpdateByID(";
        }

        public static string UpdateByIDEnd =
"           );\r\n" +
"           if(ret == 1) return true;\r\n" +
"           return false;\r\n" +
"       }\r\n";

        public static string InsertStart(string namespaceStr)
        {
            return
                "        public static bool Insert(%ClassName% o)\r\n" +
                "        {\r\n" +
                "            DateTime? d = null;\r\n" +
                "            o.createdate = o.modifydate = DateTime.Now;\r\n" +
                "            " + namespaceStr + ".App_Data." + namespaceStr + "DBTableAdapters.%TableAdapter% ta = new " + namespaceStr + ".App_Data." + namespaceStr + "DBTableAdapters.%TableAdapter%();\r\n" +
                "            ta.InsertQuery(";
        }

        public static string InsertEnd =
"           );\r\n" +
"           return true;\r\n" +
"       }\r\n";


        public static string PublicStaticFunctions(string namespaceStr)
        {
            return

                "        public static bool Exists(%ClassName% o)\r\n" +
                "        {\r\n" +
                "            return Exists(o.%primarykey%);\r\n" +
                "        }\r\n" +
                "        public static bool Exists(string id)\r\n" +
                "        {\r\n" +
                "            " + namespaceStr + ".App_Data." + namespaceStr + "DBTableAdapters.%TableAdapter% ta = new " + namespaceStr + ".App_Data." + namespaceStr + "DBTableAdapters.%TableAdapter%();\r\n" +
                "            if (ta.GetSumByID(id) == 0)\r\n" +
                "                return false;\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "\r\n" +
                "        public static bool isValid(%ClassName% o)\r\n" +
                "        {\r\n" +
                "            if (o.%primarykey% == String.Empty)\r\n" +
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
                "            %ClassName% ret = %ClassName%.GetByID(%primarykey%);\r\n" +
                "            if(ret == null)\r\n" +
                "               return false;\r\n" +
                "            Copy(ret);\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "\r\n" +
                "        public static List<%ClassName%> GetAll()\r\n" +
                "        {\r\n" +
                "            " + namespaceStr + ".App_Data." + namespaceStr + "DBTableAdapters.%TableAdapter% ta = new " + namespaceStr + ".App_Data." + namespaceStr + "DBTableAdapters.%TableAdapter%();\r\n" +
                "            " + namespaceStr + ".App_Data." + namespaceStr + "DB.%ClassName%DataTable table = ta.GetData();\r\n" +
                "            return LoadFromModel(table);\r\n" +
                "        }\r\n" +
                "\r\n" +
                "        public static %ClassName% GetByID(string id)\r\n" +
                "        {\r\n" +
                "            try{\r\n" +
                "               " + namespaceStr + ".App_Data." + namespaceStr + "DBTableAdapters.%TableAdapter% ta = new " + namespaceStr + ".App_Data." + namespaceStr + "DBTableAdapters.%TableAdapter%();\r\n" +
                "               " + namespaceStr + ".App_Data." + namespaceStr + "DB.%ClassName%DataTable table = ta.GetDataByID(id);\r\n" +
                "               " + namespaceStr + ".App_Data." + namespaceStr + "DB.%ClassName%Row row = (" + namespaceStr + ".App_Data." + namespaceStr + "DB.%ClassName%Row)table.Rows[0];\r\n" +
                "\r\n" +
                "               return %ClassName%.LoadFromModel(row);\r\n" +
                "           }\r\n" +
                "           catch(Exception){\r\n" +
                "               return null;\r\n" +
                "           }\r\n" +
                "        }\r\n" +
                "\r\n" +
                "        static bool Delete(%ClassName% p)\r\n" +
                "        {\r\n" +
                "            throw new NotImplementedException();\r\n" +
                "        }\r\n" +
                "\r\n" +
                "        static bool Update(%ClassName% j)\r\n" +
                "        {\r\n" +
                "            return UpdateByID(j);\r\n" +
                "        }\r\n" +
                "\r\n" +
                "        static List<%ClassName%> LoadFromModel(" + namespaceStr + ".App_Data." + namespaceStr + "DB.%ClassName%DataTable table)\r\n" +
                "        {\r\n" +
                "            List<%ClassName%> list = new List<%ClassName%>();\r\n" +
                "            foreach (" + namespaceStr + ".App_Data." + namespaceStr + "DB.%ClassName%Row row in table.Rows)\r\n" +
                "            {\r\n" +
                "                %ClassName% d = %ClassName%.LoadFromModel(row);\r\n" +
                "                list.Add(d);\r\n" +
                "            }\r\n" +
                "            return list;\r\n" +
                "\r\n" +
                "        }\r\n" +
                "\r\n" +
                "        static %ClassName% LoadFromModel(" + namespaceStr + ".App_Data." + namespaceStr + "DB.%ClassName%Row row)\r\n" +
                "        {\r\n" +
                "            %ClassName% d = new %ClassName%();\r\n" +
                "            Load%ClassName%(row, d);\r\n" +
                "            return d;\r\n" +
                "        }";
        }



    }
}
