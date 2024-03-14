using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.Repositories
{
    internal class CalorieTrackerRepository
    {
        static string connectionString = @"Data Source=habit-tracker.db";
        public static void Insert(string todaysDate, int dataInput)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                   $"INSERT INTO calories_eaten(date, quantity) VALUES ('{todaysDate}', {dataInput})";

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
                    $"SELECT * FROM calories_eaten ";

                List<CaloriesEaten> tableData = new();

                SqliteDataReader reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                        new CaloriesEaten
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
                    Console.WriteLine($"{row.Id} - {row.Date.ToString("dd-MM-yyyy")} - Calories: {row.Quantity}");
                }
                Console.WriteLine("-----------------------------------------");
            }
        }
        public static void DeleteRecords(string recordId)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"DELETE from calories_eaten WHERE Id = '{recordId}'";

                int rowCount = tableCmd.ExecuteNonQuery();

                if (rowCount == 0)
                {
                    Console.WriteLine($"\n Record with Id {recordId} doesn't exist");
                    DeleteRecords(recordId);
                }
            }

        }
        public static void UpdateRecords(string recordId)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM calories_eaten WHERE Id = {recordId})";
                int checkInput = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (checkInput == 0)
                {
                    Console.WriteLine($"\nRecord with Id {recordId} doesn't exist.\n");
                    connection.Close();
                    UpdateRecords(recordId);
                }

                string date = ConsoleInterface.GetDate();
                int quantity = ConsoleInterface.GetDataInput("Please enter the number of calories you have eaten today");

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"UPDATE calories_eaten SET date = '{date}', quantity = {quantity} WHERE Id = {recordId}";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }     
        public class CaloriesEaten
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public int Quantity { get; set; }
        }
    }
}
