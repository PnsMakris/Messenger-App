using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AFDE_Project
{
    class OptionScreen
    {
        // Method for the Main Screen of the program
        public static void Connection()
        {
            var now = DateTime.Now;
            Console.WriteLine($"***** {now.ToLongDateString()} *****\n");
            Console.WriteLine("Press (1) to Sign in.");
            Console.WriteLine("Press (2) to Register a new account.");
            Console.WriteLine("Press (3) to Exit the program.\n");

            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");

            switch (choice)
            {
                case 3:
                    Console.WriteLine("Good Bye !!!");
                    Environment.Exit(2);
                    break;
                case 2:
                    UserInsertion.CreateNewUser();
                    Connection();
                    break;
                case 1:
                    UserInsertion.UserLogin();
                    break;
                default:
                    Console.WriteLine("Not a legal entry. Try again later !!!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    Connection();
                    break;
            }
        }



    }
}
