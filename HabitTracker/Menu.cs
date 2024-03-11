using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker
{
    internal class Menu
    {
        public static void MenuOptions()
        {
            Console.Clear();
            var exitApplication = false;

            while (exitApplication == false)
            {
                Console.WriteLine("Main Menu\n");

                Console.WriteLine("Welcome to your step tracker, what would you like to do?\n");

                Console.WriteLine(@"
Type 0 to close the application.
Type 1 to view all records.
Type 2 to insert record.
Type 3 to delete record.
Type 4 to update record.");

                var menuItemChosen = Console.ReadLine();

                switch (menuItemChosen)
                {
                    case "0":
                        exitApplication = true;
                        Console.WriteLine("Have a nice day, application exiting...");
                        Console.ReadLine();
                        break;
                    case "1":
                        Helpers.RetrieveRecords();
                        Helpers.ReturnToMenu("Press any button to return to main menu");
                        Console.Clear();
                        break;
                    case "2":
                        {
                            Helpers.Insert();
                            Console.Clear();
                        }
                        break;
                    case "3":
                        {
                            Helpers.DeleteRecords();
                            Helpers.ReturnToMenu("Record successfully deleted. Press any button to return to main menu");
                            Console.Clear();
                        }
                        break;
                    case "4":
                        Helpers.UpdateRecords();
                        Helpers.ReturnToMenu("Record successfully updated. Press any button to return to main menu");
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        Helpers.ReturnToMenu("Please enter a number between 0 - 4. Press any button to return to main menu");
                        Console.Clear();
                        break;
                }
            }
        }   
    }
}
