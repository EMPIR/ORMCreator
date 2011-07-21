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
            /*int counter = 0;
            string line;
            string strCWD = Directory.GetCurrentDirectory();
            // Read the file and display it line by line.
            System.IO.StreamReader file =
               new System.IO.StreamReader("ContactData.txt");
            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine(line);
                counter++;
            }

            file.Close();
             



            // Suspend the screen.
            Console.ReadLine();
             * */

            DBTable dbTable = new DBTable();
            dbTable.LoadFile("ContactData.txt");
        }
    }
}
