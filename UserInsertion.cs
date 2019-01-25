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
    class UserInsertion
    {
        // As Enumeration the Role each User can be declared
        public enum Role
        {
            SuperAdmin = 1,
            Admin,
            CommonUser 
        };

        // Properties
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        // Method to Create Users
        public static void CreateNewUser()
        {
            UserInsertion user = new UserInsertion();

            Console.WriteLine("Enter your FirstName: ");
            user.FirstName = Console.ReadLine();
            Console.WriteLine("Enter your LastName: ");
            user.LastName = Console.ReadLine();
            Console.WriteLine("Enter your UserName: ");
            user.UserName = Console.ReadLine();

            while (CheckSameUserName(user) == true)
            {
                Console.WriteLine("Your UserName already Exists. Please try another: ");
                user.UserName = Console.ReadLine();
                CheckSameUserName(user);
            }

            Console.WriteLine("Enter your Password: ");
            user.Password = Console.ReadLine();

            while (CheckPasswordExist(user) == true)
            {
                Console.WriteLine("Your Password doesn't match. Please try another: ");
                user.Password = Console.ReadLine();
                CheckPasswordExist(user);
            }

            InsertUserToSql(user);
        }

        // Validation that the inserted Password Exist
        public static bool CheckPasswordExist(UserInsertion user)
        {
            // Connection with the Database
            string connectionString =
            @"Server = LAPTOP-7FF4UD5B\SQLEXPRESS;Database = afdeDB; Trusted_Connection = True;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            var check = true;

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

                    SqlCommand cmdSelect = new SqlCommand($"SELECT * FROM UsersInfo WHERE Password LIKE '{user.Password}'", sqlConnection);
                    SqlDataReader reader = cmdSelect.ExecuteReader();
                    if (reader.Read())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Password doesn't match.");
                            check = true;
                        }
                    }
                    else
                    {
                        check = false;
                    }
                    reader.Close();
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
            return check;
        }

        // Validation that the inserted UserName is Unique
        public static bool CheckSameUserName(UserInsertion user)
        {
            // Connection with the Database
            string connectionString =
            @"Server = LAPTOP-7FF4UD5B\SQLEXPRESS;Database = afdeDB; Trusted_Connection = True;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            var check = true;

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

                    SqlCommand cmdSelect = new SqlCommand($"SELECT * FROM UsersInfo WHERE UserName LIKE '{user.UserName}'", sqlConnection);
                    SqlDataReader reader = cmdSelect.ExecuteReader();
                    if (reader.Read())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("UserName already Exists");
                            check = true;
                        }
                    }
                    else
                    {
                        check = false;
                    }
                    reader.Close();
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
            return check;
        }

        // Method to Send the Data to SQL Database
        public static void InsertUserToSql(UserInsertion user)
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

                    SqlCommand cmdInsert = new SqlCommand($"INSERT INTO UsersInfo(FirstName, LastName, UserName, Password, Role) VALUES('{user.FirstName}', '{user.LastName}', '{user.UserName}', '{user.Password}', '{(int)Role.CommonUser}')", sqlConnection);
                    int rowsInserted = cmdInsert.ExecuteNonQuery();
                    if (rowsInserted > 0)
                    {
                        Console.WriteLine("Insertion Successful");
                        Console.WriteLine($"{rowsInserted} rows inserted Successfully");
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

        // Method to Log in a User
        public static void UserLogin()
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

                    Console.WriteLine("Enter your UserName: ");
                    string usernameInserted = Console.ReadLine();
                    Console.WriteLine("Enter your Password: ");
                    string passwordInserted = Console.ReadLine();

                    SqlCommand cmdLogin = new SqlCommand($"SELECT ID, UserName, Password FROM UsersInfo WHERE UserName = '{usernameInserted}' AND Password = '{passwordInserted}'", sqlConnection);
                    SqlDataReader reader = cmdLogin.ExecuteReader();
                    UserInsertion user = new UserInsertion();
                    while (reader.Read())
                    {
                        
                        Console.WriteLine("User Logged In Successfully");
                        user.ID = reader.GetInt32(0);
                        user.UserName = reader.GetString(1);
                        user.Password = reader.GetString(2);
                        Console.WriteLine(user);
                        Thread.Sleep(3000);
                        Console.Clear();
                        LoginMenu.Menu(usernameInserted);
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

        // Method to Delete a User
        public static void DeleteUser()
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

                    Console.WriteLine("Enter a UserName to Delete: ");
                    string nameForDelete = Console.ReadLine();
                    SqlCommand cmdDelete = new SqlCommand($"DELETE FROM UsersInfo WHERE UserName = '{nameForDelete}'", sqlConnection);
                    int rowsDeleted = cmdDelete.ExecuteNonQuery();
                    if (rowsDeleted > 0)
                    {
                        Console.WriteLine("Delete Successfull");
                        Console.WriteLine($"{rowsDeleted} rows deleted successfully");
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

        // Method to Update a User's Password
        public static void UpdateUser()
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

                    Console.WriteLine("Enter a UserName for Update: ");
                    string nameForUpdate = Console.ReadLine();
                    Console.WriteLine("Enter a new Password for User: ");
                    string newPassword = Console.ReadLine();

                    SqlCommand cmdUpdate = new SqlCommand($"UPDATE UsersInfo SET Password = '{newPassword}'WHERE UserName = '{nameForUpdate}'", sqlConnection);
                    int rowsUpdated = cmdUpdate.ExecuteNonQuery();
                    if (rowsUpdated > 0)
                    {
                        Console.WriteLine("Update Successfull");
                        Console.WriteLine($"{rowsUpdated} rows upsated successfully");
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

        // Method to Assign new Role to existing Users
        public static void AssignRoleToUser()
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

                    Console.WriteLine("Enter a UserName to Assign a New Role: ");
                    string userNameForRoleUpdate = Console.ReadLine();
                    Console.WriteLine("Press *(1) for SuperAdmin* *(2) for Admin* *(3) for CommonUser*");
                    string newRole = Console.ReadLine();

                    SqlCommand cmdUpdate = new SqlCommand($"UPDATE UsersInfo SET Role = '{newRole}'WHERE UserName = '{userNameForRoleUpdate}'", sqlConnection);
                    int rowsUpdated = cmdUpdate.ExecuteNonQuery();
                    if (rowsUpdated > 0)
                    {
                        Console.WriteLine("Update Role Successfull");
                        Console.WriteLine($"{rowsUpdated} rows upsated successfully");
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

        // Method to Print the Data of a User
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb
                .Append($"User ID is {ID} ")
                .Append($"UserName is {UserName} ")
                .Append($"Password {Password} ");

            return sb.ToString();
        }



    }
}
