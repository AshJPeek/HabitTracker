using HabitTracker.ConsoleInterface;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.Persistence.Repositories
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
        public static List<Models.CaloriesEaten> RetrieveRecords()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM calories_eaten ";

                List<Models.CaloriesEaten> tableData = [];

                var reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                        new Models.CaloriesEaten
                        {
                            Id = reader.GetInt32(0),
                            Date = DateTime.ParseExact(reader.GetString(1), "dd-MM-yyyy", new CultureInfo("en-UK")),
                            Quantity = reader.GetInt32(2)
                        });
                    }
                }

                connection.Close();
                return tableData;

            }
        }
        public static int DeleteRecords(string recordId)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"DELETE from calories_eaten WHERE Id = '{recordId}'";

                return tableCmd.ExecuteNonQuery();
            }

        }
        public static bool UpdateRecords(string recordId, string date, int quantity)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM calories_eaten WHERE Id = {recordId})";
                var matchingRecords = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (matchingRecords == 0)
                {
                    connection.Close();
                    return false;
                }


                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"UPDATE calories_eaten SET date = '{date}', quantity = {quantity} WHERE Id = {recordId}";
                tableCmd.ExecuteNonQuery();
                connection.Close();

                return true;
            }
        }
        public static bool IdMatch(string recordId)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM calories_eaten WHERE Id = {recordId})";
                var matchingRecords = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (matchingRecords == 0)
                {
                    connection.Close();
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
