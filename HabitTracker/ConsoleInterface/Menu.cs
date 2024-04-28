using HabitTracker.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.ConsoleInterface
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

                Console.WriteLine("Welcome to your habit tracker, what would you like to track?\n");

                Console.WriteLine(@"
Type S to track step count.
Type C to track calories.");

                var habitChosen = Console.ReadLine();
                switch (habitChosen)
                {
                    case "s":
                        StepsTrackerMenu();
                        break;
                    case "c":
                        CalorieTrackerMenu();
                        break;
                }
            }
        }
        public static void StepsTrackerMenu()
        {

            Console.Clear();
            Console.WriteLine(@"_______________________________________

Type 0 to return to main menu.
Type 1 to view all records.
Type 2 to insert record.
Type 3 to delete record.
Type 4 to update record.
_______________________________________");

            var menuItemChosen = Console.ReadLine();

            switch (menuItemChosen)
            {
                case "0":
                    {
                        MenuHelpers.MainMenuReturn();
                    }
                    break;
                case "1":
                    {
                        StepTrackerRepository.RetrieveRecords();
                        MenuHelpers.ReturnToMenu("Press any button to return to menu");
                        Console.Clear();
                        StepsTrackerMenu();
                    }
                    break;
                case "2":
                    {
                        string todaysDate = UserInput.GetDate(); int stepsTaken = UserInput.GetDataInput("Please enter the number of steps you have taken today");
                        StepTrackerRepository.Insert(todaysDate, stepsTaken);
                        Console.Clear(); StepsTrackerMenu();
                    }
                    break;
                case "3":
                    {
                        Console.Clear();
                        string recordId = UserInput.GetStepsRecordId("Please enter the record Id of the record you would like to delete.");
                        StepTrackerRepository.DeleteRecords(recordId);
                        MenuHelpers.ReturnToMenu("Record successfully deleted. Press any button to return to menu");
                        Console.Clear(); StepsTrackerMenu();
                    }
                    break;
                case "4":
                    {
                        Console.Clear();
                        var recordId = UserInput.GetStepsRecordId("Please enter the Id of the record you would like to update");
                        StepTrackerRepository.UpdateRecords(recordId);
                        MenuHelpers.ReturnToMenu("Record successfully updated. Press any button to return to menu");
                        Console.Clear(); StepsTrackerMenu();
                    }
                    break;
                default:
                    {
                        MenuHelpers.IncorrectMenuInput();
                    }
                    break;
            }

        }
        public static void CalorieTrackerMenu()
        {
            Console.Clear();
            Console.WriteLine(@"_______________________________________

Type 0 to return to main menu.
Type 1 to view all records.
Type 2 to insert record.
Type 3 to delete record.
Type 4 to update record.
_______________________________________");

            var menuItemChosen = Console.ReadLine();

            switch (menuItemChosen)
            {
                case "0":
                    {
                        MenuHelpers.MainMenuReturn();
                    }
                    break;
                case "1":
                    {
                        CalorieTrackerRepository.RetrieveRecords();
                        MenuHelpers.ReturnToMenu("Press any button to return to menu");
                        Console.Clear();
                        CalorieTrackerMenu();
                    }
                    break;
                case "2":
                    {
                        string todaysDate = UserInput.GetDate(); int stepsTaken = UserInput.GetDataInput("Please enter the number of calories you have eaten today");
                        CalorieTrackerRepository.Insert(todaysDate, stepsTaken);
                        Console.Clear();
                        CalorieTrackerMenu();
                    }
                    break;
                case "3":
                    {
                        Console.Clear();
                        var recordId = UserInput.GetCalorieRecordId("Please enter the Id of the record you would like to update");
                        CalorieTrackerRepository.DeleteRecords(recordId);
                        MenuHelpers.ReturnToMenu("Record successfully deleted. Press any button to return to menu");
                        Console.Clear(); CalorieTrackerMenu();
                    }
                    break;
                case "4":
                    {
                        Console.Clear();
                        var recordId = UserInput.GetCalorieRecordId("Please enter the Id of the record you would like to update");
                        CalorieTrackerRepository.UpdateRecords(recordId);
                        MenuHelpers.ReturnToMenu("Record successfully updated. Press any button to return to menu");
                        Console.Clear(); CalorieTrackerMenu();
                        break;
                    }
                default:
                    {
                        Console.Clear();
                        MenuHelpers.ReturnToMenu("Please enter a number between 0 - 4. Press any button to return to menu");
                        Console.Clear();
                        CalorieTrackerMenu();
                    }
                    break;
            }

        }




    }


}

