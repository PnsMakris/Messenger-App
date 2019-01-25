using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AFDE_Project
{
    class LoginMenu
    {
        // Super Admin Menu Selection
        public static void SuperAdminMenu(string user)
        {
            Console.WriteLine("(1) -- Assign a Role to a User.");
            Console.WriteLine("(2) -- View User's Sent Messages.");
            Console.WriteLine("(3) -- View User's Received Messages.");
            Console.WriteLine("(4) -- Create a new User Profile.");
            Console.WriteLine("(5) -- Update a User's Profile.");
            Console.WriteLine("(6) -- Delete a User's Profile.");
            Console.WriteLine("(7) -- Edit an Existing Message.");
            Console.WriteLine("(8) -- Send a new Message.");
            Console.WriteLine("(9) -- View your Sent Messages.");
            Console.WriteLine("(10) -- View your Received Messages.");
            Console.WriteLine("(11) -- Exit the Program.\n");

            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");

            switch (choice)
            {
                case 11:
                    Console.WriteLine("Good Bye !!!");
                    Environment.Exit(2);
                    break;
                case 10:
                    Message.ViewReceivedMessages(user);
                    SuperAdminMenu(user);
                    break;
                case 9:
                    Message.ViewSentMessages(user);
                    SuperAdminMenu(user);
                    break;
                case 8:
                    Message.SendNewMessage(user);
                    SuperAdminMenu(user);
                    break;
                case 7:Message.EditExistingMessage();
                    SuperAdminMenu(user);
                    break;
                case 6:
                    UserInsertion.DeleteUser();
                    SuperAdminMenu(user);
                    break;
                case 5:
                    UserInsertion.UpdateUser();
                    SuperAdminMenu(user);
                    break;
                case 4:
                    UserInsertion.CreateNewUser();
                    SuperAdminMenu(user);
                    break;
                case 3:
                    Console.WriteLine("Enter UserName to View his Received Messages:");
                    string usersNameReceivedMessages = Console.ReadLine();
                    Console.WriteLine("\n");
                    Message.ViewReceivedMessages(usersNameReceivedMessages);
                    SuperAdminMenu(user);
                    break;
                case 2:
                    Console.WriteLine("Enter UserName to View his Sent Messages:");
                    string usersNameSentMessages = Console.ReadLine();
                    Console.WriteLine("\n");
                    Message.ViewSentMessages(usersNameSentMessages);
                    SuperAdminMenu(user);
                    break;
                case 1:
                    UserInsertion.AssignRoleToUser();
                    SuperAdminMenu(user);
                    break;
                default:
                    Console.WriteLine("Not a legal entry. Try again!!!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    SuperAdminMenu(user);
                    break;
            }
            
        }
        
        // Admin Menu Selection
        public static void AdminMenu(string user)
        {
            Console.WriteLine("(1) -- Create a new User Profile.");
            Console.WriteLine("(2) -- Update a User's Profile.");
            Console.WriteLine("(3) -- Delete a User's Profile.");
            Console.WriteLine("(4) -- Edit an Existing Message.");
            Console.WriteLine("(5) -- Send a new Message.");
            Console.WriteLine("(6) -- View your Sent Messages.");
            Console.WriteLine("(7) -- View your Received Messages.");
            Console.WriteLine("(8) -- Exit the Program.\n");

            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");

            switch (choice)
            {
                case 8:
                    Console.WriteLine("Good Bye !!!");
                    Environment.Exit(2);
                    break;
                case 7:
                    Message.ViewReceivedMessages(user);
                    AdminMenu(user);
                    break;
                case 6:
                    Message.ViewSentMessages(user);
                    AdminMenu(user);
                    break;
                case 5:
                    Message.SendNewMessage(user);
                    AdminMenu(user);
                    break;
                case 4:
                    Message.EditExistingMessage();
                    AdminMenu(user);
                    break;
                case 3:
                    UserInsertion.DeleteUser();
                    AdminMenu(user);
                    break;
                case 2:
                    UserInsertion.UpdateUser();
                    AdminMenu(user);
                    break;
                case 1:
                    UserInsertion.CreateNewUser();
                    AdminMenu(user);
                    break;
                default:
                    Console.WriteLine("Not a legal entry. Try again!!!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    AdminMenu(user);
                    break;
            }

        }

        // Common User Menu Selection
        public static void CommonUserMenu(string user)
        {
            Console.WriteLine("(1) -- Send a new Message.");
            Console.WriteLine("(2) -- View your Sent Messages.");
            Console.WriteLine("(3) -- View your Received Messages.");
            Console.WriteLine("(4) -- Exit the Program.\n");

            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");

            switch (choice)
            {
                case 4:
                    Console.WriteLine("Good Bye !!!");
                    Environment.Exit(2);
                    break;
                case 3:
                    Message.ViewReceivedMessages(user);
                    CommonUserMenu(user);
                    break;
                case 2:
                    Message.ViewSentMessages(user);
                    CommonUserMenu(user);
                    break;
                case 1:
                    Message.SendNewMessage(user);
                    CommonUserMenu(user);
                    break;
                default:
                    Console.WriteLine("Not a legal entry. Try again!!!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    CommonUserMenu(user);
                    break;
            }
        }

        // Check of User's Role which will determine which Menu will appear on the screen 
        public static void Menu(string user)
        {
            // Connection with the Database
            string connectionString =
            @"Server = LAPTOP-7FF4UD5B\SQLEXPRESS;Database = afdeDB; Trusted_Connection = True;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            using (sqlConnection)
            {
                try
                {
                    // Check if the connection with Database is already open
                    if (sqlConnection.State == ConnectionState.Open)
                    {
                        sqlConnection.Close();
                    }
                    sqlConnection.Open();

                    SqlCommand cmdSelect = new SqlCommand($"SELECT Role FROM UsersInfo WHERE UserName = '{user}'", sqlConnection);
                    var choice = cmdSelect.ExecuteScalar();

                    switch (choice)
                    {
                        case 3:
                            CommonUserMenu(user);
                            break;
                        case 2:
                            AdminMenu(user);
                            break;
                        case 1:
                            SuperAdminMenu(user);
                            break;
                        default:
                            Console.WriteLine("Not a legal entry. Try again!!!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }

            }





        }


    }
}
