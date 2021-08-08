using System;
using System.Threading;

namespace ConsoleAppDBTxT
{
    class Program
    {
        private static DataText conDB;
        static void Main(string[] args)
        {
            //new file as one table db
            conDB = new DataText();
            
            // conDB.Path = "C://Users//LENOVO//source//repos//ConsoleAppDBTxT//ConsoleAppDBTxT//bin//Debug/db.txt"; // dont use '\'!!
            conDB.Path = "db.txt"; // dont use '\'!!

            Console.WriteLine("================================");
            Console.WriteLine("Test 1 - ReadAll Info");
            Console.WriteLine("================================");
            /*test 1 - ReadAll and ReadById */
            ReadAll();
            Thread.Sleep(500);


            Console.WriteLine("================================");
            Console.WriteLine("Test 2 - Insert new info ");
            Console.WriteLine("================================");
            /* test 2 */
            InsertInto("Merita", "Hoti", "3344", "meri@hoti.net", "www.meri.net");
            Thread.Sleep(500);

            /*test 3 - ReadAll and ReadById */
            Console.WriteLine("================================");
            Console.WriteLine("Test 3 - Read All Info from DB ");
            Console.WriteLine("================================");
            ReadAll();
            Thread.Sleep(500);

            /*test 4 - update by id = 2 */
            Console.WriteLine("================================");
            Console.WriteLine("Test 4 - Update info with DB ID = 2  ");
            Console.WriteLine("================================");
            UpdateById("2", "Nard", "Nard", "22334455", "nard@hoti.net", "www.nard.net");
            Thread.Sleep(500);

            /*test 5 - ReadAll and ReadById */
            Console.WriteLine("================================");
            Console.WriteLine("Test 5 - Read All Info after update  ");
            Console.WriteLine("================================");
            ReadAll();
            Thread.Sleep(500);

            /*test 6 - delete by id = 0 */
            Console.WriteLine("================================");
            Console.WriteLine("Test 6 - Delete info from DB with ID = 0  ");
            Console.WriteLine("================================");
            DeleteById("0");
            Thread.Sleep(500);

            /*test 7 - ReadAll and ReadById */
            Console.WriteLine("================================");
            Console.WriteLine("Test 7 - Read All Info after DELETE  ");
            Console.WriteLine("================================");
            ReadAll();
            Thread.Sleep(500);

            /*test 8 - Find id for searched info  */
            Console.WriteLine("================================");
            Console.WriteLine("Test 8 - Find it  ");
            Console.WriteLine("================================");
            FindId("Proti", "1");

            int cnt = 1;

            while(true)
            {
                Thread.Sleep(3000);
                Console.WriteLine("Cnt = " + cnt.ToString());
                cnt++;
            }
        }

        private static void ReadAll()
        {
            int temp = conDB.Entries();
            if (temp > 0)
            {
                temp--;
                for (int i = 0; i <= temp; i++)
                {
                    ReadById(i);
                }
            }
        }
        private static void ReadById(int id)
        {
            Info.DbID = id.ToString();
            string str_name = "";
            string str_lastname = "";
            string str_phone = "";
            string str_mail = "";
            string str_web = "";

            bool result = conDB.ReadEntrie(id, ref str_name, ref str_lastname, ref str_phone, ref str_mail, ref str_web);
            
            Info.Name = str_name;
            Info.SurName = str_lastname;
            Info.Number = str_phone;
            Info.Email = str_mail;
            Info.Web = str_web;

            if (!result) {
                Console.WriteLine("Invalid record \n"); 
            }
            else
            {
                Console.WriteLine("DB ID = " + Info.DbID);
                Console.WriteLine("Name = " + Info.Name);
                Console.WriteLine("Surname = " + Info.SurName);
                Console.WriteLine("Number = " + Info.Number);
                Console.WriteLine("Email = " + Info.Email);
                Console.WriteLine("Web = " + Info.Web);
                Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-+-+");

            }
        }


        private static void InsertInto(string name, string surname, string number, string mail, string web)
        {
            bool result = conDB.InsertEntrie(name, surname, number, mail, web);
            Info.DbID = Convert.ToString(conDB.Entries() - 1);
            if(result)
            {
                Console.WriteLine("Record inserted and his id = " + Info.DbID);
                Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-+-+");
                
            }
        }

        private static void UpdateById(string ids,string name,string surname,string number,string mail,string web)
        {
            int id = Convert.ToInt32(ids);
            bool result = conDB.UpdateEntrie(id, name, surname, number, mail, web);
            if (result)
            {
                Console.WriteLine("Entry id = " + ids + " updated");
                Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-+-+");

            }
        }

        private static void DeleteById(string ids)
        {
            int id = Convert.ToInt32(ids);
            bool result = conDB.DeleteEntrie(id);
            if (result)
            {
                Console.WriteLine("Record with id = " + ids + " deleted");
                Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-+-+");
            }
        }

        private static void FindId(string search,string columnno)
        {
            string results = conDB.Select(search, Convert.ToInt32(columnno));
            Console.WriteLine("ID of record = " + results);
            Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-+-+");

        }
    }
}
