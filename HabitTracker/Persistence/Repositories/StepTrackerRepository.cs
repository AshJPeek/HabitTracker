using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.Persistence.Repositories
{
    public class StepTrackerRepository
    {
        static string connectionString = @"Data Source=habit-tracker.db";
        public static void Insert(string todaysDate, int stepsTaken)
        {
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
        public static List<Models.NumberOfSteps> RetrieveRecords()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM number_of_steps ";

                List<Models.NumberOfSteps> tableData = [];

                var reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                        new Models.NumberOfSteps
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
                tableCmd.CommandText = $"DELETE from number_of_steps WHERE Id = '{recordId}'";

                return tableCmd.ExecuteNonQuery();
            }

        }
        public static bool UpdateRecords(string recordId, string date, int quantity)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM number_of_steps WHERE Id = {recordId})";
                var matchingRecords = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (matchingRecords == 0)
                {
                    connection.Close();
                    return false;
                }

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"UPDATE number_of_steps SET date = '{date}', quantity = {quantity} WHERE Id = {recordId}";
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
                checkCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM number_of_steps WHERE Id = {recordId})";
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
