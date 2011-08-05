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
            strInput = strInput.Trim();
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
            if (strInput.ToLower() == "datetime" || strInput.ToLower() == "date")
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
            FieldName = strInput.ToLower();
        }
        public void Load(string strInput)
        {
            string strName = ParseFieldName(strInput);
            string strType = ParseFieldType(strInput);

            SetType(strType);
            SetName(strName);
           

        }

        public string PrintCopyConstructorCode()
        {
            return "             this." + FieldName + " = o." + FieldName + ";"; 
        }

        public string PrintConstructorCode()
        {
            return "             this." + FieldName + " = " + GetFieldDefault() + ";";
        }


        public string PrintUpdateSqlCode(bool isFirst)
        {
            if(isFirst)
                return "             " + FieldName + " = @" + FieldName;
            return "             ," + FieldName + " = @" + FieldName;
        }


        public string PrintEqualsCode()
        {
            if (FieldType == (int)Type.DateTime)
            {
                return "             if(this." + FieldName + ".ToString() != o." + FieldName + ".ToString())return false;"; 
            }
            return "             if(this." + FieldName + " != o." + FieldName + ")return false;"; 

        }

        public string PrintLoadCode()
        {
            return
"            try { d."+FieldName+" = row."+FieldName.ToUpper()+"; }\r\n" +
"            catch (Exception) { }\r\n\r\n";
            
        }

        public string GetFieldDefault()
        {
            switch ((Type)FieldType)
            {
                case Type.Integer:
                case Type.Double:
                case Type.Float:
                    {
                        return "0";

                    }
               
                    
                case Type.Boolean:
                    {
                        return "false";

                    }
                case Type.String:
                    {
                        return "String.Empty";

                    }
                case Type.DateTime:
                    {
                        return "Convert.ToDateTime(@\"1/1/1753\")";

                    }
                default:
                    {

                        break;
                    }
            }
            return "String.Empty";
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
        public string TableAdapter { get; set; }

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
                line = file.ReadLine();
                TableAdapter = line;
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

        void PrintUpdateFunction(TextWriter tw)
        {
            string str = Attributes.UpdateByIDStart.Replace("%ClassName%", String.Format("{0}", Name)).Replace("%TableAdapter%", String.Format("{0}", TableAdapter));
            str = str.Replace("%classname%", String.Format("{0}", Name.ToLower()));
            str = str.Replace("%primarykey%", String.Format("{0}", dbFields[0].FieldName));
            tw.WriteLine(str);

            for (int i = 1; i < dbFields.Count; ++i)
            {
                tw.WriteLine("          o."+dbFields[i].FieldName+",");
            }
            tw.WriteLine("          o." + dbFields[0].FieldName + "\r\n");
            

            str = Attributes.UpdateByIDEnd.Replace("%ClassName%", String.Format("{0}", Name)).Replace("%TableAdapter%", String.Format("{0}", TableAdapter));
            str = str.Replace("%classname%", String.Format("{0}", Name.ToLower()));
            str = str.Replace("%primarykey%", String.Format("{0}", dbFields[0].FieldName));
            tw.WriteLine(str);
        }

        void PrintCopyContructor(TextWriter tw)
        {
            string str = Attributes.ClassConstructorStart.Replace("%ClassName%", String.Format("{0}", Name));
            str = str.Replace("%classname%", String.Format("{0}", Name.ToLower()));
            str = str.Replace("%primarykey%", String.Format("{0}", dbFields[0].FieldName));
            tw.WriteLine(str);

            foreach (DBField field in dbFields)
            {
                tw.WriteLine(field.PrintConstructorCode());
            }

            str = Attributes.ClassConstructorEnd.Replace("%ClassName%", String.Format("{0}", Name));
            str = str.Replace("%classname%", String.Format("{0}", Name.ToLower()));
            str = str.Replace("%primarykey%", String.Format("{0}", dbFields[0].FieldName));

            tw.WriteLine(str);


            str = Attributes.ClassCopyConstructor.Replace("%ClassName%", String.Format("{0}", Name));
            str = str.Replace("%classname%", String.Format("{0}", Name.ToLower()));
            str = str.Replace("%primarykey%", String.Format("{0}", dbFields[0].FieldName));
            tw.WriteLine(str);


            str = Attributes.CopyFunctionStart.Replace("%ClassName%", String.Format("{0}", Name));
            str = str.Replace("%classname%", String.Format("{0}", Name.ToLower()));
            str = str.Replace("%primarykey%", String.Format("{0}", dbFields[0].FieldName));

            tw.WriteLine(str);

            foreach (DBField field in dbFields)
            {
                tw.WriteLine(field.PrintCopyConstructorCode());
            }
            tw.WriteLine(Attributes.CloseFunction);

        }

        void PrintUpdateSLQ(TextWriter tw)
        {
            string str = Attributes.UpdateSqlStart.Replace("%ClassName%", String.Format("{0}", Name));
            str = str.Replace("%classname%", String.Format("{0}", Name.ToLower()));
            str = str.Replace("%primarykey%", String.Format("{0}", dbFields[0].FieldName));
            tw.WriteLine(str);

            bool isFirst = true;
            //start at 1 to skip primary key in the set values
            for (int i = 1; i < dbFields.Count; ++i)
            {
                tw.WriteLine(dbFields[i].PrintUpdateSqlCode(isFirst));
                isFirst = false;
            }
            

            str = Attributes.UpdateSqlEnd.Replace("%ClassName%", String.Format("{0}", Name));
            str = str.Replace("%classname%", String.Format("{0}", Name.ToLower()));
            str = str.Replace("%primarykey%", String.Format("{0}", dbFields[0].FieldName));
            tw.WriteLine(str);

        }

        void PrintEquals(TextWriter tw)
        {
            string str = Attributes.EqualsFunctionStart.Replace("%ClassName%", String.Format("{0}", Name));
            str = str.Replace("%classname%", String.Format("{0}", Name.ToLower()));
            str = str.Replace("%primarykey%", String.Format("{0}", dbFields[0].FieldName));
            tw.WriteLine(str);
            
            foreach (DBField field in dbFields)
            {
                tw.WriteLine(field.PrintEqualsCode());
            }
            tw.WriteLine(Attributes.ReturnTrue);
            tw.WriteLine(Attributes.CloseFunction);

        }

        void PrintPublicFunctions(TextWriter tw)
        {
            string str = Attributes.PublicStaticFunctions.Replace("%ClassName%", String.Format("{0}", Name)).Replace("%TableAdapter%", String.Format("{0}", TableAdapter));
            str = str.Replace("%classname%", String.Format("{0}", Name.ToLower()));
            str = str.Replace("%primarykey%", String.Format("{0}", dbFields[0].FieldName));

            tw.WriteLine(str);
        }

        void PrintLoadFunction(TextWriter tw)
        {
            string str = Attributes.LoadFunctionStart.Replace("%ClassName%", String.Format("{0}", Name));
            str = str.Replace("%classname%", String.Format("{0}", Name.ToLower()));
            str = str.Replace("%primarykey%", String.Format("{0}", dbFields[0].FieldName));

            tw.WriteLine(str);
            foreach (DBField field in dbFields)
            {
                tw.WriteLine(field.PrintLoadCode());
            }
            tw.WriteLine(Attributes.CloseFunction);

        }

        public bool Save(string filename)
        {
            // create a writer and open the file
            TextWriter tw = new StreamWriter(filename);

            // write a line of text to the file
            tw.WriteLine(Attributes.Includes);
            string startClass = Attributes.StartClass.Replace("%ClassName%",String.Format("{0}", Name));
            startClass = startClass.Replace("%classname%", String.Format("{0}", Name.ToLower()));
            startClass = startClass.Replace("%primarykey%", String.Format("{0}", dbFields[0].FieldName));
            tw.WriteLine(startClass);

            foreach (DBField field in dbFields)
            {
                tw.WriteLine(Attributes.MemeberIndent + field.PrintMemberCode());
            }
            string serializationCode = Attributes.ClassXMLSerialization.Replace("%ClassName%", String.Format("{0}", Name));
            serializationCode = serializationCode.Replace("%classname%", String.Format("{0}", Name.ToLower()));
            serializationCode = serializationCode.Replace("%primarykey%", String.Format("{0}", dbFields[0].FieldName));
            tw.WriteLine(serializationCode);

            PrintCopyContructor(tw);

            PrintEquals(tw);

            PrintUpdateFunction(tw);

            PrintPublicFunctions(tw);

            PrintLoadFunction(tw);

            tw.WriteLine(Attributes.CloseNamespace);
            tw.WriteLine(Attributes.CloseClass);

            PrintUpdateSLQ(tw);
            
            // close the stream
            tw.Close();
            return true;
        }
       
    }
}
