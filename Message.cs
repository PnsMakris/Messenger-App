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
    class Message
    {
        // Properties
        public string ID { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public DateTime DateOfSubmission { get; set; }
        public string TextMessage { get; set; }

        // Method to Send a new Message
        public static void SendNewMessage(string userName)
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

                    Message message = new Message();
                    message.Sender = userName;
                    Console.WriteLine("Enter the UserName of the Receiver.");
                    message.Receiver = Console.ReadLine();
                    Console.WriteLine("Enter your Text Message up to 250 characters");
                    message.TextMessage = Console.ReadLine();
                    message.DateOfSubmission = DateTime.Now;

                    SqlCommand cmdInsert = new SqlCommand($"INSERT INTO MessageInfo(Sender, Receiver, DateOfSubmission, TextMessage) VALUES('{message.Sender}', '{message.Receiver}', '{message.DateOfSubmission}', '{message.TextMessage}')", sqlConnection);
                    int rowsInserted = cmdInsert.ExecuteNonQuery();
                    if (rowsInserted > 0)
                    {
                        Console.WriteLine("Insertion Successful");
                        Console.WriteLine($"{rowsInserted} rows inserted Successfully");
                    }

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Public\afdemp\TextMessages.txt", true))
                    {
                        file.WriteLine($"Sender is: {message.Sender}, Reciever is: {message.Receiver}, Message is: ({message.TextMessage}), Date is: {message.DateOfSubmission}");
                        file.WriteLine(" ");
                    }
                    Thread.Sleep(3000);
                    Console.Clear();
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

        // Method to View the Received Messages
        public static void ViewReceivedMessages(string userName)
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

                    SqlCommand cmdSelect = new SqlCommand($"SELECT * FROM MessageInfo WHERE Receiver = '{userName}'", sqlConnection);
                    SqlDataReader reader = cmdSelect.ExecuteReader();
                    while (reader.Read())
                    {
                        Message receivedMessage = new Message();
                        receivedMessage.ID = reader.GetInt32(0).ToString();
                        receivedMessage.Sender = reader.GetString(1);
                        receivedMessage.Receiver = reader.GetString(2);
                        receivedMessage.DateOfSubmission = reader.GetDateTime(3);
                        receivedMessage.TextMessage = reader.GetString(4);
                        Console.WriteLine(receivedMessage);
                    }
                    reader.Close();
                    Console.WriteLine("\n\n");
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

        // Method to View Sent Messages
        public static void ViewSentMessages(string userName)
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

                    SqlCommand cmdSelect = new SqlCommand($"SELECT * FROM MessageInfo WHERE Sender = '{userName}'", sqlConnection);
                    SqlDataReader reader = cmdSelect.ExecuteReader();
                    while (reader.Read())
                    {
                        Message sentMessage = new Message();
                        sentMessage.ID = reader.GetInt32(0).ToString();
                        sentMessage.Sender = reader.GetString(1);
                        sentMessage.Receiver = reader.GetString(2);
                        sentMessage.DateOfSubmission = reader.GetDateTime(3);
                        sentMessage.TextMessage = reader.GetString(4);
                        Console.WriteLine(sentMessage);
                    }
                    reader.Close();
                    Console.WriteLine("\n\n");
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

        // Method to Edit Existing Messages
        public static void EditExistingMessage()
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

                    SqlCommand cmdSelect = new SqlCommand($"SELECT * FROM MessageInfo", sqlConnection);
                    SqlDataReader reader = cmdSelect.ExecuteReader();
                    while (reader.Read())
                    {
                        Message existingMessage = new Message();
                        existingMessage.ID = reader.GetInt32(0).ToString();
                        existingMessage.Sender = reader.GetString(1);
                        existingMessage.Receiver = reader.GetString(2);
                        existingMessage.DateOfSubmission = reader.GetDateTime(3);
                        existingMessage.TextMessage = reader.GetString(4);
                        Console.WriteLine(existingMessage);
                    }
                    reader.Close();
                    Console.WriteLine("\n\n");
                    Console.WriteLine("Give the ID of the Message you want to Edit: ");
                    string idForEdit = Console.ReadLine();
                    Console.WriteLine("Write the New Message: ");
                    string newMessage = Console.ReadLine();
                    SqlCommand cmdEditMessage = new SqlCommand($"UPDATE MessageInfo SET TextMessage = '{newMessage}'WHERE ID = '{idForEdit}'", sqlConnection);
                    int rowsUpdated = cmdEditMessage.ExecuteNonQuery();
                    if (rowsUpdated > 0)
                    {
                        Console.WriteLine("Edit was Successfull");
                        Console.WriteLine($"{rowsUpdated} rows updated successfully");
                    }
                    Thread.Sleep(3000);
                    Console.Clear();
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

        public override string ToString()
        {
            return "ID is: (" + ID + "), Sender is: (" + this.Sender + "), Receiver is: (" + this.Receiver + "), Date of Submission is: (" + this.DateOfSubmission + "), Message is: (" + this.TextMessage + ")";
        }
    }
}
