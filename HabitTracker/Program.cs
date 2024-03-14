using HabitTracker;
using Microsoft.Data.Sqlite;

string connectionString = @"Data Source=habit-tracker.db";

using (var connection = new SqliteConnection(connectionString)) 
{ 
    connection.Open();
    var tableCmd = connection.CreateCommand();

    tableCmd.CommandText =
        @"CREATE TABLE IF NOT EXISTS number_of_steps (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Date TEXT,
            Quantity INTEGER
            )";
    tableCmd.CommandText =
        @"CREATE TABLE IF NOT EXISTS calories_eaten (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Date TEXT,
            Calories INTEGER
            )";


    tableCmd.ExecuteNonQuery();

    connection.Close();
}

ConsoleInterface.MenuOptions();