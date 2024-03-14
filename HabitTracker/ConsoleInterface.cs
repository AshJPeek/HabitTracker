using HabitTracker.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker
{
    internal class ConsoleInterface
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
                        MainMenuReturn();                       
                    }
                    break;
                case "1":
                    {
                        StepTrackerRepository.RetrieveRecords();
                        ReturnToMenu("Press any button to return to menu");
                        Console.Clear();
                        StepsTrackerMenu();
                    }
                    break;
                case "2":
                    {
                        string todaysDate = GetDate(); int stepsTaken = GetDataInput("Please enter the number of steps you have taken today");
                        StepTrackerRepository.Insert(todaysDate, stepsTaken);
                        Console.Clear(); StepsTrackerMenu();
                    }
                    break;
                case "3":
                    {
                        Console.Clear();
                        string recordId = GetStepsRecordId("Please enter the record Id of the record you would like to delete.");                       
                        StepTrackerRepository.DeleteRecords(recordId);
                        ReturnToMenu("Record successfully deleted. Press any button to return to menu");
                        Console.Clear(); StepsTrackerMenu();
                    }
                    break;
                case "4":
                    {
                        Console.Clear();
                        var recordId = GetStepsRecordId("Please enter the Id of the record you would like to update");
                        StepTrackerRepository.UpdateRecords(recordId);
                        ReturnToMenu("Record successfully updated. Press any button to return to menu");
                        Console.Clear(); StepsTrackerMenu();
                    }
                    break;
                default:
                    {
                        IncorrectMenuInput();
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
                        MainMenuReturn();
                    }
                    break;
                case "1":
                    {
                        CalorieTrackerRepository.RetrieveRecords();
                        ReturnToMenu("Press any button to return to menu");
                        Console.Clear();
                        CalorieTrackerMenu();
                    }
                    break;
                case "2":
                    {
                        string todaysDate = GetDate(); int stepsTaken = GetDataInput("Please enter the number of calories you have eaten today");
                        CalorieTrackerRepository.Insert(todaysDate, stepsTaken);
                        Console.Clear();
                        CalorieTrackerMenu();
                    }
                    break;
                case "3":
                    {
                        Console.Clear();
                        var recordId = GetCalorieRecordId("Please enter the Id of the record you would like to update");
                        CalorieTrackerRepository.DeleteRecords(recordId);
                        ReturnToMenu("Record successfully deleted. Press any button to return to menu");
                        Console.Clear(); CalorieTrackerMenu();
                    }
                    break;
                case "4":
                    {
                        Console.Clear();
                        var recordId = GetCalorieRecordId("Please enter the Id of the record you would like to update");
                        CalorieTrackerRepository.UpdateRecords(recordId);
                        ReturnToMenu("Record successfully updated. Press any button to return to menu");
                        Console.Clear(); CalorieTrackerMenu();
                        break;
                    }
                default:
                    {
                        Console.Clear();
                        ReturnToMenu("Please enter a number between 0 - 4. Press any button to return to menu");
                        Console.Clear();
                        CalorieTrackerMenu();
                    }
                    break;
            }

        }

        public static void ReturnToMenu(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
        }
        public static void MainMenuReturn()
        {
            Console.WriteLine("Returning to main menu. Press any button to continue.");
            Console.ReadLine();
            MenuOptions();
        }
        public static void IncorrectMenuInput()
        {
            Console.Clear();
            ReturnToMenu("Please enter a number between 0 - 4. Press any button to return to menu");
            Console.Clear(); StepsTrackerMenu();
        }
        public static string GetDate()
        {
            Console.Clear();
            Console.WriteLine("Please enter the date (dd-mm-yyyy)");
            string todaysDate = Console.ReadLine();
            if (todaysDate == "0") GetDate();

            while (!DateTime.TryParseExact(todaysDate, "dd-MM-yyyy", new CultureInfo("en-UK"), DateTimeStyles.None, out _))
            {
                Console.Clear();
                Console.WriteLine("Invalid date. (Format: dd-mm-yyyy). Please try again");
                todaysDate = Console.ReadLine();
            }
            Console.Clear();
            return todaysDate;
        }
        public static int GetDataInput(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            string dataInput = Console.ReadLine();

            if (dataInput == "0")
            {
                GetDataInput(message);
            }

            while (!Int32.TryParse(dataInput, out _) || Convert.ToInt32(dataInput) < 0)
            {
                Console.Clear();
                Console.WriteLine("Invalid number. Please try again.");
                dataInput = Console.ReadLine();
            }
            Console.Clear();

            int integerSteps = Convert.ToInt32(dataInput);
            return integerSteps;
        }
        public static string GetStepsRecordId(string message)
        {
            Console.Clear();
            StepTrackerRepository.RetrieveRecords();
            Console.WriteLine(message);
            var recordId = Console.ReadLine();
            return recordId;
        }
        public static string GetCalorieRecordId(string message)
        {
            Console.Clear();
            CalorieTrackerRepository.RetrieveRecords();
            Console.WriteLine(message);
            var recordId = Console.ReadLine();
            return recordId;
        }

    }


}

