using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMCreator;
using System.IO;

namespace ORMDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: <inputfile> <outputfile>");
            }
            DBTable dbTable = new DBTable();
            dbTable.LoadFile(args[0]);
            dbTable.Save(args[1]);

            /*
             * dbTable.LoadFile("ContactData.txt");
            dbTable.Save("SavedData.cs");
             * */
        }
    }
}
