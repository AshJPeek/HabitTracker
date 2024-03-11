using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace HabitTracker
{
    internal class Helpers
    {
        static string connectionString = @"Data Source=habit-tracker.db";
        public static void Insert()
        {
            Console.Clear();
            string todaysDate = GetDate();
            Console.Clear();
            int stepsTaken = GetSteps();           

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                   $"INSERT INTO number_of_steps(date, quantity) VALUES ('{todaysDate}', {stepsTaken})";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }

        }
        public static void RetrieveRecords()
        {
            Console.Clear();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM number_of_steps ";

                List<NumberOfSteps> tableData = new();

                SqliteDataReader reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                        new NumberOfSteps
                        {
                            Id = reader.GetInt32(0),
                            Date = DateTime.ParseExact(reader.GetString(1), "dd-MM-yyyy", new CultureInfo("en-UK")),
                            Quantity = reader.GetInt32(2)
                        });
                    }
                }
                else
                {
                    Console.WriteLine("No records found. Press any button to continue.");
                    Console.ReadLine();                   
                }

                connection.Close();
                Console.WriteLine("-----------------------------------------");

                foreach (var row in tableData) 
                {
                    Console.WriteLine($"{row.Id} - {row.Date.ToString("dd-MM-yyyy")} - Steps: {row.Quantity}");
                }
                Console.WriteLine("-----------------------------------------");
            }
        }
        public static void DeleteRecords() 
        {
            Console.Clear();
            RetrieveRecords();
            var recordId = GetRecordId("Please enter the Id of the record you would like to delete");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"DELETE from number_of_steps WHERE Id = '{recordId}'";

                int rowCount = tableCmd.ExecuteNonQuery();

                if (rowCount == 0)
                {
                    Console.WriteLine($"\n Record with Id {recordId} doesn't exist");
                    DeleteRecords();
                }
            }

        }
        public static void UpdateRecords()
        {
            Console.Clear();
            RetrieveRecords();

            var recordId = GetRecordId("Please enter the Id of the record you would like to update");

            using ( var connection = new SqliteConnection(connectionString)) 
            {
                connection.Open();              

                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM number_of_steps WHERE Id = {recordId})";
                int checkInput = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (checkInput == 0)
                {
                    Console.WriteLine($"\nRecord with Id {recordId} doesn't exist.\n");
                    connection.Close();
                    UpdateRecords();
                }

                string date = GetDate();
                int quantity = GetSteps();

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"UPDATE number_of_steps SET date = '{date}', quantity = {quantity} WHERE Id = {recordId}";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }
        public static string GetDate()
        {
            Console.WriteLine("Please enter the date (dd-mm-yyyy)");
            string todaysDate = Console.ReadLine();
            if (todaysDate == "0") GetDate();

            while (!DateTime.TryParseExact(todaysDate, "dd-MM-yyyy", new CultureInfo("en-UK"), DateTimeStyles.None, out _))
            {
                Console.Clear();
                Console.WriteLine("Invalid date. (Format: dd-mm-yyyy). Please try again");
                todaysDate = Console.ReadLine();    
            }
            return todaysDate;
        }
        public static int GetSteps()
        {
            Console.WriteLine("Please enter the number of steps you have taken today");
            var stepsTaken = Console.ReadLine();

            if (stepsTaken == "0")
            {
                GetSteps();
            }

            while (!Int32.TryParse(stepsTaken, out _) || Convert.ToInt32(stepsTaken) < 0)
            {
                Console.Clear();
                Console.WriteLine("Invalid number. Please try again.");
                stepsTaken = Console.ReadLine();
            }

            int integerSteps = Convert.ToInt32(stepsTaken);
            return integerSteps;
        }
        public static string GetRecordId(string message)
        {
            Console.WriteLine(message);
            var recordId = Console.ReadLine();
            return recordId;
        }
        public static void ReturnToMenu(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
        }
    }

    public class NumberOfSteps
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
    }
}   

