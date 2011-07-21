using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace ORMCreator
{
    public class DBField
    {
        enum Type
        {
            Integer,
            Double,
            Float,
            Boolean,
            String,
            DateTime
        }
        public int FieldType { get; set; }
        public string FieldName { get; set; }

        public DBField()
        {
        }

        string ParseFieldType(string strInput)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };

            string ret;
            string[] words = strInput.Split(delimiterChars);
            ret = words[words.GetUpperBound(0)];
            return ret;
        }
        string ParseFieldName(string strInput)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            char[] removeChars = { '[', ']' };
            string ret;
            string[] words = strInput.Split(delimiterChars);
            ret = words[words.GetUpperBound(0) -1];
            ret = ret.TrimStart(removeChars);
            ret = ret.TrimEnd(removeChars);
            return ret;
        }
        void SetType(string strInput)
        {
            if (strInput.ToLower() == "string" || strInput.ToLower() == "standardid" || strInput.ToLower() == "varchar" || strInput.ToLower() == "char")
            {
                FieldType = (int)Type.String;
                return;
            }
            if (strInput.ToLower() == "int")
            {
                FieldType = (int)Type.Integer;
                return;
            }
            if (strInput.ToLower() == "datetime")
            {
                FieldType = (int)Type.DateTime;
                return;
            }
            if (strInput.ToLower() == "boolean" || strInput.ToLower() == "bool")
            {
                FieldType = (int)Type.Boolean;
                return;
            }

            FieldType = (int)Type.String;
        }
        void SetName(string strInput)
        {
            FieldName = strInput.ToUpper();
        }
        public void Load(string strInput)
        {
            string strName = ParseFieldName(strInput);
            string strType = ParseFieldType(strInput);

            SetType(strType);
            SetName(strName);
           

        }
        public string PrintMemberCode()
        {

            switch ((Type)FieldType)
            {
                case Type.Integer:
                {
                    return "public int "+ FieldName+" { get; set; }";
                   
                }
                case Type.Double:
                {
                    return "public double " + FieldName + " { get; set; }";
                    
                }
                case Type.Float:
                {
                    return "public float " + FieldName + " { get; set; }";
                    
                }
                case Type.Boolean:
                {
                    return "public bool " + FieldName + " { get; set; }";
                   
                }
                case Type.String:
                {
                    return "public string " + FieldName + " { get; set; }";
                    
                }
                case Type.DateTime:
                {
                    return "public DateTime " + FieldName + " { get; set; }";
                 
                }
                default:
                {

                    break;
                }
            }
            return "public string Entry " + FieldName + " { get; set; }"; ;
        }

    }
    public class DBTable
    {
        public List<DBField> dbFields { get; set; }
        public string Name { get; set; }

        public DBTable()
        {
            Name = String.Empty;
            dbFields = null;
        }
        void SetName(string strInput)
        {
            Name = strInput;
        }
        public bool LoadFile(string filename)
        {
            try
            {
                System.IO.StreamReader file =
                  new System.IO.StreamReader(filename);

                if (file == null) return false;
                dbFields = new List<DBField>();
                int counter = 0;
                string line;
                line = file.ReadLine();
                SetName(line);
                while ((line = file.ReadLine()) != null)
                {
                    DBField dbField = new DBField();
                    dbField.Load(line);
                    if (dbField.FieldName == String.Empty)
                        continue;
                    dbFields.Add(dbField);
                    Console.WriteLine(dbField.PrintMemberCode());
                    counter++;
                }
                file.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           

        }
        public bool Save(string filename)
        {
            return false;
        }
    }
}
